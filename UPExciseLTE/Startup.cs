using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(UPExciseLTE.Startup))]
namespace UPExciseLTE
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
