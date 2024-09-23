using Microsoft.AspNetCore.Components;
using Newtonsoft.Json;
using Shopping_App.Models;
using Shopping_App.Storage;
using System.Net.Http.Json;

namespace Shopping_App.Services
{
	public class AuthService : IAuthService
	{
		private IHttpClientFactory _httpFactory;
		private ILogger<AuthService> _logger;
		private NavigationManager _navigationManager;
		private LocalStorage _localStorage;
		public string token { get; private set; }
		public LoginModel LoggedUser { get; private set; }

		public AuthService(IHttpClientFactory factory, ILogger<AuthService> logger,
			NavigationManager navigationManager, LocalStorage localStorage)
		{
			_httpFactory = factory;
			_logger = logger;
			_navigationManager = navigationManager;
			_localStorage = localStorage;
		}
		public async Task Initialize()
		{
			token = await _localStorage.GetItem<string>("token");
			LoggedUser = await _localStorage.GetItem<LoginModel>("user");
		}
		public async Task Login(LoginModel model)
		{
			try
			{
				var response = await _httpFactory.CreateClient("authService").PostAsync("api/authentication/login", JsonContent.Create(model));
				if (!response.IsSuccessStatusCode)
					throw new UnauthorizedAccessException("Login failed.");
				var result = await response.Content.ReadAsStringAsync();

				var _loginModel = JsonConvert.DeserializeObject<LoginModel>(result);

				if (_loginModel != null)
				{
					token = _loginModel.jwtToken;
					_localStorage.SetItem("token", token);
					LoggedUser = _loginModel;
					await _localStorage.SetItem("user", _loginModel);
				}
				else
				{
					throw new UnauthorizedAccessException("Login failed.");
				}
			}
			catch (Exception ex)
			{
				_logger.LogError($"{ex.Message}{Environment.NewLine}{ex.InnerException} {Environment.NewLine}{ex.StackTrace}");
			}
		}

		public async Task Logout()
		{
			await _localStorage.RemoveItem("token");
			_navigationManager.NavigateTo("/login", forceLoad: true);			
		}
	}
}
