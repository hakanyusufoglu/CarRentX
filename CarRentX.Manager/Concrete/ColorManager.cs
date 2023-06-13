using CarRentX.DTO.Car;
using CarRentX.DTO.Color;
using CarRentX.Manager.Abstact;
using CarRentX.Mapping.Abstract;
using CarRentX.Service.Abstract;
using CarRentX.Service.Concrete;
using CarRentX.Utility.BaseResponse;
using CarRentX.ViewModel;
using CarRentX.ViewModel.Car;
using FluentValidation;
using Microsoft.Extensions.Configuration;

namespace CarRentX.Manager.Concrete
{
	public class ColorManager : IColorManager
	{
		private readonly IColorService _colorService;
		private readonly IConfiguration _config;
		private readonly IMapping _mapper;
		public ColorManager(IColorService colorService, IMapping mapper, IConfiguration config)
		{
			_colorService = colorService;
			_mapper = mapper;
			_config = config;
		}
		public async Task<BaseResponse<int>> AddAsync(ColorViewModel colorViewModel)
		{
			try
			{
				var colorDto = _mapper.Map<ColorViewModel, ColorDto>(colorViewModel);
				var result = await _colorService.AddAsync(colorDto);
				if (result <= 0)
				{
					return BaseResponse<int>.Error(_config.GetSection("StaticMessages").GetSection("Color").GetSection("Add").GetSection("Error").Value ?? string.Empty);
				}
				return BaseResponse<int>.Success(colorDto.Id, _config.GetSection("StaticMessages").GetSection("Color").GetSection("Add").GetSection("Success").Value ?? string.Empty);
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

		public BaseResponse<IEnumerable<ColorViewModel>> GetAll()
		{
			try
			{
				var result = _mapper.Map<IEnumerable<ColorDto>, IEnumerable<ColorViewModel>>(_colorService.GetAll());
				if (!result.Any())
				{
					return BaseResponse<IEnumerable<ColorViewModel>>.Error(_config.GetSection("StaticMessages").GetSection("Color").GetSection("GetAll").GetSection("Error").Value ?? string.Empty);
				}
				return BaseResponse<IEnumerable<ColorViewModel>>.Success(result, _config.GetSection("StaticMessages").GetSection("Car").GetSection("GetAll").GetSection("Success").Value ?? string.Empty);
			}
			catch (Exception ex)
			{
				if (ex.InnerException != null)
				{
					string errorMessage = $"Inner Exception Message: {ex.InnerException.Message}, Exception Message: {ex.Message}";
					return BaseResponse<IEnumerable<ColorViewModel>>.Error(errorMessage);
				}
				return BaseResponse<IEnumerable<ColorViewModel>>.Error(ex.Message);
			}
		}

		public async Task<BaseResponse<ColorViewModel>> GetByIdAsync(int id)
		{
			try
			{
				var result = _mapper.Map<ColorDto, ColorViewModel>(await _colorService.GetByIdAsync(id));
				if (result == null)
				{
					return BaseResponse<ColorViewModel>.Error(_config.GetSection("StaticMessages").GetSection("Color").GetSection("Get").GetSection("Warning").Value ?? string.Empty);
				}
				return BaseResponse<ColorViewModel>.Success(result, _config.GetSection("StaticMessages").GetSection("Car").GetSection("Get").GetSection("Success").Value ?? string.Empty);
			}
			catch (Exception ex)
			{
				if (ex.InnerException != null)
				{
					string errorMessage = $"Inner Exception Message: {ex.InnerException.Message}, Exception Message: {ex.Message}";
					return BaseResponse<ColorViewModel>.Error(errorMessage);
				}
				return BaseResponse<ColorViewModel>.Error(ex.Message);
			}
		}

		public BaseResponse<bool> Remove(ColorViewModel colorViewModel)
		{
			try
			{
				var carDto = _mapper.Map<ColorViewModel, ColorDto>(colorViewModel);
				var result = _colorService.Remove(carDto);
				if (!result)
				{
					return BaseResponse<bool>.Error(_config.GetSection("StaticMessages").GetSection("Color").GetSection("Delete").GetSection("Error").Value ?? string.Empty);
				}
				return BaseResponse<bool>.Success(result, _config.GetSection("StaticMessages").GetSection("Color").GetSection("Delete").GetSection("Success").Value ?? string.Empty);
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
				var result = await _colorService.RemoveAsync(id);
				if (!result)
				{
					return BaseResponse<bool>.Error(_config.GetSection("StaticMessages").GetSection("Color").GetSection("Delete").GetSection("Error").Value ?? string.Empty);
				}
				return BaseResponse<bool>.Success(result, _config.GetSection("StaticMessages").GetSection("Color").GetSection("Delete").GetSection("Success").Value ?? string.Empty);
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

		public BaseResponse<bool> Update(ColorViewModel colorViewModel)
		{
			try
			{
				var colorDto = _mapper.Map<ColorViewModel, ColorDto>(colorViewModel);
				var result = _colorService.Update(colorDto);
				if (!result)
				{
					return BaseResponse<bool>.Error(_config.GetSection("StaticMessages").GetSection("Color").GetSection("Update").GetSection("Warning").Value ?? string.Empty);
				}
				return BaseResponse<bool>.Success(result, _config.GetSection("StaticMessages").GetSection("Color").GetSection("Update").GetSection("Success").Value ?? string.Empty);
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
