using WeatherAPI.Models;

namespace WeatherAPI.Repositories
{
	public interface IWeatherRepo
	{
		Task<IReadOnlyList<WeatherResult>> GetWeatherDetails(CancellationToken ct = default);
		Task<WeatherResult> GetWeatherAsync(string origDate, CancellationToken ct = default);
	}
}
