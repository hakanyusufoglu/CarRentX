using CarRentX.DTO.User;
using CarRentX.Manager.Abstact;
using CarRentX.Mapping.Abstract;
using CarRentX.Service.Abstract;
using CarRentX.Utility.BaseResponse;
using CarRentX.Utility.Security.Jwt.Concrete;
using CarRentX.ViewModel.Car;
using CarRentX.ViewModel.User;
using Microsoft.Extensions.Configuration;

namespace CarRentX.Manager.Concrete
{
	public class AuthManager : IAuthManager
	{
		private readonly IAuthService _authService;
		private readonly IConfiguration _config;
		private readonly IMapping _mapper;
		public AuthManager(IAuthService authService, IConfiguration config, IMapping mapper)
		{
			_authService = authService;
			_config = config;
			_mapper = mapper;
		}

		public async Task<BaseResponse<AccessToken>> CreateTokenAsync(LoginViewModel loginViewModel)
		{
			try
			{
				var result = await _authService.CreateTokenAsync(_mapper.Map<LoginViewModel, LoginDto>(loginViewModel));
				if (result == null)
				{
					return BaseResponse<AccessToken>.Error(_config.GetSection("StaticMessages").GetSection("AccessToken").GetSection("Get").GetSection("Warning").Value ?? string.Empty);
				}
				return BaseResponse<AccessToken>.Success(result, _config.GetSection("StaticMessages").GetSection("AccessToken").GetSection("Get").GetSection("Success").Value ?? string.Empty);

			}
			catch (Exception ex)
			{
				if (ex.InnerException != null)
				{
					string errorMessage = $"Inner Exception Message: {ex.InnerException.Message}, Exception Message: {ex.Message}";
					return BaseResponse<AccessToken>.Error(errorMessage);
				}
				return BaseResponse<AccessToken>.Error(ex.Message);
			}
		}

		public async Task<BaseResponse<AccessToken>> CreateTokenByRefreshToken(string refreshToken)
		{
			try
			{
				var result = await _authService.CreateTokenByRefreshTokenAsync(refreshToken);
				if (result == null)
				{
					return BaseResponse<AccessToken>.Error(_config.GetSection("StaticMessages").GetSection("AccessToken").GetSection("Get").GetSection("Warning").Value ?? string.Empty);
				}
				return BaseResponse<AccessToken>.Success(result, _config.GetSection("StaticMessages").GetSection("AccessToken").GetSection("Get").GetSection("Success").Value ?? string.Empty);

			}
			catch (Exception ex)
			{
				if (ex.InnerException != null)
				{
					string errorMessage = $"Inner Exception Message: {ex.InnerException.Message}, Exception Message: {ex.Message}";
					return BaseResponse<AccessToken>.Error(errorMessage);
				}
				return BaseResponse<AccessToken>.Error(ex.Message);
			}
		}

		public Task RevokeRefreshToken(string refreshToken)
		{
			throw new NotImplementedException();
		}
	}
}
