using CarRentX.BaseRepository.Concrete;
using CarRentX.ContextDb;
using CarRentX.Entity.Concrete;
using CarRentX.Repository.Abstact;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRentX.Repository.Concrete
{
	public class ColorReadRepository : ReadRepository<Color, RentCarXEfDbContext, int>, IColorReadRepository
	{
		public ColorReadRepository(RentCarXEfDbContext context) : base(context)
		{
		}
	}
}
