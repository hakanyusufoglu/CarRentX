using CarRentX.BaseRepository.Concrete;
using CarRentX.ContextDb;
using CarRentX.Entity.Concrete;
using CarRentX.Repository.Abstact;

namespace CarRentX.Repository.Concrete
{
	public class CustomerReadRepository : ReadRepository<Customer, RentCarXEfDbContext, int>, ICustomerReadRepository
	{
		public CustomerReadRepository(RentCarXEfDbContext context) : base(context)
		{
		}
	}
}
