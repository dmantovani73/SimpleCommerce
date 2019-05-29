using Funq;
using Serilog;
using ServiceStack;
using ServiceStack.Caching;
using ServiceStack.Data;
using ServiceStack.Logging;
using ServiceStack.Logging.Serilog;
using ServiceStack.OrmLite;
using ServiceStack.Redis;
using ServiceStack.Text;
using System;
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

        container.Register<IRedisClientsManager>(c => new RedisManagerPool(AppSettings.Get<string>("Redis")));
        container.Register<ICacheClient>(c => c.Resolve<IRedisClientsManager>().GetCacheClient());

        InitDb();
    }

    void InitDb()
    {
        var connectionString = AppSettings.Get<string>("ConnectionString");
        var dialectProvider = AppSettings.Get<DialectProvider>("DialectProvider");

        var dbFactory = new OrmLiteConnectionFactory(
            connectionString,
            DialectProviderFactory.Create(dialectProvider)
        );

        Register<IDbConnectionFactory>(dbFactory);

        using (var db = dbFactory.Open())
        {
            db.CreateTableIfNotExists<Product>();

            db.SaveAll(new[]
            {
                new Product
                {
                    Id = 1,
                    Name = ".NET Bot Black Sweatshirt",
                    Price = 19.5M,
                    PictureUrl = "/images/products/1.png",
                },
                new Product
                {
                    Id = 2,
                    Name = ".NET Black & White Mug",
                    Price = 8.5M,
                    PictureUrl = "/images/products/2.png",
                },
                new Product
                {
                    Id = 3,
                    Name = "Prism White T-Shirt",
                    Price = 12,
                    PictureUrl = "/images/products/3.png",
                }
            });
        }
    }
}