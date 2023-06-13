using CarRentX.Utility.BaseResponse;
using CarRentX.ViewModel;

namespace CarRentX.Manager.Abstact
{
	public interface IBrandManager
	{
		BaseResponse<IEnumerable<BrandViewModel>> GetAll();
		Task<BaseResponse<BrandViewModel>> GetByIdAsync(int id);
		Task<BaseResponse<int>> AddAsync(BrandViewModel brandViewModel);
		Task<BaseResponse<bool>> RemoveAsync(int id);
		BaseResponse<bool> Update(BrandViewModel brandViewModel);
		BaseResponse<bool> Remove(BrandViewModel brandViewModel);
	}
}
