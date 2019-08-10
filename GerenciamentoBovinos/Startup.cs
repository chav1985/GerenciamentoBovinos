using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(GerenciamentoBovinos.Startup))]
namespace GerenciamentoBovinos
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
