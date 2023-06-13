using CarRentX.BaseRepository.Concrete;
using CarRentX.ContextDb;
using CarRentX.Repository.Abstact;

namespace CarRentX.Repository.Concrete
{
	public class BrandReadRepository : ReadRepository<Entity.Concrete.Brand, RentCarXEfDbContext, int>, IBrandReadRepository
	{
		public BrandReadRepository(RentCarXEfDbContext context) : base(context)
		{
		}
	}
}
