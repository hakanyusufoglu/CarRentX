using Azure;
using CarRentX.DTO.Token;
using CarRentX.DTO.User;
using CarRentX.Utility.Security.Jwt.Concrete;

namespace CarRentX.Service.Abstract
{
	public interface IAuthService
	{
		Task<AccessToken> CreateTokenAsync(LoginDto loginDto);//logindto doğruysa geriye token dönecek.
		Task<AccessToken> CreateTokenByRefreshTokenAsync(string refreshToken); //refresh token ile yeni bir token alacağız.
		bool RevokeRefreshToken(string refreshToken); //eğer bir refresh token varsa onu kaldır. Yani logout yapıyor.
	}
}
