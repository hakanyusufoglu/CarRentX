using CarRentX.BaseEntity;

namespace CarRentX.Entity.Concrete
{
	public class Brand:BaseEntity<int>
	{
		public string Name { get; set; }
		public virtual ICollection<Car> Cars { get; set; }
	}
}
