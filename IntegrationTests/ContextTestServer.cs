namespace IntegrationTests
{
    using Data.Repository;

    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.TestHost;
    using Microsoft.Extensions.DependencyInjection;

    public class ContextTestServer : TestServer
    {
        public ContextTestServer(IWebHostBuilder builder) : base(builder)
        {
            ApiContext = Host.Services.GetRequiredService<ApiContext>();
        }

        public ApiContext ApiContext { get; set; }
    }
}