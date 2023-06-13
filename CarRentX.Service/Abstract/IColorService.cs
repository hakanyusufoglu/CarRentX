using CarRentX.DTO.Color;

namespace CarRentX.Service.Abstract
{
	public interface IColorService
	{
		IEnumerable<ColorDto> GetAll();
		Task<ColorDto> GetByIdAsync(int id);
		Task<int> AddAsync(ColorDto colorDto);
		Task<bool> RemoveAsync(int id);
		bool Update(ColorDto colorDto);
		bool Remove(ColorDto colorDto);
	}
}
