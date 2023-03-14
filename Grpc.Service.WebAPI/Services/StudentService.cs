using Grpc.Core;
using Grpc.Service.Protos;

namespace Grpc.Service.Services
{
    public class StudentService : Student.StudentBase
    {
        private readonly ILogger<StudentService> _logger;

        public StudentService(ILogger<StudentService> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// student.proto协议文件中定义的方法GetStudent，请求参数StudentRequest
        /// 响应参数
        /// </summary>
        /// <param name="request"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public override Task<StudentResponse> GetStudent(StudentRequest request, ServerCallContext context)
        {
            //return base.GetStudent(request, context);

            return Task.FromResult(new StudentResponse
            {
                Id = request.Id,
                Name = request.Name,
                Age = 10,
                Gender = "female"
            });
        }
    }
}
