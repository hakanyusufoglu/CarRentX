using CarRentX.BaseRepository.Abstract;
using CarRentX.Entity.Concrete;

namespace CarRentX.Repository.Abstact
{
	public interface ICarReadRepository:IReadRepository<Car,int>
	{
		IEnumerable<Car> GetCarsByColorId(int id);
		IEnumerable<Car> GetCarsByBrandId(int id);
		Task<IEnumerable<Car>> GetAllWithBrandColorAsync();
	}
}
