using CarRentX.BaseEntity;

namespace CarRentX.Entity
{
	public class Car:BaseEntity<int>
	{
		public int BrandId { get; set; }
		public string ColorId { get; set; }
		public DateTime ModelYear { get; set; }
		public string Description { get; set; }
	}
}
