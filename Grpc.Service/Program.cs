using Grpc.Service.Services;

namespace Grpc.Service
{
    /// <summary>
    /// 创建自定义服务：
    /// 1、Protos文件夹创建protobuf buffer file，即x.proto文件
    /// 2、编辑项目文件，ItemGroup节点添加x.proto协议文件配置，生成项目
    /// 3、Services文件夹新增x.cs实现定义的服务
    /// 4、program.cs注册StudentService服务
    /// </summary>
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Additional configuration is required to successfully run gRPC on macOS.
            // For instructions on how to configure Kestrel and gRPC clients on macOS, visit https://go.microsoft.com/fwlink/?linkid=2099682

            // Add services to the container.
            // 添加 gRPC 服务
            builder.Services.AddGrpc();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            // 注册 GreeterService 服务
            app.MapGrpcService<StudentService>();
            app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

            app.Run();
        }
    }
}