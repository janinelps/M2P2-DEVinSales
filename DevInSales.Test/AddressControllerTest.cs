using DevInSales.Models;
using DevInSales.Test.Fixtures;
using FluentAssert;
using Newtonsoft.Json;
using NUnit.Framework;
using System.Text;

namespace DevInSales.Test
{
    [TestFixture]
    public class AddressControllerTest
    {
		private APIWebApplicationFactory _factory;
		private HttpClient _client;


		[SetUp]
		public void SetUp()
		{
			_factory = new APIWebApplicationFactory();
			_client = _factory.CreateClient();
		}

		[Test]
		public async Task Should_Post_Address_Success()
		{
			var andrress = AddressFixture.GenratePostAddress();
			var requestJson = JsonConvert.SerializeObject(andrress);
			var result = await _client.PostAsync("/api/address", new StringContent(requestJson, Encoding.UTF8, "application/json"));
			result.EnsureSuccessStatusCode();
			var responseContent = await result.Content.ReadAsStringAsync();
			var responseModel = JsonConvert.DeserializeObject<Address>(responseContent);
			responseModel.ShouldNotBeNull();
			responseModel.CEP.ShouldBeEqualTo(andrress.CEP);
		}

	}
}