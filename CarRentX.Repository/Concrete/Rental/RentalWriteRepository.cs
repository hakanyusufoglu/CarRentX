using CarRentX.BaseRepository.Concrete;
using CarRentX.ContextDb;
using CarRentX.Repository.Abstact;

namespace CarRentX.Repository.Concrete
{
	public class RentalWriteRepository : WriteRepository<Entity.Concrete.Rental, RentCarXEfDbContext, int>, IRentalWriteRepository
	{
		public RentalWriteRepository(RentCarXEfDbContext context) : base(context)
		{
		}
	}
}
