using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(VideokeRental.Startup))]
namespace VideokeRental
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
