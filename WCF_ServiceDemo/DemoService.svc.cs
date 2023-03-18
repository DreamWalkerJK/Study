using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using WCF_ServiceDemo.Models;

namespace WCF_ServiceDemo
{
    // 加上此特性，表明当前服务为所有服务请求生成唯一服务对象，这样每个请求访问List集合就是公共的
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class DemoService : IDemoService
    {
        private List<Student> _students = new List<Student>();

        public bool AddStudent(Student student)
        {
            var selectStudent = _students.Where(s => s.SId.Equals(student.SId)).FirstOrDefault();

            if(selectStudent == null)
            {
                _students.Add(student);
                return true;
            }

            return false;
        }

        public bool DeleteStudent(string sId)
        {
            var student = _students.Where(s => s.SId.Equals(sId)).FirstOrDefault();

            if (student != null)
            {
                _students.Remove(student);
                return true;
            }

            return false;
        }

        public Student GetStudent(string sId)
        {
            return _students.Where(s => s.SId.Equals(sId)).FirstOrDefault();
        }

        public List<Student> GetStudents()
        {
            return _students;
        }

        public bool UpdateStudent(Student student)
        {
            var selectStudent = _students.Where(s => s.SId.Equals(student.SId)).FirstOrDefault();

            if(selectStudent != null)
            {
                var index = _students.IndexOf(selectStudent);
                _students[index].Name = string.IsNullOrEmpty(student.Name) ? selectStudent.Name : student.Name;
                _students[index].Age = student.Age == 0 ? selectStudent.Age : student.Age;
                return true;
            }

            return false;
        }
    }
}
