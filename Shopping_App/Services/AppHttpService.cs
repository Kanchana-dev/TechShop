using Microsoft.AspNetCore.Components;
using Shopping_App.Storage;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;

namespace Shopping_App.Services
{
	public interface IAppHttpService
	{
		Task<T> Get<T>(string uri, string service);
		Task<T> Post<T>(string uri, object value, string service);
	}

	public class AppHttpService: IAppHttpService
	{
		private IHttpClientFactory _httpFactory;
		private NavigationManager _navigationManager;
		private ILocalStorage _localStorage;
		private IConfiguration _configuration;
		public AppHttpService(IHttpClientFactory httpFactory, NavigationManager navigationManager,
				LocalStorage localStorage, IConfiguration configuration)
		{
			_httpFactory = httpFactory;
			_navigationManager = navigationManager;
			_localStorage = localStorage;
			_configuration = configuration;
		}
	

		public async Task<T> Get<T>(string uri, string service="")
		{
			var request = new HttpRequestMessage(HttpMethod.Get, uri);
			return await sendRequest<T>(request, service);
		}

		public async Task<T> Post<T>(string uri, object value, string service)
		{
			var request = new HttpRequestMessage(HttpMethod.Post, uri);
			request.Content = new StringContent(JsonSerializer.Serialize(value), Encoding.UTF8, "application/json");
			return await sendRequest<T>(request, service);
		}

		
		private async Task<T> sendRequest<T>(HttpRequestMessage request, string service="")
		{			
			var _token = await _localStorage.GetItem<string>("token");
			if (_token != null )
				request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", _token);
			var _httpClient = new HttpClient();

			if (!string.IsNullOrEmpty(service) && service == _configuration["ProductsService"])
			{
				 _httpClient = _httpFactory.CreateClient("productsService");
			}
			if (!string.IsNullOrEmpty(service) && service == _configuration["OrdersService"])
			{
				 _httpClient = _httpFactory.CreateClient("ordersService");
			}

			using var response = await _httpClient.SendAsync(request);
			
			if (response.StatusCode == HttpStatusCode.Unauthorized)
			{
				_navigationManager.NavigateTo("logout");
				return default;
			}

			// throw exception on error response
			if (!response.IsSuccessStatusCode)
			{
				var error = await response.Content.ReadFromJsonAsync<Dictionary<string, string>>();
				throw new Exception(error["message"]);
			}

			return await response.Content.ReadFromJsonAsync<T>();
		}
	}
}
