using CarRentX.BaseRepository.Abstract;

namespace CarRentX.Repository.Abstact
{
	public interface ICustomerReadRepository:IReadRepository<Entity.Concrete.Customer,int>
	{
	}
}
