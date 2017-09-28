using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using api.Dto;
using Microsoft.Extensions.Options;

namespace api.Controllers
{
    [Route("api/[controller]")]
    public class WeatherController : Controller
    {
        private readonly ApiSettings _settings;

        public WeatherController(IOptions<ApiSettings> settings)
        {
            _settings = settings.Value;
        }

        // GET api/weather/location
        [HttpGet("{location}")]
        public async Task<WeatherResponse> GetAsync(string location)
        {
            //Open Weather API Key
            var apiKey = _settings.OpenWeatherApiKey;

            //Call Open Weather API and return results
            var httpClient = new HttpClient();
            var response = await httpClient.GetAsync("http://api.openweathermap.org/data/2.5/weather?q=" + location + "&appid=" + apiKey + "&units=imperial");
            var weatherResult = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<WeatherResponse>(weatherResult);
        }
    }
}
