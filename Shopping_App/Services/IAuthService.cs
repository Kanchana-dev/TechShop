using Shopping_App.Models;

namespace Shopping_App.Services
{
	public interface IAuthService
	{
		Task Initialize();
		Task Login(LoginModel model);
		Task Logout();
		string token { get; }

		LoginModel LoggedUser { get; }
	}
	
}
