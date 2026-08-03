using System.Text.Json;
using WeatherAPI.Models;

namespace WeatherAPI.Service
{
	public class WeatherStorage
	{
		private readonly IHostEnvironment _env;
		private readonly IConfiguration _config;
		private readonly ILogger<WeatherStorage> _logger;
		private readonly string _directoryPath;
		private string StorageFilePath(string parsedDate) => Path.Combine(_directoryPath, $"{parsedDate}.json");

		private static readonly JsonSerializerOptions weatherJsonOpts = new JsonSerializerOptions
		{
			PropertyNameCaseInsensitive = true
		};

		public WeatherStorage(IHostEnvironment env, IConfiguration config, ILogger<WeatherStorage> logger)
		{
			_env = env;
			_config = config;
			_logger = logger;

			_directoryPath = Path.Combine(_env.ContentRootPath, _config["Storage:FolderName"] ?? "weather-data");			
			Directory.CreateDirectory(_directoryPath);
		}
		
		public bool IsWeatherStored(string parsedDate, out WeatherResult? storedDate)
		{
			storedDate = null;
			var dateFilepath = StorageFilePath(parsedDate);
			if (!File.Exists(dateFilepath))
			{				
				return false;
			}

			try
			{
				var json = File.ReadAllText(dateFilepath);
				storedDate = JsonSerializer.Deserialize<WeatherResult>(json, weatherJsonOpts);
				_logger.LogInformation($"Date exists in Storage - {0}", parsedDate);
				return true;
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, $"Failed fetching storage data for date - {0}", parsedDate);				
				return false;
			}
		}
		
		public async Task StoreWeather(string parsedDate, WeatherResult newRecord)
		{
			var dateFilepath = StorageFilePath(parsedDate);
			try
			{
				await File.WriteAllTextAsync(dateFilepath, JsonSerializer.Serialize(newRecord, weatherJsonOpts));
				_logger.LogInformation($"Successfully stored weather data for {0}", parsedDate);				
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, $"Failed to store weather data for {0}", parsedDate);				
			}
		}		
	}
}
