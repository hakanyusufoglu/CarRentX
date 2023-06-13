using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRentX.DTO.Brand
{
	public class BrandDto
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public bool IsDeleted { get; set; }
		public DateTime CreatedDateTime { get; set; }
	}
}
