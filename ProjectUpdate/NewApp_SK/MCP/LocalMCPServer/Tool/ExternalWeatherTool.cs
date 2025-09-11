//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
using ModelContextProtocol.Server;
using System.ComponentModel;

namespace LocalMCPServer.Tool
{
    [McpServerToolType]
    internal class ExternalWeatherTool
    {
        private readonly HttpClient _httpClient = new();
        // Just hard-code here (not recommended for production)
        private readonly string _weatherApiKey = "your-api-key-here";

        [McpServerTool, Description("Get the current weather information for a specified city.")]
        public string? GetWeatherAsync(string cityName)
        {
            //var apiUrl = $"http://api.weatherapi.com/v1/current.json?key={Config.WeatherApiKey}&q={cityName}&aqi=no";
            //var apiUrl = $"https://wttr.in/{cityName}?format=j1";
            var apiUrl = $"http://api.weatherapi.com/v1/current.json?key={_weatherApiKey}&q={cityName}&aqi=no";

            try
            {
                HttpResponseMessage response = _httpClient.GetAsync(apiUrl).Result;
                response.EnsureSuccessStatusCode();

                var responseBody = response.Content.ReadAsStringAsync().Result;

                return responseBody;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching weather data: {ex.Message}");
                return null;
            }
        }
    }
}