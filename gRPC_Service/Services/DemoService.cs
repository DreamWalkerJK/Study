using Grpc.Core;
using System.Threading.Channels;

namespace gRPC_Service.Services
{
    public class DemoService : Demo.DemoBase
    {
        private readonly ILogger<DemoService> _logger;

        public DemoService(ILogger<DemoService> logger)
        {
            _logger = logger;
        }

        public override Task<DemoResponse> Unary(DemoRequest request, 
            ServerCallContext context)
        {
            // access gRPC request headers
            var userAgent = context.RequestHeaders.GetValue("user-agent");

            return Task.FromResult(new DemoResponse
            {
                ReponseMessage = $"Unary : {request.Header} : {request.Body} : {request.IsOk}"
            }) ;
        }

        public override async Task ServerStreaming(DemoRequest request,
            IServerStreamWriter<DemoResponse> responseStream, ServerCallContext context)
        {
            for (var i = 0; i < 10; i++)
            {
                await responseStream.WriteAsync(new DemoResponse());
                //await Task.Delay(TimeSpan.FromSeconds(1));
                await Task.Delay(TimeSpan.FromSeconds(1), context.CancellationToken);
            }
        }

        public override async Task<DemoResponse> ClientStreaming(
            IAsyncStreamReader<DemoRequest> requestStream, ServerCallContext context)
        {
            //while(await requestStream.MoveNext())
            //{
            //    var message = requestStream.Current;
            //}

            // C# 8 or later
            await foreach(var message in requestStream.ReadAllAsync())
            {

            }
            return new DemoResponse();
        }

        public override async Task BothWaysStreaming(IAsyncStreamReader<DemoRequest> requestStream,
            IServerStreamWriter<DemoResponse> responseStream, ServerCallContext context)
        {
            // basic usage : sends a response for each request
            //await foreach(var message in requestStream.ReadAllAsync())
            //{
            //    await responseStream.WriteAsync(new DemoResponse());
            //}

            // reading requests and sending responses simultaneously
            // read requests in a background task
            var readTask = Task.Run(async () =>
            {
                await foreach (var message in requestStream.ReadAllAsync())
                {
                    // process request
                }
            });

            // send responses until the client signals that it is complete
            while (!readTask.IsCompleted)
            {
                await responseStream.WriteAsync(new DemoResponse());
                await Task.Delay(TimeSpan.FromSeconds(1), context.CancellationToken);
            }
        }

        // a safe way to enable multiple threads to interact with a gRPC method
        // use the producer-consumer pattern with System.Threading.Channels
        public override async Task Download(DataRequest request,
            IServerStreamWriter<DataResponse> responseStream, ServerCallContext context)
        {
            // Creates a bounded channel for producing and consuming DataResponse messages
            // Starts a task to read messages from the channel and write them to the response stream
            // Writes messages to the channel from multiple threads

            var channel = Channel.CreateBounded<DataResponse>(new BoundedChannelOptions(capacity: 5));

            var consumerTask = Task.Run(async () =>
            {
                // consume messages from channel and write to response stream
                await foreach (var message in channel.Reader.ReadAllAsync())
                {
                    await responseStream.WriteAsync(message);
                }
            });

            var dataChunks = request.Value.Chunk(size: 10);

            // write messages to channel from multiple threads
            await Task.WhenAll(dataChunks.Select(
                async c =>
                {
                    var message = new DataResponse { BytesProcessed = c.Length };
                    await channel.Writer.WriteAsync(message);
                }));

            // Complete writing and wait for consumer to complete
            channel.Writer.Complete();
            await consumerTask;
        }
    }
}
