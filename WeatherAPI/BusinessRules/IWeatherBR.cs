using WeatherAPI.Models;

namespace WeatherAPI.BusinessRules
{
	public interface IWeatherBR
	{
		Task<IReadOnlyList<WeatherResult>> GetWeatherDetails(CancellationToken ct = default);
	}
}
