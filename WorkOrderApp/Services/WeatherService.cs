using Microsoft.Extensions.Caching.Memory;
using System.Text.Json;

namespace WorkOrderApp.Services
{
    public class WeatherService
    {
        private readonly HttpClient _httpClient;
        private readonly IMemoryCache _cache;
        private readonly string _apiKey = "YOUR_API_KEY_HERE";

        public WeatherService(HttpClient httpClient, IMemoryCache cache)
        {
            _httpClient = httpClient;
            _cache = cache;
        }

        public async Task<WeatherResult?> GetWeatherAsync(double lat, double lon)
        {
            // check for caching first
            string cacheKey = $"weather_{lat}_{lon}";
            if (_cache.TryGetValue(cacheKey, out WeatherResult? cachedResult))
            {
                return cachedResult;
            }

            // if not, call the API
            var url = $"https://api.openweathermap.org/data/2.5/weather?lat={lat}&lon={lon}&appid=e3ea55d5c82a1e0427c29a64f094990c&units=metric"; //default public id for testing
            var response = await _httpClient.GetAsync(url);


            if (!response.IsSuccessStatusCode)
                return null;

            var json = await response.Content.ReadAsStringAsync();

            System.Diagnostics.Debug.WriteLine("==== WEATHER JSON ====");
            System.Diagnostics.Debug.WriteLine(json);

            return JsonSerializer.Deserialize<WeatherResult>(json, new JsonSerializerOptions{PropertyNameCaseInsensitive = true});    //ignore case        
        }
    }

    public class WeatherResult
    {
        public MainInfo Main { get; set; }
        public WeatherInfo[] Weather { get; set; }
    }

    public class MainInfo
    {
        public double Temp { get; set; }
    }

    public class WeatherInfo
    {
        public string Description { get; set; }
        public string Icon { get; set; }
    }
}
