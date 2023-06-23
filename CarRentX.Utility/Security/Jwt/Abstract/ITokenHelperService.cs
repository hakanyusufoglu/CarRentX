using CarRentX.Entity.Concrete;
using CarRentX.Utility.Security.Jwt.Concrete;

namespace CarRentX.Utility.Security.Jwt.Abstract
{
	public interface ITokenHelperService
	{
		AccessToken CreateToken(UserApp userApp);
	}
}
