using CarRentX.DTO.User;
using CarRentX.Manager.Abstact;
using CarRentX.Mapping.Abstract;
using CarRentX.ViewModel.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CarRentX.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class UsersController : ControllerBase
	{
		private readonly IUserManager _userManager;
		private readonly IMapping _mapper;
		public UsersController(IUserManager userManager, IMapping mapper)
		{
			_userManager = userManager;
			_mapper = mapper;
		}
		//api/user
		[HttpPost]
		public async Task<IActionResult> CreateUser(RegisterViewModel registerViewModel)
		{
			return Ok(await _userManager.CreateUserAsync(_mapper.Map<RegisterViewModel,RegisterDto>(registerViewModel)));
		}
		[Authorize] //bu endpoint mutlaka token istiyor
		[HttpGet]
		public async Task<IActionResult> GetUser()
		{ //ToDo usercontroller ve authcontroller içerisindeki Ok metotları iyileştirilmelidir.
			return Ok(await _userManager.GetUserByNameAsync(HttpContext.User.Identity.Name)); //namei payloaddan çıkarıyor
		}
	}
}
