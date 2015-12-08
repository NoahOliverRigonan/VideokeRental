using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(smartshop.Startup))]
namespace smartshop
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
