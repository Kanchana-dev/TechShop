using System.Globalization;

namespace WeatherAPI.Helper
{
	public static class WeatherHelper
	{
		private static readonly string datesFilePath = Path.Combine(Directory.GetCurrentDirectory(), "dates.txt");

		public static IEnumerable<string> ReadFromDatesFile(ILogger logger)
		{
			try
			{
				if (!File.Exists(datesFilePath))
				{
					logger.LogError("WeatherHelper - dates.txt is not found");					
					return [];
				}

				return File.ReadLines(datesFilePath).Select(x => x.Trim()).Where(y => y.Length > 0);
			}		
			catch (Exception ex)
			{
				logger.LogError($"{ex.Message}{Environment.NewLine}{ex.InnerException} {Environment.NewLine}{ex.StackTrace}");
				return null;
			}
		}

		public static (string? parsedDate, string? error) GetISODate(string origDate)
		{

			if (DateTime.TryParse(origDate.Trim(), CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime result))
			{
				return (result.ToString("yyyy-MM-dd"), null);
			}

			return (null, $"Invalid date: {origDate}");
		}
	}
}
