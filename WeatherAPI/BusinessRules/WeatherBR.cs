using WeatherAPI.Models;
using WeatherAPI.Repositories;

namespace WeatherAPI.BusinessRules
{
	public class WeatherBR: IWeatherBR
	{
		private readonly IWeatherRepo _weatherRepo;
		private readonly ILogger<WeatherBR> _logger;

		public WeatherBR(IWeatherRepo weatherRepo, ILogger<WeatherBR> logger)
		{
			_weatherRepo = weatherRepo;
			_logger = logger;
		}


		public async Task<IReadOnlyList<WeatherResult>> GetWeatherDetails(CancellationToken ct)
		{
			_logger.LogInformation($"In WeatherBR. Calling WeatherRepo");

			var result = await _weatherRepo.GetWeatherDetails(ct);
			if (result?.Count > 0)
			{
				return result;
			}

			return new List<WeatherResult>();

		}
	}
}
