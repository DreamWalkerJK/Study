using Grpc.Net.Client;

namespace gRPC_Client
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            using var channel = GrpcChannel.ForAddress("https://localhost:7129");
            var client = new Greeter.GreeterClient(channel);
            var reply = await client.SayHelloAsync(
                new HelloRequest { Name = "gRPC_Client" });

            Console.WriteLine($"Greeting: {reply.Message}");
            Console.ReadKey();
        }
    }
}