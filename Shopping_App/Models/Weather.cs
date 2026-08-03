namespace Shopping_App.Models
{
	public class Weather
	{
		public string? ProvidedDate { get; set; }
		public double? MaxTemp { get; set; }
		public double? MinTemp { get; set; }
		public double? Precipitation { get; set; }
		public string? ErrorMessage { get; set; }
	}
}
