using CarRentX.DTO.Car;
using CarRentX.Entity.Concrete;
using CarRentX.Mapping.Abstract;
using CarRentX.Repository.Abstact;
using CarRentX.Service.Abstract;
using CarRentX.UnitOfWork.Abstract;
using CarRentX.Utility.BaseResponse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRentX.Service.Concrete
{
	public class CarService : ICarService
	{
		private readonly IUnitOfWork _carUnitOfWork;
		private readonly IMapping _mapper;
		public CarService(IUnitOfWork carUnitOfWork, IMapping mapper)
		{
			_carUnitOfWork = carUnitOfWork;
			_mapper = mapper;
		}

		public async Task<int> AddAsync(CarDto carDto)
		{
			try
			{
				if (carDto == null)
					throw new ArgumentNullException(nameof(carDto));

				await _carUnitOfWork.CarWriteRepository.AddAsync(_mapper.Map<CarDto, Car>(carDto));
				return await _carUnitOfWork.CommitAsync();
			}
			catch (ArgumentNullException ex)
			{
				// ArgumentNullException özel olarak yakalandı
				// İşlem yapılacak kod buraya gelebilir
				// Örneğin, hata mesajını günlüğe kaydedebilir veya uygun bir şekilde işleyebilirsiniz
				throw new InvalidOperationException("CarDto parameter cannot be null.", ex);
			}
			catch (Exception ex)
			{
				// Diğer tüm istisnalar burada yakalanır
				// İşlem yapılacak kod buraya gelebilir
				// Örneğin, hatayı günlüğe kaydedebilir veya uygun bir şekilde işleyebilirsiniz
				throw new ApplicationException("An error occurred while adding the car.", ex);
			}
		}


		public Task<IEnumerable<CarDto>> GetAllAsync()
		{
			throw new NotImplementedException();
		}

		public Task<CarDto> GetByIdAsync(int id)
		{
			throw new NotImplementedException();
		}

		public bool Remove(CarDto carDto)
		{
			throw new NotImplementedException();
		}

		public Task<bool> RemoveAsync(int id)
		{
			throw new NotImplementedException();
		}

		public int Update(CarDto carDto)
		{
			throw new NotImplementedException();
		}
	}
}
