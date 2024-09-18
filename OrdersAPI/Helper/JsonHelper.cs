using Newtonsoft.Json;
using OrdersAPI.Models;

namespace OrdersAPI.Helper
{
	public static class JsonHelper
	{
		private static readonly string ordersFilePath = Path.Combine(Directory.GetCurrentDirectory(), "orders.json");

		public static List<Order> ReadFromOrdersJson()
		{
			try
			{
				using (StreamReader r = new StreamReader(ordersFilePath))
				{
					string ordersJson = r.ReadToEnd();
					JsonSerializerSettings settings = new JsonSerializerSettings
					{
						DateFormatHandling = DateFormatHandling.IsoDateFormat,
						DateTimeZoneHandling = DateTimeZoneHandling.Utc,
						DateParseHandling = DateParseHandling.DateTime
					};					
					var items = JsonConvert.DeserializeObject<ListOrders>(ordersJson, settings);
					return items?.Orders;
				}
			}
			catch {
				//Log to error
				return null;			
			}
			
		}

		public static bool WriteToJsonFile(Order data)
		{	
			try
			{
				var orders = new List<Order>();
				var items = new ListOrders();
				using (var sr = new StreamReader(ordersFilePath))
				{
					var ordersJson = sr.ReadToEnd();
					items = JsonConvert.DeserializeObject<ListOrders>(ordersJson);
					orders = items?.Orders;
				}
				if(orders.Count > 0)
				{
					orders.Add(data);
					JsonConvert.SerializeObject(orders, Formatting.Indented);
					using (var sw = new StreamWriter(ordersFilePath))
					{
						sw.Write(JsonConvert.SerializeObject(items));
					}
					return true;
				}

				return false;
			}
			catch(Exception ex)
			{
				return false;
			}		

		}
	}
}
