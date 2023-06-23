using CarRentX.Manager.Abstact;
using CarRentX.ViewModel.User;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CarRentX.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AuthController : ControllerBase
	{
		private readonly IAuthManager _authManager;

		public AuthController(IAuthManager authManager)
		{
			_authManager = authManager;
		}

		[HttpPost]
		public async Task<IActionResult> CreateToken(LoginViewModel loginViewModel)
		{
			var result = await _authManager.CreateTokenAsync(loginViewModel);
			return Ok(result);
		}	
	}
}
