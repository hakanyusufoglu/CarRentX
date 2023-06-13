using CarRentX.BaseEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRentX.Entity.Concrete
{
	public class Rental:BaseEntity<int>
	{
		public DateTime RentDate { get; set; }
		public DateTime ReturnDate { get; set; }
		public virtual Customer Customer { get; set; }
	}
}
