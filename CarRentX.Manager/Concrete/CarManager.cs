using CarRentX.DTO.Car;
using CarRentX.Entity.Concrete;
using CarRentX.Manager.Abstact;
using CarRentX.Mapping.Abstract;
using CarRentX.Service.Abstract;
using CarRentX.Utility.BaseResponse;
using CarRentX.ViewModel.Car;
using FluentValidation;
using Microsoft.Extensions.Configuration;

namespace CarRentX.Manager.Concrete
{
	public class CarManager : ICarManager
	{
		private readonly ICarService _carService;
		private readonly IValidator<CarViewModel> _validator;
		private readonly IConfiguration _config;
		private readonly IMapping _mapper;

		public CarManager(ICarService carService, IMapping mapper, IConfiguration config, IValidator<CarViewModel> validator)
		{
			_carService = carService;
			_mapper = mapper;
			_config = config;
			_validator = validator;
		}
		public async Task<BaseResponse<int>> AddAsync(CarViewModel carViewModel)
		{
			try
			{
				var validationResult = _validator.Validate(carViewModel);
				
				if (!validationResult.IsValid) return BaseResponse<int>.Error(string.Join(" ", validationResult.Errors.Select(error => error.ErrorMessage)));

				var carDto = _mapper.Map<CarViewModel, CarDto>(carViewModel);
				var result = await _carService.AddAsync(carDto);
				if (result <= 0)
				{
					return BaseResponse<int>.Error(_config.GetSection("StaticMessages").GetSection("Car").GetSection("Add").GetSection("Error").Value ?? string.Empty);
				}
				return BaseResponse<int>.Success(carDto.Id, _config.GetSection("StaticMessages").GetSection("Car").GetSection("Add").GetSection("Success").Value ?? string.Empty);
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
		public BaseResponse<IEnumerable<CarViewModel>> GetAll()
		{
			try
			{
				var result = _mapper.Map<IEnumerable<CarDto>, IEnumerable<CarViewModel>>(_carService.GetAll());
				if (!result.Any())
				{
					return BaseResponse<IEnumerable<CarViewModel>>.Error(_config.GetSection("StaticMessages").GetSection("Car").GetSection("GetAll").GetSection("Error").Value ?? string.Empty);
				}
				return BaseResponse<IEnumerable<CarViewModel>>.Success(result, _config.GetSection("StaticMessages").GetSection("Car").GetSection("GetAll").GetSection("Success").Value ?? string.Empty);
			}
			catch (Exception ex)
			{
				if (ex.InnerException != null)
				{
					string errorMessage = $"Inner Exception Message: {ex.InnerException.Message}, Exception Message: {ex.Message}";
					return BaseResponse<IEnumerable<CarViewModel>>.Error(errorMessage);
				}
				return BaseResponse<IEnumerable<CarViewModel>>.Error(ex.Message);
			}
		}
		public async Task<BaseResponse<CarViewModel>> GetByIdAsync(int id)
		{
			try
			{
				var result = _mapper.Map<CarDto, CarViewModel>(await _carService.GetByIdAsync(id));
				if (result == null)
				{
					return BaseResponse<CarViewModel>.Error(_config.GetSection("StaticMessages").GetSection("Car").GetSection("Get").GetSection("Warning").Value ?? string.Empty);
				}
				return BaseResponse<CarViewModel>.Success(result, _config.GetSection("StaticMessages").GetSection("Car").GetSection("Get").GetSection("Success").Value ?? string.Empty);
			}
			catch (Exception ex)
			{
				if (ex.InnerException != null)
				{
					string errorMessage = $"Inner Exception Message: {ex.InnerException.Message}, Exception Message: {ex.Message}";
					return BaseResponse<CarViewModel>.Error(errorMessage);
				}
				return BaseResponse<CarViewModel>.Error(ex.Message);
			}
		}
		public BaseResponse<bool> Remove(CarViewModel carViewModel)
		{
			try
			{
				var carDto = _mapper.Map<CarViewModel, CarDto>(carViewModel);
				var result = _carService.Remove(carDto);
				if (!result)
				{
					return BaseResponse<bool>.Error(_config.GetSection("StaticMessages").GetSection("Car").GetSection("Delete").GetSection("Error").Value ?? string.Empty);
				}
				return BaseResponse<bool>.Success(result, _config.GetSection("StaticMessages").GetSection("Car").GetSection("Delete").GetSection("Success").Value ?? string.Empty);
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
				var result = await _carService.RemoveAsync(id);
				if (!result)
				{
					return BaseResponse<bool>.Error(_config.GetSection("StaticMessages").GetSection("Car").GetSection("Delete").GetSection("Error").Value ?? string.Empty);
				}
				return BaseResponse<bool>.Success(result, _config.GetSection("StaticMessages").GetSection("Car").GetSection("Delete").GetSection("Success").Value ?? string.Empty);
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
		public BaseResponse<bool> Update(CarViewModel carViewModel)
		{
			try
			{
				var carDto = _mapper.Map<CarViewModel, CarDto>(carViewModel);
				var result = _carService.Update(carDto);
				if (!result)
				{
					return BaseResponse<bool>.Error(_config.GetSection("StaticMessages").GetSection("Car").GetSection("Update").GetSection("Warning").Value ?? string.Empty);
				}
				return BaseResponse<bool>.Success(result, _config.GetSection("StaticMessages").GetSection("Car").GetSection("Update").GetSection("Success").Value ?? string.Empty);
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
