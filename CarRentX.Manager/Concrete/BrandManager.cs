using CarRentX.DTO.Brand;
using CarRentX.DTO.Car;
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
	public class BrandManager : IBrandManager
	{
		private readonly IBrandService _brandService;
		private readonly IConfiguration _config;
		private readonly IMapping _mapper;

		public BrandManager(IBrandService brandService, IMapping mapper, IConfiguration config)
		{
			_brandService = brandService;
			_mapper = mapper;
			_config = config;
		}

		public async Task<BaseResponse<int>> AddAsync(BrandViewModel brandViewModel)
		{
			try
			{
				var brandDto = _mapper.Map<BrandViewModel, BrandDto>(brandViewModel);
				var result = await _brandService.AddAsync(brandDto);
				if (result <= 0)
				{
					return BaseResponse<int>.Error(_config.GetSection("StaticMessages").GetSection("Brand").GetSection("Add").GetSection("Error").Value ?? string.Empty);
				}
				return BaseResponse<int>.Success(result, _config.GetSection("StaticMessages").GetSection("Brand").GetSection("Add").GetSection("Success").Value ?? string.Empty);
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

		public BaseResponse<IEnumerable<BrandViewModel>> GetAll()
		{
			try
			{
				var result = _mapper.Map<IEnumerable<BrandDto>, IEnumerable<BrandViewModel>>(_brandService.GetAll());
				if (!result.Any())
				{
					return BaseResponse<IEnumerable<BrandViewModel>>.Error(_config.GetSection("StaticMessages").GetSection("Brand").GetSection("GetAll").GetSection("Error").Value ?? string.Empty);
				}
				return BaseResponse<IEnumerable<BrandViewModel>>.Success(result, _config.GetSection("StaticMessages").GetSection("Car").GetSection("GetAll").GetSection("Success").Value ?? string.Empty);
			}
			catch (Exception ex)
			{
				if (ex.InnerException != null)
				{
					string errorMessage = $"Inner Exception Message: {ex.InnerException.Message}, Exception Message: {ex.Message}";
					return BaseResponse<IEnumerable<BrandViewModel>>.Error(errorMessage);
				}
				return BaseResponse<IEnumerable<BrandViewModel>>.Error(ex.Message);
			}
		}

		public async Task<BaseResponse<BrandViewModel>> GetByIdAsync(int id)
		{
			try
			{
				var result = _mapper.Map<BrandDto, BrandViewModel>(await _brandService.GetByIdAsync(id));
				if (result == null)
				{
					return BaseResponse<BrandViewModel>.Error(_config.GetSection("StaticMessages").GetSection("Brand").GetSection("Get").GetSection("Warning").Value ?? string.Empty);
				}
				return BaseResponse<BrandViewModel>.Success(result, _config.GetSection("StaticMessages").GetSection("Car").GetSection("Get").GetSection("Success").Value ?? string.Empty);
			}
			catch (Exception ex)
			{
				if (ex.InnerException != null)
				{
					string errorMessage = $"Inner Exception Message: {ex.InnerException.Message}, Exception Message: {ex.Message}";
					return BaseResponse<BrandViewModel>.Error(errorMessage);
				}
				return BaseResponse<BrandViewModel>.Error(ex.Message);
			}
		}

		public BaseResponse<bool> Remove(BrandViewModel brandViewModel)
		{
			try
			{
				var brandDto = _mapper.Map<BrandViewModel, BrandDto>(brandViewModel);
				var result = _brandService.Remove(brandDto);
				if (!result)
				{
					return BaseResponse<bool>.Error(_config.GetSection("StaticMessages").GetSection("Brand").GetSection("Delete").GetSection("Error").Value ?? string.Empty);
				}
				return BaseResponse<bool>.Success(result, _config.GetSection("StaticMessages").GetSection("Brand").GetSection("Delete").GetSection("Success").Value ?? string.Empty);
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
				var result = await _brandService.RemoveAsync(id);
				if (!result)
				{
					return BaseResponse<bool>.Error(_config.GetSection("StaticMessages").GetSection("Brand").GetSection("Delete").GetSection("Error").Value ?? string.Empty);
				}
				return BaseResponse<bool>.Success(result, _config.GetSection("StaticMessages").GetSection("Brand").GetSection("Delete").GetSection("Success").Value ?? string.Empty);
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

		public BaseResponse<bool> Update(BrandViewModel brandViewModel)
		{
			try
			{
				var brandDto = _mapper.Map<BrandViewModel, BrandDto>(brandViewModel);
				var result = _brandService.Update(brandDto);
				if (!result)
				{
					return BaseResponse<bool>.Error(_config.GetSection("StaticMessages").GetSection("Brand").GetSection("Update").GetSection("Warning").Value ?? string.Empty);
				}
				return BaseResponse<bool>.Success(result, _config.GetSection("StaticMessages").GetSection("Brand").GetSection("Update").GetSection("Success").Value ?? string.Empty);
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
