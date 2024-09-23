using Newtonsoft.Json;
using OrdersAPI.Helper;
using OrdersAPI.Models;

namespace OrdersAPI.Repositories
{
	public class OrdersRepository:IOrdersRepository
	{
		private readonly ILogger<OrdersRepository> _logger;

		public OrdersRepository(ILogger<OrdersRepository> logger) { 
			_logger = logger;
		}

		//***********************************************Information***************************************************************
		//DBContext class can be Dependency Injected and used to work with EntityFramework, or to call ADO.NET methods to wotk with 
		//MySQL or other no SQL database context objects like SQLite, MongDB Documents. 
		//For simplicty of the POC, data is saved and retrieved from a JSON file.
		//*************************************************************************************************************************

		public async Task<List<Order>> GetAllOrders(CancellationToken ct=default)
		{
			try
			{
				_logger.LogInformation("Delaying Task to check for cancellation event");
				await Task.Delay(1000, ct);
				
				var allOrders = JsonHelper.ReadFromOrdersJson();
				if (allOrders != null && allOrders.Count() > 0)
				{
					_logger.LogInformation("Sucessfully recevied All Orders.");
					return allOrders;
				}
				else
				{ 
					_logger.LogInformation("Orders returned as Null");
				}

				return null;
			}
			catch (TaskCanceledException cte)
			{
				_logger.LogError($"{cte.Message}{Environment.NewLine}{cte.StackTrace}");
				return null;
			}
			catch (Exception ex)
			{
				_logger.LogError($"{ex.Message}{Environment.NewLine}{ex.InnerException} {Environment.NewLine}{ex.StackTrace}");
				return null;
			}
		}

		public async Task<bool> AddOrder(Order order, CancellationToken ct = default)
		{
			try
			{
				_logger.LogInformation("AddOrder -Delaying Task to check for cancellation event");
				await Task.Delay(1000, ct);
				var isSucess = JsonHelper.WriteToJsonFile(order);
				if (isSucess)
				{
					_logger.LogInformation($"Sucessfully added new order : {JsonConvert.SerializeObject(order)} ");

					return true;
				}
				else { 
					_logger.LogInformation($"UnSucessfully adding new order : {JsonConvert.SerializeObject(order)} ");
				}
				return false;
			}
			catch (TaskCanceledException cte)
			{
				_logger.LogError($"{cte.Message}{Environment.NewLine}{cte.StackTrace} ");
				return false;
			}
			catch (Exception ex)
			{
				_logger.LogError($"Exception adding new order : {JsonConvert.SerializeObject(order)} {Environment.NewLine}{ex.Message} {Environment.NewLine}{ex.StackTrace}  ");
				return false;
			}
		}
	}
}
