using CarRentX.DTO.Car;
using CarRentX.Manager.Abstact;
using CarRentX.Mapping.Abstract;
using CarRentX.Service.Abstract;
using CarRentX.Utility.BaseResponse;
using CarRentX.ViewModel.Car;

namespace CarRentX.Manager.Concrete
{
	public class CarManager : ICarManager
	{
		private readonly ICarService _carService;
		private readonly IMapping _mapper;

		public CarManager(ICarService carService, IMapping mapper)
		{
			_carService = carService;
			_mapper = mapper;
		}
		public async Task<BaseResponse<int>> AddAsync(CarViewModel carViewModel)
		{
			try
			{
				var carDto = _mapper.Map<CarViewModel, CarDto>(carViewModel);
				var result = await _carService.AddAsync(carDto);
				if (result <= 0)
				{
					return BaseResponse<int>.Error("Result is not greater than zero.");
				}
				return BaseResponse<int>.Success(carDto.Id, "test1");
			}
			catch (Exception ex)
			{
				string errorMessage;
				if (ex.InnerException != null)
				{
					errorMessage = $"Inner Exception Message: {ex.InnerException.Message}, Exception Message: {ex.Message}";
				}
				else
				{
					errorMessage = ex.Message;
				}

				return BaseResponse<int>.Error(errorMessage);
			}
		}
		public BaseResponse<IEnumerable<CarViewModel>> GetAll()
		{
			try
			{
				var result = _mapper.Map<IEnumerable<CarDto>, IEnumerable<CarViewModel>>(_carService.GetAll());
				if (!result.Any())
				{
					return BaseResponse<IEnumerable<CarViewModel>>.Error("Result is not greater than zero.");
				}
				return BaseResponse<IEnumerable<CarViewModel>>.Success(result, "Araçlar getirildi.");
			}
			catch (Exception ex)
			{
				string errorMessage;
				if (ex.InnerException != null)
				{
					errorMessage = $"Inner Exception Message: {ex.InnerException.Message}, Exception Message: {ex.Message}";
				}
				else
				{
					errorMessage = ex.Message;
				}
				return BaseResponse<IEnumerable<CarViewModel>>.Error(errorMessage);
			}
		}

		public async Task<BaseResponse<CarViewModel>> GetByIdAsync(int id)
		{
			try
			{
				var result = _mapper.Map<CarDto, CarViewModel>(await _carService.GetByIdAsync(id));
				if (result == null)
				{
					return BaseResponse<CarViewModel>.Error("Araç getirilemedi.");
				}
				return BaseResponse<CarViewModel>.Success(result, "Araç getirildi.");
			}
			catch (Exception ex)
			{
				string errorMessage;
				if (ex.InnerException != null)
				{
					errorMessage = $"Inner Exception Message: {ex.InnerException.Message}, Exception Message: {ex.Message}";
				}
				else
				{
					errorMessage = ex.Message;
				}
				return BaseResponse<CarViewModel>.Error(errorMessage);
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
					return BaseResponse<bool>.Error("Araç silinemedi.");
				}
				return BaseResponse<bool>.Success(result, "Araç silindi.");
			}
			catch (Exception ex)
			{
				string errorMessage;
				if (ex.InnerException != null)
				{
					errorMessage = $"Inner Exception Message: {ex.InnerException.Message}, Exception Message: {ex.Message}";
				}
				else
				{
					errorMessage = ex.Message;
				}
				return BaseResponse<bool>.Error(errorMessage);
			}
		}

		public async Task<BaseResponse<bool>> RemoveAsync(int id)
		{
			try
			{
				var result = await _carService.RemoveAsync(id);
				if (!result)
				{
					return BaseResponse<bool>.Error("Araç silinemedi.");
				}
				return BaseResponse<bool>.Success(result, "Araç silindi.");
			}
			catch (Exception ex)
			{
				string errorMessage;
				if (ex.InnerException != null)
				{
					errorMessage = $"Inner Exception Message: {ex.InnerException.Message}, Exception Message: {ex.Message}";
				}
				else
				{
					errorMessage = ex.Message;
				}
				return BaseResponse<bool>.Error(errorMessage);
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
					return BaseResponse<bool>.Error("Araç güncüllenemedi.");
				}
				return BaseResponse<bool>.Success(result, "Araç güncellendi.");
			}
			catch (Exception ex)
			{
				string errorMessage;
				if (ex.InnerException != null)
				{
					errorMessage = $"Inner Exception Message: {ex.InnerException.Message}, Exception Message: {ex.Message}";
				}
				else
				{
					errorMessage = ex.Message;
				}
				return BaseResponse<bool>.Error(errorMessage);
			}
		}
	}
}
