using AuthenticationAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AuthenticationAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AuthenticationController : ControllerBase
	{
		private readonly IConfiguration _configuration;
		private readonly ILogger<AuthenticationController> _logger;
		public AuthenticationController(IConfiguration configuration, ILogger<AuthenticationController> logger)
		{
			_configuration = configuration;
			_logger = logger;
		}

		[HttpPost("Login")]		
		public IActionResult Login([FromBody] LoginModel model)
		{
			_logger.LogInformation("Login called");
			var isValidUser = IsUserValid(model.UserName);
		
			if (isValidUser==null)
				return Unauthorized();
			JwtSecurityToken token = GenerateToken(model.UserName);
			var _token = new JwtSecurityTokenHandler().WriteToken(token);
			isValidUser.jwtToken = _token;
			return Ok(isValidUser);			
		}

		private JwtSecurityToken GenerateToken(string username)
		{
			try
			{
				var claims = new List<Claim>
			{
					new Claim(ClaimTypes.Name, username),
					new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
			};

				var jwtToken = new JwtSecurityToken(
					claims: claims,
					notBefore: DateTime.UtcNow,
					expires: DateTime.UtcNow.AddDays(30),
					signingCredentials: new SigningCredentials(
						new SymmetricSecurityKey(
						   Encoding.UTF8.GetBytes(_configuration["JwtSettings:SecretKey"] ?? "")
							),
						SecurityAlgorithms.HmacSha256Signature)
					);
				
				return jwtToken;
			}
			catch (Exception ex)
			{
				return null;
			}
		}


		private LoginModel IsUserValid(string username)
		{
			try
			{
				var ordersFilePath = Path.Combine(Directory.GetCurrentDirectory(), "users.json");

				using (StreamReader r = new StreamReader(ordersFilePath))
				{
					string usersJson = r.ReadToEnd();				
					var items = JsonConvert.DeserializeObject<ListUsers>(usersJson);
					var _user = items.Users.Where(x => x.UserName == username).FirstOrDefault();
				
					if (_user != null )
					{
						return _user;						
					}
					else
						return null;						
				}

			}
			catch (Exception ex)
			{
				return null;				
			}
		}
	}
}
