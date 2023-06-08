using CarRentX.DTO.Car;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRentX.Service.Abstract
{
	public interface ICarService
	{
		Task<IEnumerable<CarDto>> GetAllAsync();
		Task<CarDto> GetByIdAsync(int id);
		Task<int> AddAsync(CarDto carDto);
		Task<bool> RemoveAsync(int id);
		int Update(CarDto carDto);
		bool Remove(CarDto carDto);
	}
}
