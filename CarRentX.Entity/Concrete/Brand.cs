namespace CarRentX.Entity.Concrete
{
	public class Brand
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public virtual ICollection<Car> Cars { get; set; }
	}
}
