using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;

namespace Fiver.Azure.Cache.Client
{
    public class Startup
    {
        public Startup(
            IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public static IConfiguration Configuration;

        public void ConfigureServices(
            IServiceCollection services)
        {
            services.AddScoped<IAzureCacheStorage>(factory =>
            {
                return new AzureCacheStorage(new AzureCacheSettings(
                    connectionString: Configuration["Cache_ConnectionString"]));
            });

            services.AddMvc();
        }

        public void Configure(
            IApplicationBuilder app,
            IHostingEnvironment env)
        {
            app.UseDeveloperExceptionPage();
            app.UseMvcWithDefaultRoute();
        }
    }
}
