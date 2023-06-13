using CarRentX.BaseRepository.Concrete;
using CarRentX.ContextDb;
using CarRentX.Repository.Abstact;

namespace CarRentX.Repository.Concrete
{
	public class RentalReadRepository : ReadRepository<Entity.Concrete.Rental, RentCarXEfDbContext, int>, IRentalReadRepository
	{
		public RentalReadRepository(RentCarXEfDbContext context) : base(context)
		{
		}
	}
}
