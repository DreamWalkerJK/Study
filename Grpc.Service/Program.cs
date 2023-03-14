using Grpc.Service.Services;

namespace Grpc.Service
{
    /// <summary>
    /// �����Զ������
    /// 1��Protos�ļ��д���protobuf buffer file����x.proto�ļ�
    /// 2���༭��Ŀ�ļ���ItemGroup�ڵ����x.protoЭ���ļ����ã�������Ŀ
    /// 3��Services�ļ�������x.csʵ�ֶ���ķ���
    /// 4��program.csע��StudentService����
    /// </summary>
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Additional configuration is required to successfully run gRPC on macOS.
            // For instructions on how to configure Kestrel and gRPC clients on macOS, visit https://go.microsoft.com/fwlink/?linkid=2099682

            // Add services to the container.
            // ��� gRPC ����
            builder.Services.AddGrpc();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            // ע�� GreeterService ����
            app.MapGrpcService<StudentService>();
            app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

            app.Run();
        }
    }
}