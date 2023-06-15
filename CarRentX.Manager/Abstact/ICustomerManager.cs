using CarRentX.Utility.BaseResponse;
using CarRentX.ViewModel.Customer;

namespace CarRentX.Manager.Abstact
{
	public interface ICustomerManager
	{
		BaseResponse<IEnumerable<CustomerViewModel>> GetAll();
		Task<BaseResponse<CustomerViewModel>> GetByIdAsync(int id);
		Task<BaseResponse<int>> AddAsync(CustomerViewModel customerViewModel);
		Task<BaseResponse<bool>> RemoveAsync(int id);
		BaseResponse<bool> Update(CustomerViewModel customerViewModel);
		BaseResponse<bool> Remove(CustomerViewModel customerViewModel);
	}
}
