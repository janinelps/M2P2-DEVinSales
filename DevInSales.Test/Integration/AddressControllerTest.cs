using DevInSales.Models;
using DevInSales.Test.Fixtures;
using FluentAssert;
using Newtonsoft.Json;
using NUnit.Framework;
using System.Text;

namespace DevInSales.Test.Integration
{
    [TestFixture]
    public class AddressControllerTest: IntegrationBaseTest
	{
		[Test]
		public async Task Should_Post_Address_Success()
		{
			GenerateToken();
			var address = AddressFixture.GenratePostAddress();
			var requestJson = JsonConvert.SerializeObject(address);
			var result = await Client.PostAsync("/api/address", new StringContent(requestJson, Encoding.UTF8, "application/json"));
			result.EnsureSuccessStatusCode();
			var responseContent = await result.Content.ReadAsStringAsync();
			var responseModel = JsonConvert.DeserializeObject<Address>(responseContent);
			responseModel.ShouldNotBeNull();
			responseModel.CEP.ShouldBeEqualTo(address.CEP);
		}

		[Test]
		public async Task Should_Post_Address_Fail()
		{
			GenerateToken("usuario");
			var address = AddressFixture.GenratePostAddress();
			var requestJson = JsonConvert.SerializeObject(address);
			var result = await Client.PostAsync("/api/address", new StringContent(requestJson, Encoding.UTF8, "application/json"));
			result.StatusCode.ShouldBeEqualTo(System.Net.HttpStatusCode.Forbidden);
		}

		[Test]
		public async Task Should_Delete_Address_Success()
		{
			var address = AddressFixture.GenrateAddress();
			var repo = new ContextInMemory<Address>(Context, Factory);
			repo.AddInMemoryDatabase(address);

			GenerateToken();
			var result = await Client.DeleteAsync($"/api/address/{address.Id}" );
			result.StatusCode.ShouldBeEqualTo(System.Net.HttpStatusCode.NoContent);
		}

		[Test]
		public async Task Should_Delete_Address_Fail()
		{
			GenerateToken("usuario");
			var result = await Client.DeleteAsync($"/api/address/1");
			result.StatusCode.ShouldBeEqualTo(System.Net.HttpStatusCode.Forbidden);
		}

		[Test]
		public async Task Should_Put_Address_Success()
		{
			var address = AddressFixture.GenrateAddress();
			var repo = new ContextInMemory<Address>(Context, Factory);
			repo.AddInMemoryDatabase(address);
			address.CEP = "31100000";
			GenerateToken();
			
			var requestJson = JsonConvert.SerializeObject(address);
			var result = await Client.PutAsync($"/api/address/{address.Id}", new StringContent(requestJson, Encoding.UTF8, "application/json"));
			result.StatusCode.ShouldBeEqualTo(System.Net.HttpStatusCode.NoContent);
		}

        [Test]
        public async Task Should_Put_Address_Fail()
        {
            var address = AddressFixture.GenrateAddress();
            var repo = new ContextInMemory<Address>(Context, Factory);
            repo.AddInMemoryDatabase(address);
            address.Id +=1;
			GenerateToken();
            var requestJson = JsonConvert.SerializeObject(address);
            var result = await Client.PutAsync($"/api/address/{address.Id-1}", new StringContent(requestJson, Encoding.UTF8, "application/json"));
            result.StatusCode.ShouldBeEqualTo(System.Net.HttpStatusCode.BadRequest);
        }

        [Test]
        public async Task Should_Get_Address_All_Success()
        {
            GenerateToken();
            var result = await Client.GetAsync("/api/address");
            result.StatusCode.ShouldBeEqualTo(System.Net.HttpStatusCode.OK);

        }

		[Test]
		public async Task Should_Get_Address_Unauthorized_Fail()
		{
            GenerateToken();
			var result = await Client.GetAsync("/api/address/9999");
			result.StatusCode.ShouldBeEqualTo(System.Net.HttpStatusCode.NotFound);

		}

        [Test]
        public async Task Should_Get_Address_Fail()
        {
            var result = await Client.GetAsync("/api/address");
            result.StatusCode.ShouldBeEqualTo(System.Net.HttpStatusCode.Unauthorized);

        }

		[Test]
        public async Task Should_Get_Address_Success()
		{
			var address = AddressFixture.GenrateAddress();
            var repo = new ContextInMemory<Address>(Context, Factory);
            repo.AddInMemoryDatabase(address);

			GenerateToken();
			var result = await Client.GetAsync($"/api/address/{address.Id}");
            result.StatusCode.ShouldBeEqualTo(System.Net.HttpStatusCode.OK);

        }

        [Test]
        public async Task Should_Get_All_Address_Success()
        {
            GenerateToken();
            var result = await Client.GetAsync($"/api/address");
            result.StatusCode.ShouldBeEqualTo(System.Net.HttpStatusCode.OK);

        }
	}
}