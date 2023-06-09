using CarRentX.DTO.Car;

namespace CarRentX.Service.Abstract
{
	public interface ICarService
	{
		IEnumerable<CarDto> GetAll();
		Task<CarDto> GetByIdAsync(int id);
		Task<int> AddAsync(CarDto carDto);
		Task<bool> RemoveAsync(int id);
		bool Update(CarDto carDto);
		bool Remove(CarDto carDto);
	}
}
