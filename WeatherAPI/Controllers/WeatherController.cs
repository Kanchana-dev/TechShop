using Microsoft.AspNetCore.Mvc;
using WeatherAPI.BusinessRules;

namespace WeatherAPI.Controllers
{
	[ApiController]
	[Route("api/weather")]
	public class WeatherController : ControllerBase
	{
		private readonly IWeatherBR _weatherBusinessRules;
		private readonly ILogger<WeatherController> _logger;

		public WeatherController(IWeatherBR weatherBusinessRules, ILogger<WeatherController> logger)
		{
			_weatherBusinessRules = weatherBusinessRules;
			_logger = logger;
		}

		[HttpGet]
		[Route("getweatherdetails")]
		public async Task<IActionResult> Get(CancellationToken ct = default)
		{
			_logger.LogInformation("In internal WeatherController");

			try
			{
				return Ok(await _weatherBusinessRules.GetWeatherDetails(ct));
			}
			catch (OperationCanceledException cte)
			{
				_logger.LogError($"Client cancelled Task: {cte.Message}{Environment.NewLine}{cte.StackTrace}");
				throw;
			}
			catch (Exception ex)
			{
				_logger.LogError($"{ex.Message}{Environment.NewLine}{ex.InnerException} {Environment.NewLine}{ex.StackTrace}");
				return StatusCode(500, ex.Message);
			}
		}
	}
}
