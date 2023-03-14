using Grpc.Net.Client;
using Grpc.Service.Protos;

namespace Grpc.Client.ConsoleApp
{
    /// <summary>
    /// 方式一
    /// </summary>
    public class Demo
    {
        /// <summary>
        /// 调用服务端的Http地址
        /// </summary>
        public static void GetStudentByHttp()
        {
            AppContext.SetSwitch("System.Net.Http.SocketHttpHandler.Http2UnencryptedSupport", isEnabled: true);
            var httpUrl = "http://localhost:5283";

            using (var channel = GrpcChannel.ForAddress(httpUrl))
            {
                var client = new Student.StudentClient(channel);
                var student = client.GetStudent(new StudentRequest()
                {
                    Id = "S_001",
                    Name = "Tom"
                });

                Console.WriteLine(student);
            }
        }

        /// <summary>
        /// 调用服务端的Https地址
        /// </summary>
        public static void GetStudentByHttps() 
        {
            var httpsUrl = "https://localhost:7177";

            using (var channel = GrpcChannel.ForAddress(httpsUrl))
            {
                var client = new Student.StudentClient(channel);
                var student = client.GetStudent(new StudentRequest()
                {
                    Id = "S_001",
                    Name = "Tom"
                });

                Console.WriteLine(student);
            }
        }
    }
}
