using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MindTheGapProject.Startup))]
namespace MindTheGapProject
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
