using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(GreaterHeights.WebMonkey.Startup))]
namespace GreaterHeights.WebMonkey
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
