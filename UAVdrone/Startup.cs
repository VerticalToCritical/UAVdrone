using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(UAVdrone.Startup))]
namespace UAVdrone
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
