using CarRentX.DTO.Car;
using CarRentX.Utility.BaseResponse;
using CarRentX.ViewModel.Car;

namespace CarRentX.Manager.Abstact
{
	public interface ICarManager
	{
		BaseResponse<IEnumerable<CarViewModel>> GetAll();
		Task<BaseResponse<CarViewModel>> GetByIdAsync(int id);
		Task<BaseResponse<int>> AddAsync(CarViewModel carViewModel);
		Task<BaseResponse<bool>> RemoveAsync(int id);
		BaseResponse<bool> Update(CarViewModel carViewModel);
		BaseResponse<bool> Remove(CarViewModel carViewModel);
		Task<BaseResponse<IEnumerable<CarViewModel>>> GetAllWithBrandColorAsync();
	}
}
