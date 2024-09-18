using Shopping_App.Models;
using System.Net.Http.Json;

namespace Shopping_App.Services
{
	public class ProductsService
	{
		private IHttpClientFactory _httpFactory;
		public ProductsService(IHttpClientFactory httpFactory, ILogger<OrdersService> logger)
		{
			_httpFactory = httpFactory;
		}

		public async Task<List<Product>> GetAllProducts()
		{
			var _httpClient = _httpFactory.CreateClient("productsService");

			var result = await _httpClient.GetFromJsonAsync<List<Product>>($"api/products");
			return result!;
		}

		public async Task<List<int>> GetQuantity()
		{
			var _httpClient = _httpFactory.CreateClient("productsService");

			var result = await _httpClient.GetFromJsonAsync<List<int>>($"api/products/quantity");
			return result!;
		}
	}
}
