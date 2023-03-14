using Grpc.Service.Protos;

namespace Grpc.Client.ConsoleApp
{
    /// <summary>
    /// 方式二
    /// 依赖注入方式调用gRPC
    /// </summary>
    public class DemoIoC
    {
        private readonly Student.StudentClient _client;

        public DemoIoC(Student.StudentClient client)
        {
            _client = client;
        }

        public void GetStudent()
        {
            var student = _client.GetStudent(new StudentRequest()
            {
                Id = "S_001",
                Name = "Tom"
            });

            Console.WriteLine(student);
        }
    }
}
