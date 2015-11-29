using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(WhiteLotus.Startup))]
namespace WhiteLotus
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
