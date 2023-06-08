using CarRentX.BaseRepository.Concrete;
using CarRentX.ContextDb;
using CarRentX.Entity.Concrete;
using CarRentX.Repository.Abstact;

namespace CarRentX.Repository.Concrete
{
	public class CarReadRepository : ReadRepository<Car, RentCarXEfDbContext, int>, ICarReadRepository
	{
		public CarReadRepository(RentCarXEfDbContext context) : base(context)
		{
		}
	}
}