using System;
using WCF_InvokeDemo.WCFDemoServiceReference;

namespace WCF_InvokeDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            var service = new DemoServiceClient();
            //var student = new Student()
            //{
            //    SId = "S_002",
            //    Name = "Jerry",
            //    Age = 4
            //};

            //var result = service.AddStudent(student);
            //Console.WriteLine($"add student result : {result}");

            var students = service.GetStudents();
            foreach(var item in students)
            {
                Console.WriteLine($"{item.SId}:{item.Name}:{item.Age}");
            }

            Console.ReadKey();
        }
    }
}
