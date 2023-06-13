using CarRentX.BaseRepository.Concrete;
using CarRentX.ContextDb;
using CarRentX.Repository.Abstact;

namespace CarRentX.Repository.Concrete
{
	public class BrandWriteRepository : WriteRepository<Entity.Concrete.Brand, RentCarXEfDbContext, int>, IBrandWriteRepository
	{
		public BrandWriteRepository(RentCarXEfDbContext context) : base(context)
		{
		}
	}
}
