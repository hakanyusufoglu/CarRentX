using CarRentX.BaseRepository.Concrete;
using CarRentX.ContextDb;
using CarRentX.Entity.Concrete;
using CarRentX.Repository.Abstact;

namespace CarRentX.Repository.Concrete
{
	public class CarWriteRepository : WriteRepository<Car, RentCarXEfDbContext, int>, ICarWriteRepository
	{
		public CarWriteRepository(RentCarXEfDbContext context) : base(context)
		{
		}
	}
}