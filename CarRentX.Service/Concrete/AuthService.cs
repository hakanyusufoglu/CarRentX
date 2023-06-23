using Azure;
using CarRentX.DTO.Token;
using CarRentX.DTO.User;
using CarRentX.Entity.Concrete;
using CarRentX.Service.Abstract;
using CarRentX.UnitOfWork.Abstract;
using CarRentX.Utility.Security.Jwt.Abstract;
using CarRentX.Utility.Security.Jwt.Concrete;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRentX.Service.Concrete
{
	public class AuthService : IAuthService
	{
		private readonly ITokenHelperService _tokenHelperService;
		private readonly UserManager<UserApp> _userManager;
		private readonly IUnitOfWork _unitOfWork;
		private readonly IConfiguration _config;

		public AuthService(ITokenHelperService tokenHelperService, UserManager<UserApp> userManager, IUnitOfWork unitOfWork, IConfiguration config)
		{
			_tokenHelperService = tokenHelperService;
			_userManager = userManager;
			_unitOfWork = unitOfWork;
			_config = config;
		}

		public async Task<AccessToken> CreateTokenAsync(LoginDto loginDto)
		{
			await _unitOfWork.BeginTransactionAsync();
			try
			{
				//Her işlemden sonra ifle kontrol ediyoruz buna savunmacı yaklaşımı adı verilmektedir.

				if (loginDto == null) throw new ArgumentNullException(nameof(loginDto));//logindto hakkında hata fırlat
				var user = await _userManager.FindByEmailAsync(loginDto.Email);
				if (user == null) //email yok
				{
					//return Response<AccessTokenDto>.Fail("Email or password is wrong", 400, true);//email veya şifra yanlış dedik ki kötü kullanıcı bunu anlamasın client taraflı hata olduğunda 400 diyoruz
				}
				if (!await _userManager.CheckPasswordAsync(user, loginDto.Password))//paswordu kontrol ediyoruz
				{
					//return Response<TokenDto>.Fail("Email or password is wrong", 400, true);
				}
				var token = _tokenHelperService.CreateToken(user);
				var userRefreshToken = _unitOfWork.UserRefreshTokenReadRepository.Where(x => x.UserId == user.Id).SingleOrDefault(); //veritabanında daha önce refresh token var mı?
				if (userRefreshToken == null) //yoksa
				{
					await _unitOfWork.UserRefreshTokenWriteRepository.AddAsync(new UserRefreshToken
					{
						UserId = user.Id,
						Code = token.RefreshToken,
						Expiration = token.RefreshTokenExpriration
					});
				}
				else //varsa
				{
					userRefreshToken.Code = token.RefreshToken;
					userRefreshToken.Expiration = token.RefreshTokenExpriration;
				}
				return token;
			}
			catch(Exception ex)
			{
				await _unitOfWork.RollbackAsync();
				throw new ApplicationException(_config.GetSection("StaticMessages").GetSection("AccessToken").GetSection("Add").GetSection("Error").Value ?? string.Empty, ex);
			}
		
		}

		public async Task<AccessToken> CreateTokenByRefreshTokenAsync(string refreshToken)
		{
			await _unitOfWork.BeginTransactionAsync();
			try
			{
				var existRefreshToken = _unitOfWork.UserRefreshTokenReadRepository.Where(x => x.Code == refreshToken).SingleOrDefault();
				if (existRefreshToken == null)
				{
					//return Response<TokenDto>.Fail("Refresh token not found", 404, true);
				}
				var user = await _userManager.FindByIdAsync(existRefreshToken.UserId);
				if (user == null)
				{
					//return Response<TokenDto>.Fail("User Id not found", 404, true);
				}
				var token = _tokenHelperService.CreateToken(user);
				existRefreshToken.Code = token.RefreshToken;
				existRefreshToken.Expiration = token.RefreshTokenExpriration;
				//güncelleme
				return token;
			}
			catch(Exception ex)
			{
				await _unitOfWork.RollbackAsync();
				throw new ApplicationException(_config.GetSection("StaticMessages").GetSection("AccessToken").GetSection("Add").GetSection("Error").Value ?? string.Empty, ex);

			}


		}

		public bool RevokeRefreshToken(string refreshToken)
		{
			_unitOfWork.BeginTransaction();
			try
			{
				var existRefreshToken = _unitOfWork.UserRefreshTokenReadRepository.Where(x => x.Code == refreshToken).SingleOrDefault();
				if (existRefreshToken == null)
				{
					//return Response<NoDataDto>.Fail("Refresh token not found", 404, true);
				}
				return _unitOfWork.UserRefreshTokenWriteRepository.Remove(existRefreshToken);
			}
			catch(Exception ex)
			{
				_unitOfWork.Rollback();
				throw new ApplicationException(_config.GetSection("StaticMessages").GetSection("AccessToken").GetSection("Delete").GetSection("Warning").Value ?? string.Empty, ex);
			}
		
		}
	}
}
