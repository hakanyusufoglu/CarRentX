using CarRentX.DTO.Color;
using CarRentX.Entity.Concrete;
using CarRentX.Mapping.Abstract;
using CarRentX.Service.Abstract;
using CarRentX.UnitOfWork.Abstract;
using Microsoft.Extensions.Configuration;

namespace CarRentX.Service.Concrete
{
	public class ColorService : IColorService
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IConfiguration _config;
		private readonly IMapping _mapper;
		public ColorService(IUnitOfWork carUnitOfWork, IMapping mapper, IConfiguration config)
		{
			_unitOfWork = carUnitOfWork;
			_mapper = mapper;
			_config = config;
		}
		public async Task<int> AddAsync(ColorDto colorDto)
		{
			await _unitOfWork.BeginTransactionAsync();
			if (colorDto == null)
			{
				throw new ArgumentNullException(nameof(colorDto), _config.GetSection("StaticMessages").GetSection("Color").GetSection("Add").GetSection("Error").Value ?? string.Empty);
			}
			try
			{
				await _unitOfWork.ColorWriteRepository.AddAsync(_mapper.Map<ColorDto, Color>(colorDto));
				return await _unitOfWork.CommitAsync();
			}
			catch (ArgumentNullException ex)
			{
				_unitOfWork.Rollback();
				// ArgumentNullException özel olarak yakalandı
				// İşlem yapılacak kod buraya gelebilir
				// Örneğin, hata mesajını günlüğe kaydedebilir veya uygun bir şekilde işleyebilirsiniz
				throw new InvalidOperationException(_config.GetSection("StaticMessages").GetSection("Color").GetSection("Add").GetSection("Error").Value ?? string.Empty, ex);
			}
			catch (Exception ex)
			{
				_unitOfWork.Rollback();
				// Diğer tüm istisnalar burada yakalanır
				// İşlem yapılacak kod buraya gelebilir
				// Örneğin, hatayı günlüğe kaydedebilir veya uygun bir şekilde işleyebilirsiniz
				throw new ApplicationException(_config.GetSection("StaticMessages").GetSection("Color").GetSection("Add").GetSection("Error").Value ?? string.Empty, ex);
			}
		}

		public IEnumerable<ColorDto> GetAll()
		{
			_unitOfWork.BeginTransaction();
			try
			{
				var result = _unitOfWork.ColorReadRepository.GetAll();
				return _mapper.Map<IEnumerable<Color>, IEnumerable<ColorDto>>(result);
			}
			catch (Exception ex)
			{
				_unitOfWork.Rollback();
				throw new ApplicationException(_config.GetSection("StaticMessages").GetSection("Color").GetSection("GetAll").GetSection("Error").Value ?? string.Empty, ex);
			}
		}

		public async Task<ColorDto> GetByIdAsync(int id)
		{
			await _unitOfWork.BeginTransactionAsync();
			try
			{
				var result = await _unitOfWork.ColorReadRepository.GetByIdAsync(id);
				return _mapper.Map<Color, ColorDto>(result);
			}
			catch (Exception ex)
			{
				_unitOfWork.Rollback();
				throw new ApplicationException(_config.GetSection("StaticMessages").GetSection("Color").GetSection("Get").GetSection("Warning").Value ?? string.Empty, ex);
			}
		}

		public bool Remove(ColorDto colorDto)
		{
			_unitOfWork.BeginTransaction();
			try
			{
				var result = _mapper.Map<ColorDto, Color>(colorDto);
				return _unitOfWork.ColorWriteRepository.Remove(result);
			}
			catch (Exception ex)
			{
				_unitOfWork.Rollback();
				throw new ApplicationException(_config.GetSection("StaticMessages").GetSection("Color").GetSection("Delete").GetSection("Error").Value ?? string.Empty, ex);
			}
		}

		public async Task<bool> RemoveAsync(int id)
		{
			await _unitOfWork.BeginTransactionAsync();
			try
			{
				return await _unitOfWork.ColorWriteRepository.RemoveAsync(id);
			}
			catch (Exception ex)
			{
				await _unitOfWork.RollbackAsync();
				throw new ApplicationException(_config.GetSection("StaticMessages").GetSection("Color").GetSection("Delete").GetSection("Error").Value ?? string.Empty, ex);
			}
		}

		public bool Update(ColorDto colorDto)
		{
			_unitOfWork.BeginTransaction();
			try
			{
				var result = _mapper.Map<ColorDto, Color>(colorDto);
				return _unitOfWork.ColorWriteRepository.Update(result);
			}
			catch (Exception ex)
			{
				_unitOfWork.Rollback();
				throw new ApplicationException(_config.GetSection("StaticMessages").GetSection("Color").GetSection("Update").GetSection("Warning").Value ?? string.Empty, ex);
			}
		}
	}
}
