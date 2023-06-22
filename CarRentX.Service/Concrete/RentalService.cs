using CarRentX.DTO.Color;
using CarRentX.DTO.Rental;
using CarRentX.Entity.Concrete;
using CarRentX.Mapping.Abstract;
using CarRentX.Service.Abstract;
using CarRentX.UnitOfWork.Abstract;
using Microsoft.Extensions.Configuration;

namespace CarRentX.Service.Concrete
{
	public class RentalService : IRentalService
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IConfiguration _config;
		private readonly IMapping _mapper;

		public RentalService(IUnitOfWork unitOfWork, IConfiguration config, IMapping mapper)
		{
			_unitOfWork = unitOfWork;
			_config = config;
			_mapper = mapper;
		}

		public async Task<int> AddAsync(RentalDto rentalDto)
		{
			await _unitOfWork.BeginTransactionAsync();
			if (rentalDto == null)
			{
				throw new ArgumentNullException(nameof(rentalDto), _config.GetSection("StaticMessages").GetSection("Rental").GetSection("Add").GetSection("Error").Value ?? string.Empty);
			}
			try
			{
				await _unitOfWork.RentalWriteRepository.AddAsync(_mapper.Map<RentalDto, Rental>(rentalDto));
				return await _unitOfWork.CommitAsync();
			}
			catch (ArgumentNullException ex)
			{
				_unitOfWork.Rollback();
				// ArgumentNullException özel olarak yakalandı
				// İşlem yapılacak kod buraya gelebilir
				// Örneğin, hata mesajını günlüğe kaydedebilir veya uygun bir şekilde işleyebilirsiniz
				throw new InvalidOperationException(_config.GetSection("StaticMessages").GetSection("Rental").GetSection("Add").GetSection("Error").Value ?? string.Empty, ex);
			}
			catch (Exception ex)
			{
				_unitOfWork.Rollback();
				// Diğer tüm istisnalar burada yakalanır
				// İşlem yapılacak kod buraya gelebilir
				// Örneğin, hatayı günlüğe kaydedebilir veya uygun bir şekilde işleyebilirsiniz
				throw new ApplicationException(_config.GetSection("StaticMessages").GetSection("Rental").GetSection("Add").GetSection("Error").Value ?? string.Empty, ex);
			}
		}

		public IEnumerable<RentalDto> GetAll()
		{
			_unitOfWork.BeginTransaction();
			try
			{
				var result = _unitOfWork.RentalReadRepository.GetAll();
				return _mapper.Map<IEnumerable<Rental>, IEnumerable<RentalDto>>(result);
			}
			catch (Exception ex)
			{
				_unitOfWork.Rollback();
				throw new ApplicationException(_config.GetSection("StaticMessages").GetSection("Rental").GetSection("GetAll").GetSection("Error").Value ?? string.Empty, ex);
			}
		}
	}
}
