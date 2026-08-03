using WeatherAPI.Models;

namespace WeatherAPI.Service
{
	public class MeteoClient
	{
		private readonly HttpClient _http;
		private readonly ILogger<MeteoClient> _logger;

		public MeteoClient(HttpClient http, ILogger<MeteoClient> logger)
		{
			_http = http;
			_logger = logger;
		}

		public async Task<WeatherResult?> GetWeatherAsync(string date, CancellationToken ct)
		{
			_logger.LogInformation($"Meteo GetWeatherAsync call started for date - {0}", date);

			try
			{
				var apiUrl = $"https://archive-api.open-meteo.com/v1/archive?" +
							$"latitude=32.78&longitude=-96.8&" +
							$"start_date={date}&end_date={date}&" +
							$"daily=temperature_2m_max,temperature_2m_min,precipitation_sum&timezone=auto";						

				var response = await _http.GetAsync(apiUrl, ct);
				response.EnsureSuccessStatusCode();

				var weatherData = await response.Content.ReadFromJsonAsync<WeatherResult.WeatherDTO>(ct);

				var daily = weatherData?.Daily;
				if (daily is null || daily?.Time is null || daily.Time.Length == 0)
				{
					return null;
				}

				return new WeatherResult(	
					providedDate: date,
					maxTemp: weatherData.Daily.Temperature2mMax[0],
					minTemp: weatherData.Daily.Temperature2mMin[0],
					precipitation: weatherData.Daily.PrecipitationSum[0],
					error: null
				);
			}
			catch (Exception ex)
			{
				_logger.LogError($"Failed open meteo api call GetWeatherAsync for date - {0}{Environment.NewLine}{ex}", date, ex);
				return null;
			}
		}

	}
}
