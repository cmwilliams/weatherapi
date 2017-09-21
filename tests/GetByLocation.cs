using System.Net.Http;
using System.Threading.Tasks;
using api.Dto;
using Newtonsoft.Json;
using Xunit;

namespace tests
{

    public class GetByLocation : ApiControllerTestBase
    {
        private readonly HttpClient _client;

        public GetByLocation()
        {
            _client = base.GetClient();
        }

        [Theory]
        [InlineData("weather")]
        public async Task ReturnsCorrectCityIdForAtlanta(string controllerName)
        {
            var response = await _client.GetAsync($"/api/{controllerName}/Atlanta");
            response.EnsureSuccessStatusCode();
            var stringResponse = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<WeatherResponse>(stringResponse);

            Assert.Equal(4180439, result.id);
        }
    }
}