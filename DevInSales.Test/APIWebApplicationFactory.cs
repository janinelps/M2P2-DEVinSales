using DevInSales.Context;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.VisualStudio.TestPlatform.TestHost;

namespace DevInSales.Test
{
    public class APIWebApplicationFactory : WebApplicationFactory<Program>
	{
		protected override IHost CreateHost(IHostBuilder builder)
		{
			var root = new InMemoryDatabaseRoot();

			builder.ConfigureServices(services =>
			{
				services.AddScoped(sp =>
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
