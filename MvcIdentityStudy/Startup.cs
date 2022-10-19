using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MvcIdentityStudy.Startup))]
namespace MvcIdentityStudy
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
