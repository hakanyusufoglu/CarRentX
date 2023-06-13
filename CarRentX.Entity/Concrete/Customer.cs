using CarRentX.BaseEntity;

namespace CarRentX.Entity.Concrete
{
	public class Customer:BaseEntity<int>
	{

		public string Address { get;set; }
		public string CompanyName { get; set; }
		public AppUser AppUser { get; set; }
		public virtual ICollection<Rental> Rentals { get; set; }
	}
}
