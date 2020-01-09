using Microsoft.Owin;
using Owin;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Services.Description;

[assembly: OwinStartupAttribute(typeof(_3dBay2.Startup))]
namespace _3dBay2
{
    public partial class Startup
    {
        public void ConfigureServices(ServiceCollection services)
        {
            
        }
        public void Configuration(IAppBuilder app)
        {
         
            ConfigureAuth(app);
        }
    }
}
