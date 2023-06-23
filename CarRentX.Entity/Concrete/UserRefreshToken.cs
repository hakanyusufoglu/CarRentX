using CarRentX.BaseEntity;

namespace CarRentX.Entity.Concrete
{
	public class UserRefreshToken:BaseEntity<int>
	{
		public string UserId { get; set; } //bu refresh token kime ait olacak
		public string Code { get; set; }//refreshtoken anlamında
		public DateTime Expiration { get; set; }
	}
}
