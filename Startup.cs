using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Capstone1.Startup))]
namespace Capstone1
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
