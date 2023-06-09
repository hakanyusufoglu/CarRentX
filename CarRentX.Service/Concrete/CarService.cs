using CarRentX.DTO.Car;
using CarRentX.Entity.Concrete;
using CarRentX.Mapping.Abstract;
using CarRentX.Service.Abstract;
using CarRentX.UnitOfWork.Abstract;

namespace CarRentX.Service.Concrete
{
	//ToDo statik mesajlar appsettings.json dosyasına taşınacaktır.
	public class CarService : ICarService
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IMapping _mapper;
		public CarService(IUnitOfWork carUnitOfWork, IMapping mapper)
		{
			_unitOfWork = carUnitOfWork;
			_mapper = mapper;
		}
		public async Task<int> AddAsync(CarDto carDto)
		{
			_unitOfWork.BeginTransaction();
			if (carDto == null)
			{
				throw new ArgumentNullException(nameof(carDto), "CarDto parametresi boş olamaz.");
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
				throw new InvalidOperationException("arDto parametresi boş olamaz.", ex);
			}
			catch (Exception ex)
			{
				// Diğer tüm istisnalar burada yakalanır
				// İşlem yapılacak kod buraya gelebilir
				// Örneğin, hatayı günlüğe kaydedebilir veya uygun bir şekilde işleyebilirsiniz
				throw new ApplicationException("Araba eklenirken bir hata oluştu.", ex);
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
				throw new ApplicationException("Arabalar alınırken bir hata oluştu.", ex);
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
				throw new ApplicationException("Araba alınırken bir hata oluştu.", ex);
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
				throw new ApplicationException("Araba silinirken bir hata oluştu.", ex);
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
				throw new ApplicationException("Araba silinirken bir hata oluştu.", ex);
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
				throw new ApplicationException("Araba güncellenirken bir hata oluştu.", ex);
			}
		}


	}
}
