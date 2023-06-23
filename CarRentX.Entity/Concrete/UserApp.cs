using Microsoft.AspNetCore.Identity;

namespace CarRentX.Entity.Concrete
{
	public class UserApp:IdentityUser
	{
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public DateTime CreatedDateTime { get; set; } = DateTime.Now;
		public DateTime? LastModifiedDate { get; set; }
		public virtual ICollection<Customer> Customers { get; set; }
	}
}
