using DevInSales.Context;
using DevInSales.Seeds;
using DevInSales.Services;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using System.Net.Http.Headers;
using System.Security.Claims;

namespace DevInSales.Test.Integration
{
    public abstract class IntegrationBaseTest
    {
        protected APIWebApplicationFactory Factory;
        protected HttpClient Client;
        protected ITokenService tokenService;
        protected SqlContext Context;


        [SetUp]
        public void SetUp()
        {
            Factory = new APIWebApplicationFactory();
            Client = Factory.CreateClient();
            using (var scope = Factory.Server.Services.CreateScope())
            {
                tokenService = scope.ServiceProvider.GetRequiredService<ITokenService>();
                Context = scope.ServiceProvider.GetRequiredService<SqlContext>();
                Context.AddRangeAsync(StateSeed.Seed);
                Context.SaveChangesAsync();
            }
        }

        protected void GenerateToken(string nomeProfile = "Administrador")
        {
            var claims = new List<Claim>
             {
                    new Claim(ClaimTypes.Name, "Teste"),
                    new Claim(ClaimTypes.Role, nomeProfile)
            };
            Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", tokenService.GenerateToken(claims));
        }

        
    }
}
