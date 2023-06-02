using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRentX.Entity
{
	public class Car
	{
		public int Id { get; set; }
		public int BrandId { get; set; }
		public string ColorId { get; set; }
		public DateTime ModelYear { get; set; }
		public string Description { get; set; }
	}
}
