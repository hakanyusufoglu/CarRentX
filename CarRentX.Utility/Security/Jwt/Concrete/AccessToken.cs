﻿namespace CarRentX.Utility.Security.Jwt.Concrete
{
	public class AccessToken
	{
		public string Token { get; set; }
		public DateTime AccessTokenExpriration { get; set; } //tokenin ömrünü tututyoryz zorunlu deği zaten token içinde var
		public string RefreshToken { get; set; }
		public DateTime RefreshTokenExpriration { get; set; }
	}
}