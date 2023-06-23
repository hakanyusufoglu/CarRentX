using Azure;
using CarRentX.DTO.User;
using CarRentX.Utility.BaseResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRentX.Manager.Abstact
{
	public interface IUserManager
	{
		Task<BaseResponse<UserAppDto>>  CreateUserAsync(RegisterDto registerDto);

		//username göre veritabanından kullanıcı bulalım
		Task<BaseResponse<UserAppDto>> GetUserByNameAsync(string userName);
	}
}
