using WeatherAPI.Helper;
using WeatherAPI.Models;
using WeatherAPI.Service;
namespace WeatherAPI.Repositories
{
	public class WeatherRepo : IWeatherRepo
	{
		private readonly ILogger<WeatherRepo> _logger;
		private readonly WeatherStorage _storageService;
		private readonly MeteoClient _meteoClient;

		public WeatherRepo(ILogger<WeatherRepo> logger, WeatherStorage storageService, MeteoClient meteoClient)
		{
			_logger = logger;
			_storageService = storageService;
			_meteoClient = meteoClient;
		}

		public async Task<IReadOnlyList<WeatherResult>> GetWeatherDetails(CancellationToken ct = default)
		{
			_logger.LogInformation("In WeatherRepo GetWeatherDetails");
			var taskList = new List<Task<WeatherResult>>();

			var weatherTasks = WeatherHelper.ReadFromDatesFile(_logger).Select(origDate => GetWeatherAsync(origDate, ct));
			taskList.AddRange(weatherTasks);
			var results = await Task.WhenAll(taskList);

			return results.ToList();	
		}

		public async Task<WeatherResult> GetWeatherAsync(string origDate, CancellationToken ct = default)
		{
			if(string.IsNullOrEmpty(origDate))
			{
				_logger.LogError("Empty input date");
				return new WeatherResult(origDate, null, null, null, "No date was provided");
			}

			var (isoDate, error) = WeatherHelper.GetISODate(origDate);
			if (isoDate is null)
			{
				_logger.LogWarning($"Invalid date '{0}': {1}", origDate, error);
				return new WeatherResult(origDate, null, null, null, "Invalid Input Date");
			}

			if (_storageService.IsWeatherStored(isoDate, out WeatherResult? savedWeather) && savedWeather is not null)
				return savedWeather;

			var weatherresult = await _meteoClient.GetWeatherAsync(isoDate, ct);

			if (weatherresult is null)
				return new WeatherResult();
			else
			{
				await _storageService.StoreWeather(isoDate, weatherresult);
				return weatherresult;
			}			
		}
	}
}
