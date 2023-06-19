using CarRentX.DTO.Customer;
using CarRentX.Manager.Abstact;
using CarRentX.Mapping.Abstract;
using CarRentX.Service.Abstract;
using CarRentX.Utility.BaseResponse;
using CarRentX.ViewModel.Customer;
using Microsoft.Extensions.Configuration;

namespace CarRentX.Manager.Concrete
{
	public class CustomerManager : ICustomerManager
	{
		private readonly ICustomerService _customerService;
		private readonly IConfiguration _config;
		private readonly IMapping _mapper;

		public CustomerManager(ICustomerService customerService, IConfiguration config, IMapping mapper)
		{
			_customerService = customerService;
			_config = config;
			_mapper = mapper;
		}

		public async Task<BaseResponse<int>> AddAsync(CustomerViewModel customerViewModel)
		{
			try
			{
				var customerDto = _mapper.Map<CustomerViewModel, CustomerDto>(customerViewModel);
				var result = await _customerService.AddAsync(customerDto);
				if (result <= 0)
				{
					return BaseResponse<int>.Error(_config.GetSection("StaticMessages").GetSection("Customer").GetSection("Add").GetSection("Error").Value ?? string.Empty);
				}
				return BaseResponse<int>.Success(customerDto.Id, _config.GetSection("StaticMessages").GetSection("Customer").GetSection("Add").GetSection("Success").Value ?? string.Empty);
			}
			catch (Exception ex)
			{
				if (ex.InnerException != null)
				{
					string errorMessage = $"Inner Exception Message: {ex.InnerException.Message}, Exception Message: {ex.Message}";
					return BaseResponse<int>.Error(errorMessage);
				}
				return BaseResponse<int>.Error(ex.Message);
			}
		}

		public BaseResponse<IEnumerable<CustomerViewModel>> GetAll()
		{
			try
			{
				var result = _mapper.Map<IEnumerable<CustomerDto>, IEnumerable<CustomerViewModel>>(_customerService.GetAll());
				if (!result.Any())
				{
					return BaseResponse<IEnumerable<CustomerViewModel>>.Error(_config.GetSection("StaticMessages").GetSection("Customer").GetSection("GetAll").GetSection("Error").Value ?? string.Empty);
				}
				return BaseResponse<IEnumerable<CustomerViewModel>>.Success(result, _config.GetSection("StaticMessages").GetSection("Customer").GetSection("GetAll").GetSection("Success").Value ?? string.Empty);
			}
			catch (Exception ex)
			{
				if (ex.InnerException != null)
				{
					string errorMessage = $"Inner Exception Message: {ex.InnerException.Message}, Exception Message: {ex.Message}";
					return BaseResponse<IEnumerable<CustomerViewModel>>.Error(errorMessage);
				}
				return BaseResponse<IEnumerable<CustomerViewModel>>.Error(ex.Message);
			}
		}

		public async Task<BaseResponse<CustomerViewModel>> GetByIdAsync(int id)
		{
			try
			{
				var result = _mapper.Map<CustomerDto, CustomerViewModel>(await _customerService.GetByIdAsync(id));
				if (result == null)
				{
					return BaseResponse<CustomerViewModel>.Error(_config.GetSection("StaticMessages").GetSection("Customer").GetSection("Get").GetSection("Warning").Value ?? string.Empty);
				}
				return BaseResponse<CustomerViewModel>.Success(result, _config.GetSection("StaticMessages").GetSection("Customer").GetSection("Get").GetSection("Success").Value ?? string.Empty);
			}
			catch (Exception ex)
			{
				if (ex.InnerException != null)
				{
					string errorMessage = $"Inner Exception Message: {ex.InnerException.Message}, Exception Message: {ex.Message}";
					return BaseResponse<CustomerViewModel>.Error(errorMessage);
				}
				return BaseResponse<CustomerViewModel>.Error(ex.Message);
			}
		}

		public BaseResponse<bool> Remove(CustomerViewModel customerViewModel)
		{
			try
			{
				var customerDto = _mapper.Map<CustomerViewModel, CustomerDto>(customerViewModel);
				var result = _customerService.Remove(customerDto);
				if (!result)
				{
					return BaseResponse<bool>.Error(_config.GetSection("StaticMessages").GetSection("Customer").GetSection("Delete").GetSection("Error").Value ?? string.Empty);
				}
				return BaseResponse<bool>.Success(result, _config.GetSection("StaticMessages").GetSection("Customer").GetSection("Delete").GetSection("Success").Value ?? string.Empty);
			}
			catch (Exception ex)
			{
				if (ex.InnerException != null)
				{
					string errorMessage = $"Inner Exception Message: {ex.InnerException.Message}, Exception Message: {ex.Message}";
					return BaseResponse<bool>.Error(errorMessage);
				}
				return BaseResponse<bool>.Error(ex.Message);
			}
		}

		public async Task<BaseResponse<bool>> RemoveAsync(int id)
		{
			try
			{
				var result = await _customerService.RemoveAsync(id);
				if (!result)
				{
					return BaseResponse<bool>.Error(_config.GetSection("StaticMessages").GetSection("Customer").GetSection("Delete").GetSection("Error").Value ?? string.Empty);
				}
				return BaseResponse<bool>.Success(result, _config.GetSection("StaticMessages").GetSection("Customer").GetSection("Delete").GetSection("Success").Value ?? string.Empty);
			}
			catch (Exception ex)
			{
				if (ex.InnerException != null)
				{
					string errorMessage = $"Inner Exception Message: {ex.InnerException.Message}, Exception Message: {ex.Message}";
					return BaseResponse<bool>.Error(errorMessage);
				}
				return BaseResponse<bool>.Error(ex.Message);
			}
		}

		public BaseResponse<bool> Update(CustomerViewModel customerViewModel)
		{
			try
			{
				var customerDto = _mapper.Map<CustomerViewModel, CustomerDto>(customerViewModel);
				var result = _customerService.Update(customerDto);
				if (!result)
				{
					return BaseResponse<bool>.Error(_config.GetSection("StaticMessages").GetSection("Customer").GetSection("Update").GetSection("Warning").Value ?? string.Empty);
				}
				return BaseResponse<bool>.Success(result, _config.GetSection("StaticMessages").GetSection("Customer").GetSection("Update").GetSection("Success").Value ?? string.Empty);
			}
			catch (Exception ex)
			{
				if (ex.InnerException != null)
				{
					string errorMessage = $"Inner Exception Message: {ex.InnerException.Message}, Exception Message: {ex.Message}";
					return BaseResponse<bool>.Error(errorMessage);
				}
				return BaseResponse<bool>.Error(ex.Message);
			}
		}
	}
}
