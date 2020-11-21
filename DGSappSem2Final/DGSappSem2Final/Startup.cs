using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(DGSappSem2Final.Startup))]
namespace DGSappSem2Final
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
