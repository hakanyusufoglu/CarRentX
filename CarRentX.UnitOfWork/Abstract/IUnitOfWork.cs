using CarRentX.Repository.Abstact;

namespace CarRentX.UnitOfWork.Abstract
{
	public interface IUnitOfWork : IDisposable
	{
		
		ICarReadRepository CarReadRepository { get; }
		ICarWriteRepository CarWriteRepository { get; }
		IColorReadRepository ColorReadRepository { get; }
		IColorWriteRepository ColorWriteRepository { get; }
		IBrandReadRepository BrandReadRepository { get; }
		IBrandWriteRepository BrandWriteRepository { get; }
		IRentalReadRepository RentalReadRepository { get; }
		IRentalWriteRepository RentalWriteRepository { get; }
		ICustomerReadRepository CustomerReadRepository { get; }
		ICustomerWriteRepository CustomerWriteRepository { get; }
		IUserRefreshTokenReadRepository UserRefreshTokenReadRepository { get; }
		IUserRefreshTokenWriteRepository UserRefreshTokenWriteRepository { get; }
		Task<int> CommitAsync();
		int Commit();
		void BeginTransaction();
		void Rollback();
		Task BeginTransactionAsync();
		Task RollbackAsync();
	}
}
