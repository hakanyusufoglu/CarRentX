using CarRentX.DTO.User;
using CarRentX.Utility.BaseResponse;
using CarRentX.Utility.Security.Jwt.Concrete;
using CarRentX.ViewModel.User;

namespace CarRentX.Manager.Abstact
{
	public interface IAuthManager
	{
		Task<BaseResponse<AccessToken>> CreateTokenAsync(LoginViewModel loginViewModel);
		Task<BaseResponse<AccessToken>> CreateTokenByRefreshToken(string refreshToken); 
		Task RevokeRefreshToken(string refreshToken); 
	}
}
