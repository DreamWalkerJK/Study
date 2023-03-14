using Grpc.Service.Protos;
using Microsoft.Extensions.DependencyInjection;

namespace Grpc.Client.ConsoleApp
{
    internal class Program
    {
        /// <summary>
        /// gRPC 控制台客户端：
        /// 1、NuGet安装Google.Protobuf、Grpc.Net.Client、Grpc.Tools、Grpc.Net.ClientFactory包
        /// 2、复制Grpc.Service项目中的student.proto文件
        /// 3、编辑项目文件，新增在ItemGroup下，配置GrpcServices为Client
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            // 方式一
            //Demo.GetStudentByHttp();
            //Demo.GetStudentByHttps();

            // 方式二
            IServiceCollection services = new ServiceCollection();
            // 注册DemoIoC服务
            services.AddTransient<DemoIoC>();
            // 调用http时设置
            //AppContext.SetSwitch("System.Net.Http.SocketHttpHandler.Http2UnencryptedSupport", isEnabled: true);
            // 添加gRPC客户端服务
            services.AddGrpcClient<Student.StudentClient>(options =>
            {
                options.Address = new Uri("https://localhost:7288");
            }).ConfigureChannel(grpcOption => { });
            // 构建容器
            IServiceProvider serviceProvider = services.BuildServiceProvider();
            // 解析服务
            var grpcRequest = serviceProvider.GetService<DemoIoC>();
            // 调用解析出的方法
            grpcRequest.GetStudent();

            Console.ReadKey();
        }
    }
}