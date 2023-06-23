using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace CarRentX.Utility.Security.Encryption
{
	public class SecurityKeyHelper
	{
		public static SecurityKey GetSymmetricSecurityKey(string securityKey)
		{
			return new SymmetricSecurityKey(Encoding.UTF8.GetBytes(securityKey));
		}
	}
}
