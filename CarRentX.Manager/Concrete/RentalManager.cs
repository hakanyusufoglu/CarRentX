using CarRentX.DTO.Color;
using CarRentX.DTO.Rental;
using CarRentX.Manager.Abstact;
using CarRentX.Mapping.Abstract;
using CarRentX.Service.Abstract;
using CarRentX.Service.Concrete;
using CarRentX.Utility.BaseResponse;
using CarRentX.ViewModel;
using CarRentX.ViewModel.Rental;
using Microsoft.Extensions.Configuration;

namespace CarRentX.Manager.Concrete
{
	public class RentalManager : IRentalManager
	{
		private readonly IRentalService _rentalService;
		private readonly IConfiguration _config;
		private readonly IMapping _mapper;

		public RentalManager(IRentalService rentalService, IConfiguration config, IMapping mapper)
		{
			_rentalService = rentalService;
			_config = config;
			_mapper = mapper;
		}

		public async Task<BaseResponse<int>> AddAsync(RentalViewModel rentalViewModel)
		{
			try
			{
				var rentalDto = _mapper.Map<RentalViewModel, RentalDto>(rentalViewModel);
				var result = await _rentalService.AddAsync(rentalDto);
				if (result <= 0)
				{
					return BaseResponse<int>.Error(_config.GetSection("StaticMessages").GetSection("Rental").GetSection("Add").GetSection("Error").Value ?? string.Empty);
				}
				return BaseResponse<int>.Success(rentalDto.Id, _config.GetSection("StaticMessages").GetSection("Rental").GetSection("Add").GetSection("Success").Value ?? string.Empty);
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

		public BaseResponse<IEnumerable<RentalViewModel>> GetAll()
		{
			try
			{
				var result = _mapper.Map<IEnumerable<RentalDto>, IEnumerable<RentalViewModel>>(_rentalService.GetAll());
				if (!result.Any())
				{
					return BaseResponse<IEnumerable<RentalViewModel>>.Error(_config.GetSection("StaticMessages").GetSection("Rental").GetSection("GetAll").GetSection("Error").Value ?? string.Empty);
				}
				return BaseResponse<IEnumerable<RentalViewModel>>.Success(result, _config.GetSection("StaticMessages").GetSection("Rental").GetSection("GetAll").GetSection("Success").Value ?? string.Empty);
			}
			catch (Exception ex)
			{
				if (ex.InnerException != null)
				{
					string errorMessage = $"Inner Exception Message: {ex.InnerException.Message}, Exception Message: {ex.Message}";
					return BaseResponse<IEnumerable<RentalViewModel>>.Error(errorMessage);
				}
				return BaseResponse<IEnumerable<RentalViewModel>>.Error(ex.Message);
			}
		}
	}
}
