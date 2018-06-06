using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MVC_Without_EF.Startup))]
namespace MVC_Without_EF
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
