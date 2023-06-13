using CarRentX.DTO.Car;
using CarRentX.DTO.Customer;

namespace CarRentX.Service.Abstract
{
	public interface ICustomerService
	{
		IEnumerable<CustomerDto> GetAll();
		Task<CustomerDto> GetByIdAsync(int id);
		Task<int> AddAsync(CustomerDto customerDto);
		Task<bool> RemoveAsync(int id);
		bool Update(CustomerDto customerDto);
		bool Remove(CustomerDto customerDto);
	}
}
