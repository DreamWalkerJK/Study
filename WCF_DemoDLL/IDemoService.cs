using System.Collections.Generic;
using System.ServiceModel;
using WCF_DemoDLL.Models;

namespace WCF_DemoDLL
{
    [ServiceContract]
    public interface IDemoService
    {
        [OperationContract]
        string GetHi(string name);

        [OperationContract]
        void AddStudent(string name, int age);

        [OperationContract]
        List<Student> GetStudents();
    }
}
