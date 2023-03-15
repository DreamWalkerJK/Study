using System.Web.Services;

namespace WebDemo
{
    /// <summary>
    /// Summary description for DemoService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class DemoService : System.Web.Services.WebService
    {

        [WebMethod]
        public string GetHi(string name)
        {
            return $"Hi,{name}";
        }

        [WebMethod]
        public double Add(double a, double b)
        {
            return a + b;
        }

        [WebMethod]
        public double Sub(double a, double b)
        {
            return a - b;
        }

        [WebMethod]
        public double Mul(double a, double b)
        {
            return a * b;
        }

        [WebMethod]
        public double div(double a, double b)
        {
            return a / b;
        }
    }
}
