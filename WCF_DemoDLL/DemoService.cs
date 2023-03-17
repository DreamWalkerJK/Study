using System.Collections.Generic;
using System.ServiceModel;
using WCF_DemoDLL.Models;

namespace WCF_DemoDLL
{
    // 加上此特性，表明当前服务为所有服务请求生成唯一服务对象，这样每个请求访问List集合就是公共的
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class DemoService : IDemoService
    {
        private List<Student> _list = new List<Student>();

        public void AddStudent(string name, int age)
        {
            _list.Add(new Student { Name = name, Age = age });
        }

        public string GetHi(string name)
        {
            return $"Hi,{name}";
        }

        public List<Student> GetStudents()
        {
            return _list;
        }
    }
}
