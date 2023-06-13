using CarRentX.Utility.BaseResponse;
using CarRentX.ViewModel;

namespace CarRentX.Manager.Abstact
{
	public interface IColorManager
	{
		BaseResponse<IEnumerable<ColorViewModel>> GetAll();
		Task<BaseResponse<ColorViewModel>> GetByIdAsync(int id);
		Task<BaseResponse<int>> AddAsync(ColorViewModel colorViewModel);
		Task<BaseResponse<bool>> RemoveAsync(int id);
		BaseResponse<bool> Update(ColorViewModel colorViewModel);
		BaseResponse<bool> Remove(ColorViewModel colorViewModel);
	}
}
