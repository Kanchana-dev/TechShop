using Shopping_App.Models;
using System.Net.Http.Json;

namespace Shopping_App.Services
{
	public class OrdersService : IOrdersService
	{			
		private IHttpClientFactory _httpFactory;
		private ILogger<OrdersService> _logger;
		public OrdersService(IHttpClientFactory httpFactory, ILogger<OrdersService> logger)
		{
			_httpFactory = httpFactory;		
			_logger = logger;
		}

		public async Task<List<Order>> GetAllOrdersByUser(int userId)
		{			
			var _httpClient = _httpFactory.CreateClient("ordersService");

			var result = await _httpClient.GetFromJsonAsync<List<Order>>($"api/orders/getallorders/{userId}");
			return result;
		}

		public async Task<bool> AddOrder(Order order)
		{
			try
			{
				var _httpClient = _httpFactory.CreateClient("ordersService");

				var result = await _httpClient.PostAsJsonAsync<Order>($"api/orders/addorder", order);
				result.EnsureSuccessStatusCode();

				return true;
			}
			catch(Exception ex)
			{
				_logger.LogError(ex.Message, ex.InnerException, ex.StackTrace);
				return false;
			}
		}

	}


	}
