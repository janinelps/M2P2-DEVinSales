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

        [Test]
        public async Task Should_Post_State_Fail()
        {
            GenerateToken("usuario");
            var faker = new Faker();
            var stateId = 11;
            var city = new City
            {
                Name = faker.Address.City(),
                State_Id = stateId,
            };

            var requestJson = JsonConvert.SerializeObject(city);
            var result = await Client.PostAsync($"/api/state/{stateId}/city", new StringContent(requestJson, Encoding.UTF8, "application/json"));
            result.StatusCode.ShouldBeEqualTo(System.Net.HttpStatusCode.Forbidden);
        }

        [Test]
        public async Task Should_Delete_State_Success()
        {
            
            GenerateToken();
            var result = await Client.DeleteAsync($"/api/state/35");
            result.StatusCode.ShouldBeEqualTo(System.Net.HttpStatusCode.NoContent);
        }

        [Test]
        public async Task Should_Delete_State_Fail()
        {
            GenerateToken("usuario");
            var result = await Client.DeleteAsync($"/api/state/11");
            result.StatusCode.ShouldBeEqualTo(System.Net.HttpStatusCode.Forbidden);
        }

        [Test]
        public async Task Should_Put_State_Fail()
        {
            var faker = new Faker();
            var stateId = 11;
            var state = new State
            {
                Id = stateId,
                Name = faker.Address.City()
            };

            var requestJson = JsonConvert.SerializeObject(state);
            var result = await Client.PutAsync($"/api/state/{stateId}", new StringContent(requestJson, Encoding.UTF8, "application/json"));
            result.StatusCode.ShouldBeEqualTo(System.Net.HttpStatusCode.Unauthorized);
        }

        [Test]
        public async Task Should_Get_State_Fail()
        {
            var result = await Client.GetAsync("/api/state");
            result.StatusCode.ShouldBeEqualTo(System.Net.HttpStatusCode.Unauthorized);
        }

        [Test]
        public async Task Should_Get_State_Sucess()
        {
            var result = await Client.GetAsync("/api/state");
            result.StatusCode.ShouldBeEqualTo(System.Net.HttpStatusCode.Unauthorized);
        }
    }
}
