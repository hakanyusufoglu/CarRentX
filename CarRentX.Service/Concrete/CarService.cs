using CarRentX.DTO.Car;
using CarRentX.Entity.Concrete;
using CarRentX.Mapping.Abstract;
using CarRentX.Service.Abstract;
using CarRentX.UnitOfWork.Abstract;
using Microsoft.Extensions.Configuration;

namespace CarRentX.Service.Concrete
{
	//ToDo statik mesajlar appsettings.json dosyasına taşınacaktır.
	public class CarService : ICarService
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IConfiguration _config;
		private readonly IMapping _mapper;
		public CarService(IUnitOfWork unitOfWork, IMapping mapper, IConfiguration config)
		{
			_unitOfWork = unitOfWork;
			_mapper = mapper;
			_config = config;
		}
		public async Task<int> AddAsync(CarDto carDto)
		{
			_unitOfWork.BeginTransaction();
			if (carDto == null)
			{
				throw new ArgumentNullException(nameof(carDto), _config.GetSection("StaticMessages").GetSection("Car").GetSection("Add").GetSection("Error").Value ?? string.Empty);
			}
			try
			{
				await _unitOfWork.CarWriteRepository.AddAsync(_mapper.Map<CarDto, Car>(carDto));
				return await _unitOfWork.CommitAsync();
			}
			catch (ArgumentNullException ex)
			{
				_unitOfWork.Rollback();
				// ArgumentNullException özel olarak yakalandı
				// İşlem yapılacak kod buraya gelebilir
				// Örneğin, hata mesajını günlüğe kaydedebilir veya uygun bir şekilde işleyebilirsiniz
				throw new InvalidOperationException(_config.GetSection("StaticMessages").GetSection("Car").GetSection("Add").GetSection("Error").Value ?? string.Empty, ex);
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
		public IEnumerable<CarDto> GetAll()
		{
			_unitOfWork.BeginTransaction();
			try
			{
				var result = _unitOfWork.CarReadRepository.GetAll();
				return _mapper.Map<IEnumerable<Car>, IEnumerable<CarDto>>(result);
			}
			catch (Exception ex)
			{
				_unitOfWork.Rollback();
				throw new ApplicationException(_config.GetSection("StaticMessages").GetSection("Car").GetSection("GetAll").GetSection("Error").Value ?? string.Empty, ex);
			}
		}

		public async Task<IEnumerable<CarDto>> GetAllWithBrandColorAsync()
		{
			_unitOfWork.BeginTransaction();
			try
			{
				var result = await _unitOfWork.CarReadRepository.GetAllWithBrandColorAsync();
				return _mapper.Map<IEnumerable<Car>, IEnumerable<CarDto>>(result);
			}
			catch (Exception ex)
			{
				_unitOfWork.Rollback();
				throw new ApplicationException(_config.GetSection("StaticMessages").GetSection("Car").GetSection("GetAll").GetSection("Error").Value ?? string.Empty, ex);
			}
		}

		public async Task<CarDto> GetByIdAsync(int id)
		{
			_unitOfWork.BeginTransaction();
			try
			{
				var result = await _unitOfWork.CarReadRepository.GetByIdAsync(id);
				return _mapper.Map<Car, CarDto>(result);
			}
			catch (Exception ex)
			{
				_unitOfWork.Rollback();
				throw new ApplicationException(_config.GetSection("StaticMessages").GetSection("Car").GetSection("Get").GetSection("Warning").Value ?? string.Empty, ex);
			}
		}
		public bool Remove(CarDto carDto)
		{
			_unitOfWork.BeginTransaction();
			try
			{
				var result = _mapper.Map<CarDto, Car>(carDto);
				return _unitOfWork.CarWriteRepository.Remove(result);
			}
			catch (Exception ex)
			{
				_unitOfWork.Rollback();
				throw new ApplicationException(_config.GetSection("StaticMessages").GetSection("Car").GetSection("Delete").GetSection("Error").Value ?? string.Empty, ex);
			}
		}
		public async Task<bool> RemoveAsync(int id)
		{
			await _unitOfWork.BeginTransactionAsync();
			try
			{
				return await _unitOfWork.CarWriteRepository.RemoveAsync(id);
			}
			catch (Exception ex)
			{
				await _unitOfWork.RollbackAsync();
				throw new ApplicationException(_config.GetSection("StaticMessages").GetSection("Car").GetSection("Delete").GetSection("Error").Value ?? string.Empty, ex);
			}
		}
		public bool Update(CarDto carDto)
		{
			_unitOfWork.BeginTransaction();
			try
			{
				var result = _mapper.Map<CarDto, Car>(carDto);
				return _unitOfWork.CarWriteRepository.Update(result);
			}
			catch (Exception ex)
			{
				_unitOfWork.Rollback();
				throw new ApplicationException(_config.GetSection("StaticMessages").GetSection("Car").GetSection("Update").GetSection("Warning").Value ?? string.Empty, ex);
			}
		}


	}
}
