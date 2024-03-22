namespace IntegrationTests.Tests
{
    using Data.Repository;

    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Configuration;

    using ParfoisDev;

    public class BaseTests
    {
        public static ContextTestServer CreateServer()
        {
            IWebHostBuilder hostBuilder = new WebHostBuilder()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .ConfigureAppConfiguration((context, configurationBuilder) =>
                {
                    configurationBuilder
                        .SetBasePath(Directory.GetCurrentDirectory())
                        .AddJsonFile("appsettings.json", optional: false)
                        .AddEnvironmentVariables();
                })
                .UseStartup<Startup>();

            ContextTestServer testServer = new(hostBuilder);

            testServer.Host.MigrateDbContext<ApiContext>((_, __) => { });

            return testServer;
        }
    }
}
