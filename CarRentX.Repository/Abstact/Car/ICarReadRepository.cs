using CarRentX.BaseRepository.Abstract;
using CarRentX.Entity.Concrete;

namespace CarRentX.Repository.Abstact
{
	public interface ICarReadRepository:IReadRepository<Car,int>
	{
		IEnumerable<Car> GetCarsByColorId(string id);
		IEnumerable<Car> GetCarsByBrandId(int id);
	}
}
