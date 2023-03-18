using System.Collections.Generic;
using System.ServiceModel;
using WCF_ServiceDemo.Models;

namespace WCF_ServiceDemo
{
    [ServiceContract]
    public interface IDemoService
    {
        [OperationContract]
        bool AddStudent(Student student);

        [OperationContract]
        bool UpdateStudent(Student student);

        [OperationContract]
        bool DeleteStudent(string sId);

        [OperationContract]
        Student GetStudent(string sId);

        [OperationContract]
        List<Student> GetStudents();
    }
}
