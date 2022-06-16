using Bogus;
using DevInSales.Models;
using FluentAssert;
using Newtonsoft.Json;
using NUnit.Framework;
using System.Text;

namespace DevInSales.Test.Integration
{
    [TestFixture]
    public class StateControllerTest : IntegrationBaseTest
    {
        [Test]
        public async Task Should_Post_State_Success()
        {
           GenerateToken();
           var faker = new Faker();
           var stateId = 11;
           var city = new City
            {
                Name = faker.Address.City(),
                State_Id = stateId,
           };
            var requestJson = JsonConvert.SerializeObject(city);
            var result = await Client.PostAsync($"/api/state/{stateId}/city", new StringContent(requestJson, Encoding.UTF8, "application/json"));
            result.EnsureSuccessStatusCode();
            var responseContent = await result.Content.ReadAsStringAsync();
            var responseModel = JsonConvert.DeserializeObject<State>(responseContent);
            responseModel.ShouldNotBeNull();
        }
    }
}
