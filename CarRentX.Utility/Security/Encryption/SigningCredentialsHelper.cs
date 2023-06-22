using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace CarRentX.Utility.Security.Encryption
{
	public class SigningCredentialsHelper
	{
		public static SigningCredentials CreateSigningCredentials(SecurityKey securityKey)
		{
			return new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha512Signature);
		}
	}
}
