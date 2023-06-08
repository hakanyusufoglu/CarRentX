using CarRentX.BaseEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRentX.Entity.Concrete
{
	public class Car : BaseEntity<int>
	{
		public int BrandId { get; set; }
		public string ColorId { get; set; }
		public DateTime ModelYear { get; set; }
		public string Description { get; set; }
	}
}