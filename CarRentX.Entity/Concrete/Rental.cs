using CarRentX.BaseEntity;

namespace CarRentX.Entity.Concrete
{
	public class Rental:BaseEntity<int>
	{
		public DateTime RentDate { get; set; }
		public DateTime ReturnDate { get; set; }
		public virtual Customer Customer { get; set; }
	}
}
