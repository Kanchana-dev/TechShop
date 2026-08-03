using Shopping_App.Models;
using System.Net.Http.Json;

namespace Shopping_App.Services
{
	public class WeatherProxyService : IWeatherService
	{
		private readonly HttpClient _http;
		private ILogger<WeatherProxyService> _logger;
		public WeatherProxyService(IHttpClientFactory clientFactory, ILogger<WeatherProxyService> logger)
		{
			_http = clientFactory.CreateClient("WeatherProxy");
			_logger = logger;
		}

		public async Task<IReadOnlyList<Weather>> GetWeatherDetails()
		{
			try
			{
				_logger.LogInformation($"In WeatherProxyService. Fetching Weather details");

				var response = await _http.GetAsync($"api/weather/getweatherdetails");
				response.EnsureSuccessStatusCode();

				var result = await response.Content.ReadFromJsonAsync<IReadOnlyList<Weather>>();
				return result!;
			}
			catch (Exception ex)
			{
				_logger.LogError($"{ex.Message}{Environment.NewLine}{ex.InnerException} {Environment.NewLine}{ex.StackTrace}");
				return null;
			}
		}
	}
}
