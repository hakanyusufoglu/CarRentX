﻿using CarRentX.BaseEntity;

namespace CarRentX.Entity.Concrete
{
	public class Car : BaseEntity<int>
	{
		public int BrandId { get; set; }
		public int ColorId { get; set; }
		public string Name { get; set; }
		public decimal DailyPrice { get; set; }
		public DateTime ModelYear { get; set; }
		public string Description { get; set; }
		public virtual Color Color { get; set; }
		public virtual Brand Brand { get; set; }
	}
}