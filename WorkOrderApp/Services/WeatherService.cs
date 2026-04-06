using Microsoft.Extensions.Caching.Memory;
using System.Text.Json;

namespace WorkOrderApp.Services
{
    public class WeatherService
    {
        private readonly HttpClient _httpClient;
        private readonly IMemoryCache _cache;
        private readonly string _apiKey = "YOUR_API_KEY_HERE";

        public WeatherService(HttpClient httpClient, IMemoryCache cache, IConfiguration config)
        {
            _httpClient = httpClient;
            _cache = cache;
            _apiKey = config["OpenWeather:ApiKey"]!;
        }

        public async Task<WeatherResult?> GetWeatherAsync(double lat, double lon)
        {
            // check for caching first
            string cacheKey = $"weather_{lat}_{lon}";
            if (_cache.TryGetValue(cacheKey, out WeatherResult? cachedResult))
            {
                return cachedResult;
            }

            if (string.IsNullOrWhiteSpace(_apiKey))
            {
                throw new InvalidOperationException("OpenWeather API Key is not configured.");
            }

            var url = $"https://api.openweathermap.org/data/2.5/weather?lat={lat}&lon={lon}&appid={_apiKey}&units=metric"; //moved default string to appsettings
            var response = await _httpClient.GetAsync(url);


            if (!response.IsSuccessStatusCode)
                return null;

            var json = await response.Content.ReadAsStringAsync();

            var result =  JsonSerializer.Deserialize<WeatherResult>(json, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
            
            _cache.Set(cacheKey, result, TimeSpan.FromMinutes(30));

            return result;
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
