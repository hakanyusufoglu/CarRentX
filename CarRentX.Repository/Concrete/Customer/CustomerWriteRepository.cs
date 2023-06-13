using CarRentX.BaseRepository.Concrete;
using CarRentX.ContextDb;
using CarRentX.Entity.Concrete;
using CarRentX.Repository.Abstact;

namespace CarRentX.Repository.Concrete
{
	public class CustomerWriteRepository : WriteRepository<Customer, RentCarXEfDbContext, int>, ICustomerWriteRepository
	{
		public CustomerWriteRepository(RentCarXEfDbContext context) : base(context)
		{
		}
	}
}
