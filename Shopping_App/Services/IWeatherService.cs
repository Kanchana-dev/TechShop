using Shopping_App.Models;

namespace Shopping_App.Services
{
	public interface IWeatherService
	{
		Task<IReadOnlyList<Weather>> GetWeatherDetails();
	}
}
