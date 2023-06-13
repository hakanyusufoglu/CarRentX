using CarRentX.BaseRepository.Abstract;

namespace CarRentX.Repository.Abstact
{
	public interface IRentalReadRepository:IReadRepository<Entity.Concrete.Rental,int>
	{
	}
}
