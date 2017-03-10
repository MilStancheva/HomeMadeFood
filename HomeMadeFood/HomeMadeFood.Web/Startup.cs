using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(HomeMadeFood.Web.Startup))]
namespace HomeMadeFood.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
