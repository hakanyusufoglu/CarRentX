using Azure;
using CarRentX.DTO.User;
using CarRentX.Entity.Concrete;
using CarRentX.Manager.Abstact;
using CarRentX.Mapping.Abstract;
using CarRentX.Utility.BaseResponse;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;

namespace CarRentX.Manager.Concrete
{
	public class UserManager : IUserManager
	{
		private readonly UserManager<UserApp> _userManager;
		private readonly IConfiguration _config;
		private readonly IMapping _mapper;
		public UserManager(UserManager<UserApp> userManager, IConfiguration config, IMapping mapper)
		{
			_userManager = userManager;
			_config = config;
			_mapper = mapper;
		}
		public async Task<BaseResponse<UserAppDto>> CreateUserAsync(RegisterDto registerDto)
		{
			//yeni kullanıcı kaydolucak 
			var user = new UserApp
			{
				FirstName= registerDto.FirstName,
				LastName= registerDto.LastName,
				Email = registerDto.Email,
				UserName = registerDto.UserName
			};
			//passwordü hashle ve hashlenmiş değerler 
			var result = await _userManager.CreateAsync(user, registerDto.Password);
			if (!result.Succeeded)
			{
				return BaseResponse<UserAppDto>.Error(_config.GetSection("StaticMessages").GetSection("User").GetSection("Get").GetSection("Warning").Value ?? string.Empty);
			}
			return BaseResponse<UserAppDto>.Success(_mapper.Map<UserApp,UserAppDto>(user), _config.GetSection("StaticMessages").GetSection("User").GetSection("Get").GetSection("Success").Value ?? string.Empty);
		}

		public async Task<BaseResponse<UserAppDto>> GetUserByNameAsync(string userName)
		{
			var user = await _userManager.FindByNameAsync(userName);
			if (user == null)
			{
				return BaseResponse<UserAppDto>.Error(_config.GetSection("StaticMessages").GetSection("User").GetSection("Get").GetSection("Warning").Value ?? string.Empty);
			}
			return BaseResponse<UserAppDto>.Success(_mapper.Map<UserApp, UserAppDto>(user), _config.GetSection("StaticMessages").GetSection("User").GetSection("Get").GetSection("Success").Value ?? string.Empty);
		}
	}
}
