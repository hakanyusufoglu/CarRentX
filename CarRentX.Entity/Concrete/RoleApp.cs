using Microsoft.AspNetCore.Identity;

namespace CarRentX.Entity.Concrete
{
	public class RoleApp:IdentityRole
	{
		public DateTime CreatedDateTime { get; set; } = DateTime.Now;
		public DateTime? LastModifiedDate { get; set; }
	}
}
