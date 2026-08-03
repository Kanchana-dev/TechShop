using System.Text.Json.Serialization;

namespace WeatherAPI.Models
{
	public class WeatherResult
	{
		public string? ProvidedDate { get; set; }
		public double? MaxTemp { get; set; }
		public double? MinTemp { get; set; }
		public double? Precipitation { get; set; }
		public string? ErrorMessage { get; set; }

		public WeatherResult() 
		{ 
		}

		public WeatherResult(string providedDate, double? maxTemp, double? minTemp, double? precipitation, string? error)
		{
			ProvidedDate = providedDate;
			MaxTemp = maxTemp;
			MinTemp = minTemp;
			Precipitation = precipitation;
			ErrorMessage = error;
		}

		public class WeatherDTO
		{
			[JsonPropertyName("daily")]
			public DailyDTO? Daily { get; set; }
		}

		public class DailyDTO
		{
			[JsonPropertyName("time")]
			public string[]? Time { get; set; }

			[JsonPropertyName("temperature_2m_max")]
			public double[]? Temperature2mMax { get; set; }

			[JsonPropertyName("temperature_2m_min")]
			public double[]? Temperature2mMin { get; set; }

			[JsonPropertyName("precipitation_sum")]
			public double[]? PrecipitationSum { get; set; }
		}

	}
}
