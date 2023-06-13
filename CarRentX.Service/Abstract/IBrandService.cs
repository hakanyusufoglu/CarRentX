using CarRentX.DTO.Brand;
using CarRentX.DTO.Car;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRentX.Service.Abstract
{
	public interface IBrandService
	{
		IEnumerable<BrandDto> GetAll();
		Task<BrandDto> GetByIdAsync(int id);
		Task<int> AddAsync(BrandDto brandDto);
		Task<bool> RemoveAsync(int id);
		bool Update(BrandDto brandDto);
		bool Remove(BrandDto brandDto);
	}
}
