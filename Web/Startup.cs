using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ServiceStack;
using ServiceStack.Configuration;
using ServiceStack.Logging;
using System;

namespace Web
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration) => Configuration = configuration;

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();
            app.UseMvcWithDefaultRoute();

            //Register your ServiceStack AppHost as a .NET Core module
            app.UseServiceStack(new AppHost
            {
                AppSettings = new NetCoreAppSettings(Configuration) // Use **appsettings.json** and config sources
            });

            app.Run(async (context) =>
            {
                var appSettings = HostContext.AppSettings;
                var container = HostContext.Container;
                var log = LogManager.GetLogger(typeof(Startup));

                log.Info("Run");
                await context.Response.WriteAsync(appSettings.Get<string>("Message"));
            });
        }
    }
}

static class AppSettingsUtils
{
    public static T GetOrException<T>(this IAppSettings appSettings, string key)
    {
        if (appSettings.GetAllKeys().Contains(key)) throw new Exception("");

        return appSettings.Get<T>(key);
    }
}

class Endpoint
{
    public string Host { get; set; }

    public int Port { get; set; }
}
