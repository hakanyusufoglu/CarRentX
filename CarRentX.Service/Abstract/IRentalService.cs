using CarRentX.DTO.Customer;
using CarRentX.DTO.Rental;

namespace CarRentX.Service.Abstract
{
	public interface IRentalService
	{
		IEnumerable<RentalDto> GetAll();
		Task<int> AddAsync(RentalDto rentalDto);

	}
}
