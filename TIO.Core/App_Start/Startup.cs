using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(TIO.Core.Startup))]
namespace TIO.Core
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.MapSignalR();
        }
    }
}
