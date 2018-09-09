using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Joinup.Backend.Startup))]
namespace Joinup.Backend
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
