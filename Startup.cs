using Hangfire;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(MAP_REST.Startup))]

namespace MAP_REST
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
		    GlobalConfiguration.Configuration
                .UseSqlServerStorage("Hangfire");

			app.UseHangfireDashboard();
			app.UseHangfireServer();
        }
    }
}