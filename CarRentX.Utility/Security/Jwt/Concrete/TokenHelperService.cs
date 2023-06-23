using CarRentX.Entity.Concrete;
using CarRentX.Utility.Security.Encryption;
using CarRentX.Utility.Security.Jwt.Abstract;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;

namespace CarRentX.Utility.Security.Jwt.Concrete
{
	public class TokenHelperService : ITokenHelperService
	{
		public IConfiguration Configuration { get; }
		private TokenOptions _tokenOption;
		private DateTime _accessTokenExpiration;
		public TokenHelperService(IConfiguration configuration)
		{
			Configuration = configuration;
			_tokenOption = Configuration.GetSection("TokenOptions").Get<TokenOptions>();
		}
		private string CreateRefreshToken()//random string değer üretir ve unique.
		{
			var numberByte = new Byte[32]; //32 bytelik random string üreteceğiz
			using var rnd = RandomNumberGenerator.Create(); //using{} te olabilir
			rnd.GetBytes(numberByte); //byte alırken yukarıdaki numberbyte aktar diyorum
			return Convert.ToBase64String(numberByte);
		}
		//tokenda ki payloadları eklicez. Bu metot login barındıran uygulama için...
		private IEnumerable<Claim> GetClaims(UserApp userApp, List<String> audiences) //kullanıcı bilgileri ve bu token hangi apilere karşılık geleceğini alıyorum.
		{
			//tokenda kullanıcı ile ilgili gerekli bilgileri tutuyorum
			var userList = new List<Claim> { new Claim(
                   //type = key, 
                   ClaimTypes.NameIdentifier, //id yani bir kimlik
                   userApp.Id   //kullanıcının idsini tut 
                ),
			new Claim(
				JwtRegisteredClaimNames.Email,  //yada ClaimTypes.Email 
                userApp.Email
				),
			new Claim (ClaimTypes.Name,userApp.UserName),
			new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()) //her tokena random kimlik veriyoruz zorunlu değil ancak BestPractice için
            
            };
			userList.AddRange(audiences.Select(x => new Claim(JwtRegisteredClaimNames.Aud, x)));
			return userList;
		}
		public AccessToken CreateToken(UserApp userApp)
		{
			var accessTokenExpiration = DateTime.Now.AddMinutes(_tokenOption.AccessTokenExpiration);
			var refreshTokenExpiration = DateTime.Now.AddMinutes(_tokenOption.RefreshTokenExpiration);
			var securityKey = SecurityKeyHelper.GetSymmetricSecurityKey(_tokenOption.SecurityKey);

			SigningCredentials signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);//imzayı buradan oluşturuyoruz

			JwtSecurityToken jwtSecurityToken = new JwtSecurityToken(
				issuer: _tokenOption.Issuer, //bu tokeni yayınlayan kim?
				expires: accessTokenExpiration, //ne kadar süre çalışacak bu token,
				notBefore: DateTime.Now,//benim verdiğim saatten itibaren çalışsın 
				claims: GetClaims(userApp, _tokenOption.Audience),
				signingCredentials: signingCredentials
				);

			var handler = new JwtSecurityTokenHandler(); //burası bir token oluşturacak
			var token = handler.WriteToken(jwtSecurityToken); //token oluşturduk.
															  //tokeni tokendtoya çevirdil
			var accessToken = new  AccessToken
			{
				Token = token,
				RefreshToken = CreateRefreshToken(),
				AccessTokenExpriration = accessTokenExpiration,
				RefreshTokenExpriration = refreshTokenExpiration
			};
			return accessToken;
		}
	}
}
