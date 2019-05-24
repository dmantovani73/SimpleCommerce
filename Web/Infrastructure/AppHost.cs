using Funq;
using Serilog;
using ServiceStack;
using ServiceStack.Data;
using ServiceStack.Logging;
using ServiceStack.Logging.Serilog;
using ServiceStack.OrmLite;
using System.IO;
using System.Reflection;

public class AppHost : AppHostBase
{
    public AppHost() : base("SimpleCommerce", typeof(AppHost).Assembly)
    { }

    public override void Configure(Container container)
    {
        LogManager.LogFactory = new SerilogFactory(
            new LoggerConfiguration()
                .WriteTo.File(
                    path: Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), "Log.log"), 
                    rollingInterval: RollingInterval.Day)
                .MinimumLevel.Information()
                .CreateLogger()
        );

        var dbFactory = new OrmLiteConnectionFactory(
            AppSettings.Get<string>("ConnectionString"), SqliteDialect.Provider
        );

        container.Register<IDbConnectionFactory>(dbFactory);
    }
}