using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(OnlineOrdering.Startup))]
namespace OnlineOrdering
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
