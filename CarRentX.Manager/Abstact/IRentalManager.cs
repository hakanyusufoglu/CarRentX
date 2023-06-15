using CarRentX.Utility.BaseResponse;
using CarRentX.ViewModel.Rental;

namespace CarRentX.Manager.Abstact
{
	public interface IRentalManager
	{
		BaseResponse<IEnumerable<RentalViewModel>> GetAll();
		Task<BaseResponse<int>> AddAsync(RentalViewModel rentalViewModel);
	}
}
