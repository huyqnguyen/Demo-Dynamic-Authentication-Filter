using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(DemoDynamicAuthenticationFilter.Startup))]
namespace DemoDynamicAuthenticationFilter
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
