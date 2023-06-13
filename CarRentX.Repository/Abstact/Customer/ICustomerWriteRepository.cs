using CarRentX.BaseRepository.Abstract;
using CarRentX.Entity.Concrete;

namespace CarRentX.Repository.Abstact
{
	public interface ICustomerWriteRepository:IWriteRepository<Customer,int>
	{
	}
}
