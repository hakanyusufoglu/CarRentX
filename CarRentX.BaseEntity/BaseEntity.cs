namespace CarRentX.BaseEntity
{
	public class BaseEntity<T>
	{
		public T Id { get; set; }
		public bool IsDeleted { get; set; }
		public DateTime CreatedDateTime { get; set; }	
	}
}
