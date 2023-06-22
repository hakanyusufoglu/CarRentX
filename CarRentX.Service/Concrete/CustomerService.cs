using CarRentX.DTO.Customer;
using CarRentX.Entity.Concrete;
using CarRentX.Mapping.Abstract;
using CarRentX.Service.Abstract;
using CarRentX.UnitOfWork.Abstract;
using Microsoft.Extensions.Configuration;

namespace CarRentX.Service.Concrete
{
	public class CustomerService : ICustomerService
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IConfiguration _config;
		private readonly IMapping _mapper;
		public CustomerService(IUnitOfWork unitOfWork, IMapping mapper, IConfiguration config)
		{
			_unitOfWork = unitOfWork;
			_mapper = mapper;
			_config = config;
		}
		public async Task<int> AddAsync(CustomerDto customerDto)
		{
			await _unitOfWork.BeginTransactionAsync();
			if (customerDto == null)
			{
				throw new ArgumentNullException(nameof(customerDto), _config.GetSection("StaticMessages").GetSection("Customer").GetSection("Add").GetSection("Error").Value ?? string.Empty);
			}
			try
			{
				await _unitOfWork.CustomerWriteRepository.AddAsync(_mapper.Map<CustomerDto, Customer>(customerDto));
				return await _unitOfWork.CommitAsync();
			}
			catch (ArgumentNullException ex)
			{
				_unitOfWork.Rollback();
				// ArgumentNullException özel olarak yakalandı
				// İşlem yapılacak kod buraya gelebilir
				// Örneğin, hata mesajını günlüğe kaydedebilir veya uygun bir şekilde işleyebilirsiniz
				throw new InvalidOperationException(_config.GetSection("StaticMessages").GetSection("Customer").GetSection("Add").GetSection("Error").Value ?? string.Empty, ex);
			}
			catch (Exception ex)
			{
				_unitOfWork.Rollback();
				// Diğer tüm istisnalar burada yakalanır
				// İşlem yapılacak kod buraya gelebilir
				// Örneğin, hatayı günlüğe kaydedebilir veya uygun bir şekilde işleyebilirsiniz
				throw new ApplicationException(_config.GetSection("StaticMessages").GetSection("Customer").GetSection("Add").GetSection("Error").Value ?? string.Empty, ex);
			}
		}

		public IEnumerable<CustomerDto> GetAll()
		{
			_unitOfWork.BeginTransaction();
			try
			{
				var result = _unitOfWork.CustomerReadRepository.GetAll();
				return _mapper.Map<IEnumerable<Customer>, IEnumerable<CustomerDto>>(result);
			}
			catch (Exception ex)
			{
				_unitOfWork.Rollback();
				throw new ApplicationException(_config.GetSection("StaticMessages").GetSection("Customer").GetSection("GetAll").GetSection("Error").Value ?? string.Empty, ex);
			}
		}

		public async Task<CustomerDto> GetByIdAsync(int id)
		{
			await _unitOfWork.BeginTransactionAsync();
			try
			{
				var result = await _unitOfWork.CustomerReadRepository.GetByIdAsync(id);
				return _mapper.Map<Customer, CustomerDto>(result);
			}
			catch (Exception ex)
			{
				_unitOfWork.Rollback();
				throw new ApplicationException(_config.GetSection("StaticMessages").GetSection("Customer").GetSection("Get").GetSection("Warning").Value ?? string.Empty, ex);
			}
		}

		public bool Remove(CustomerDto customerDto)
		{
			_unitOfWork.BeginTransaction();
			try
			{
				var result = _mapper.Map<CustomerDto, Customer>(customerDto);
				return _unitOfWork.CustomerWriteRepository.Remove(result);
			}
			catch (Exception ex)
			{
				_unitOfWork.Rollback();
				throw new ApplicationException(_config.GetSection("StaticMessages").GetSection("Customer").GetSection("Delete").GetSection("Error").Value ?? string.Empty, ex);
			}
		}

		public async Task<bool> RemoveAsync(int id)
		{
			await _unitOfWork.BeginTransactionAsync();
			try
			{
				return await _unitOfWork.CustomerWriteRepository.RemoveAsync(id);
			}
			catch (Exception ex)
			{
				_unitOfWork.Rollback();
				throw new ApplicationException(_config.GetSection("StaticMessages").GetSection("Customer").GetSection("Delete").GetSection("Error").Value ?? string.Empty, ex);
			}
		}

		public bool Update(CustomerDto customerDto)
		{
			_unitOfWork.BeginTransaction();
			try
			{
				var result = _mapper.Map<CustomerDto, Customer>(customerDto);
				return _unitOfWork.CustomerWriteRepository.Update(result);
			}
			catch (Exception ex)
			{
				_unitOfWork.Rollback();
				throw new ApplicationException(_config.GetSection("StaticMessages").GetSection("Customer").GetSection("Update").GetSection("Warning").Value ?? string.Empty, ex);
			}
		}
	}
}
