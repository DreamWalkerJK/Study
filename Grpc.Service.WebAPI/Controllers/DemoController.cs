using Microsoft.AspNetCore.Mvc;

namespace Grpc.Service.WebAPI.Controllers
{
    [Route("api/[controller]")]
    public class DemoController : Controller
    {
        [HttpGet]
        public string Get()
        {
            return "this is gRPC+WebAPI test demo";
        }
    }
}
