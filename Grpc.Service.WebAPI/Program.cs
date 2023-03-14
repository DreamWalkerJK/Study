using Grpc.Service.Services;

namespace Grpc.Service.WebAPI
{
    /// <summary>
    /// WebAPI中使用gRPC：
    /// 实现对外提供WebAPI数据接口服务，同时对内提供gRPC服务
    /// 1、NuGet添加Grpc.AspNetCore包
    /// 2、添加student.proto文件
    /// 3、编辑项目文件，在ItemGroup中配置为"Server"
    /// 4、添加StudentService.cs文件
    /// 5、Program.cs注册gRPC服务和StudentService服务
    /// </summary>
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // 注册gRPC服务
            builder.Services.AddGrpc();

            var app = builder.Build();

            // Configure the HTTP request pipeline.

            app.UseHttpsRedirection();

            app.UseAuthorization();
            // 注册StudentService服务
            app.MapGrpcService<StudentService>();


            app.MapControllers();

            app.Run();
        }
    }
}