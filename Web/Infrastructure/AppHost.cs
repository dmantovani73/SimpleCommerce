using Funq;
using Serilog;
using ServiceStack;
using ServiceStack.Api.Swagger;
using ServiceStack.Auth;
using ServiceStack.Data;
using ServiceStack.Logging;
using ServiceStack.Logging.Serilog;
using ServiceStack.OrmLite;
using ServiceStack.Redis;
using System.IO;
using System.Reflection;

public class AppHost : AppHostBase
{
    public AppHost() : base("", typeof(AppHost).Assembly)
    { }

    public override void Configure(Container container)
    {
        SetConfig(new HostConfig { HandlerFactoryPath = "api" });

        Plugins.Add(new SwaggerFeature());

        LogManager.LogFactory = new SerilogFactory(new LoggerConfiguration()
            .WriteTo.File(path: Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), "Log.log"), rollingInterval: RollingInterval.Day)
            .CreateLogger()
        );

        container.Register<IRedisClientsManager>(c => new RedisManagerPool(AppSettings.Get<string>("Redis")));
        container.Register(c => c.Resolve<IRedisClientsManager>().GetCacheClient());

        var dbFactory = new OrmLiteConnectionFactory(AppSettings.Get<string>("ConnectionString"), SqliteDialect.Provider);
        container.Register<IDbConnectionFactory>(dbFactory);
        container.Register(c => new UnitOfWork(dbFactory)).ReusedWithin(ReuseScope.Request);

        InitDb();
        InitAuth();
    }

    void InitDb()
    {
        var dbFactory = TryResolve<IDbConnectionFactory>();
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

    void InitAuth()
    {
        var container = Container;

        Plugins.Add(new AuthFeature(
            () => new AuthUserSession(),
            new IAuthProvider[]
            {
                new CredentialsAuthProvider(), //HTML Form post of UserName/Password credentials
                new FacebookAuthProvider(AppSettings),
            }
        ));

        Plugins.Add(new RegistrationFeature());

        container.Register<IAuthRepository>(c => new OrmLiteAuthRepository(c.Resolve<IDbConnectionFactory>()));
        container.Resolve<IAuthRepository>().InitSchema();
    }
}