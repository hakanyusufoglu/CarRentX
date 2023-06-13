using CarRentX.BaseRepository.Concrete;
using CarRentX.ContextDb;
using CarRentX.Repository.Abstact;

namespace CarRentX.Repository.Concrete
{
	public class ColorWriteRepository : WriteRepository<Entity.Concrete.Color, RentCarXEfDbContext, int>, IColorWriteRepository
	{
		public ColorWriteRepository(RentCarXEfDbContext context) : base(context)
		{
		}
	}
}
