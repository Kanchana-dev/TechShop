namespace AuthenticationAPI.Models
{
	public class LoginModel
	{
		public Guid UserId { get; set; } = Guid.Empty;
		public string UserName { get; set; }
		public string Password { get; set; }

		public string jwtToken { get; set; } = "";
	}

	public class ListUsers()
	{
		public List<LoginModel> Users { get; set; } = new List<LoginModel>();
	}
}
