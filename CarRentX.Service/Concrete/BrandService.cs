using CarRentX.DTO.Brand;
using CarRentX.DTO.Car;
using CarRentX.Entity.Concrete;
using CarRentX.Mapping.Abstract;
using CarRentX.Service.Abstract;
using CarRentX.UnitOfWork.Abstract;
using Microsoft.Extensions.Configuration;

namespace CarRentX.Service.Concrete
{
	public class BrandService : IBrandService
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IConfiguration _config;
		private readonly IMapping _mapper;
		public BrandService(IUnitOfWork unitOfWork, IMapping mapper, IConfiguration config)
		{
			_unitOfWork = unitOfWork;
			_mapper = mapper;
			_config = config;
		}
		public async Task<int> AddAsync(BrandDto brandDto)
		{
			await _unitOfWork.BeginTransactionAsync();
			if (brandDto == null)
			{
				throw new ArgumentNullException(nameof(brandDto), _config.GetSection("StaticMessages").GetSection("Brand").GetSection("Add").GetSection("Error").Value ?? string.Empty);
			}
			try
			{
				await _unitOfWork.BrandWriteRepository.AddAsync(_mapper.Map<BrandDto, Brand>(brandDto));
				return await _unitOfWork.CommitAsync();
			}
			catch (ArgumentNullException ex)
			{
				_unitOfWork.Rollback();
				// ArgumentNullException özel olarak yakalandı
				// İşlem yapılacak kod buraya gelebilir
				// Örneğin, hata mesajını günlüğe kaydedebilir veya uygun bir şekilde işleyebilirsiniz
				throw new InvalidOperationException(_config.GetSection("StaticMessages").GetSection("Brand").GetSection("Add").GetSection("Error").Value ?? string.Empty, ex);
			}
			catch (Exception ex)
			{
				_unitOfWork.Rollback();
				// Diğer tüm istisnalar burada yakalanır
				// İşlem yapılacak kod buraya gelebilir
				// Örneğin, hatayı günlüğe kaydedebilir veya uygun bir şekilde işleyebilirsiniz
				throw new ApplicationException(_config.GetSection("StaticMessages").GetSection("Car").GetSection("Add").GetSection("Error").Value ?? string.Empty, ex);
			}
		}

		public IEnumerable<BrandDto> GetAll()
		{
			_unitOfWork.BeginTransaction();
			try
			{
				var result = _unitOfWork.BrandReadRepository.GetAll();
				return _mapper.Map<IEnumerable<Brand>, IEnumerable<BrandDto>>(result);
			}
			catch (Exception ex)
			{
				_unitOfWork.Rollback();
				throw new ApplicationException(_config.GetSection("StaticMessages").GetSection("Brand").GetSection("GetAll").GetSection("Error").Value ?? string.Empty, ex);
			}
		}

		public async Task<BrandDto> GetByIdAsync(int id)
		{
			await _unitOfWork.BeginTransactionAsync();
			try
			{
				var result = await _unitOfWork.BrandReadRepository.GetByIdAsync(id);
				return _mapper.Map<Brand, BrandDto>(result);
			}
			catch (Exception ex)
			{
				_unitOfWork.Rollback();
				throw new ApplicationException(_config.GetSection("StaticMessages").GetSection("Brand").GetSection("Get").GetSection("Warning").Value ?? string.Empty, ex);
			}
		}

		public bool Remove(BrandDto brandDto)
		{
			_unitOfWork.BeginTransaction();
			try
			{
				var result = _mapper.Map<BrandDto, Brand>(brandDto);
				return _unitOfWork.BrandWriteRepository.Remove(result);
			}
			catch (Exception ex)
			{
				_unitOfWork.Rollback();
				throw new ApplicationException(_config.GetSection("StaticMessages").GetSection("Brand").GetSection("Delete").GetSection("Error").Value ?? string.Empty, ex);
			}
		}

		public async Task<bool> RemoveAsync(int id)
		{
			await _unitOfWork.BeginTransactionAsync();
			try
			{
				return await _unitOfWork.BrandWriteRepository.RemoveAsync(id);
			}
			catch (Exception ex)
			{
				await _unitOfWork.RollbackAsync();
				throw new ApplicationException(_config.GetSection("StaticMessages").GetSection("Brand").GetSection("Delete").GetSection("Error").Value ?? string.Empty, ex);
			}
		}

		public bool Update(BrandDto brandDto)
		{
			_unitOfWork.BeginTransaction();
			try
			{
				var result = _mapper.Map<BrandDto, Brand>(brandDto);
				return _unitOfWork.BrandWriteRepository.Update(result);
			}
			catch (Exception ex)
			{
				_unitOfWork.Rollback();
				throw new ApplicationException(_config.GetSection("StaticMessages").GetSection("Brand").GetSection("Update").GetSection("Warning").Value ?? string.Empty, ex);
			}
		}
	}
}
