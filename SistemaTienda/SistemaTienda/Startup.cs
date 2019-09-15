using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SistemaTienda.Startup))]
namespace SistemaTienda
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
