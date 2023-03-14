using Grpc.Service.Services;

namespace Grpc.Service.WebAPI
{
    /// <summary>
    /// WebAPI��ʹ��gRPC��
    /// ʵ�ֶ����ṩWebAPI���ݽӿڷ���ͬʱ�����ṩgRPC����
    /// 1��NuGet���Grpc.AspNetCore��
    /// 2�����student.proto�ļ�
    /// 3���༭��Ŀ�ļ�����ItemGroup������Ϊ"Server"
    /// 4�����StudentService.cs�ļ�
    /// 5��Program.csע��gRPC�����StudentService����
    /// </summary>
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // ע��gRPC����
            builder.Services.AddGrpc();

            var app = builder.Build();

            // Configure the HTTP request pipeline.

            app.UseHttpsRedirection();

            app.UseAuthorization();
            // ע��StudentService����
            app.MapGrpcService<StudentService>();


            app.MapControllers();

            app.Run();
        }
    }
}