using System;
using WebDemo.DemoWebReference;

namespace DemoCall
{
    class Program
    {
        static void Main(string[] args)
        {
            var webService = new DemoService();
            var hiJerry = webService.GetHi("jerry");
            var addResult = webService.Add(6, 8);
            var subResult = webService.Sub(9, 3);
            var mulResult = webService.Mul(7, 3);
            var divResult = webService.div(4, 2);

            Console.WriteLine(hiJerry);
            Console.WriteLine($"add:{addResult}");
            Console.WriteLine($"sub:{subResult}");
            Console.WriteLine($"mul:{mulResult}");
            Console.WriteLine($"div:{divResult}");

            Console.ReadKey();
        }
    }
}
