using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(billsapp.Startup))]
namespace billsapp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
