using DevInSales.Context;
using DevInSales.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace DevInSales.Test.Integration
{
    public class APIWebApplicationFactory : WebApplicationFactory<Program>
	{
        protected override IHost CreateHost(IHostBuilder builder)
        {
            var root = new InMemoryDatabaseRoot();

            builder.ConfigureServices(services =>
            {
                services.AddScoped<ITokenService, TokenService>();
                services.AddSingleton(sp =>
                {
                    return new DbContextOptionsBuilder<SqlContext>()
                        .UseInMemoryDatabase("InMemoryDatabase", root)
                        .UseApplicationServiceProvider(sp)
                        .Options;
                });
            });

            return base.CreateHost(builder);
        }
    }
}
