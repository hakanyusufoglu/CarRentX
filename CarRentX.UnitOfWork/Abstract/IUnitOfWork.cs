using CarRentX.Repository.Abstact;

namespace CarRentX.UnitOfWork.Abstract
{
	public interface IUnitOfWork : IDisposable
	{
		ICarReadRepository CarReadRepository { get; }
		ICarWriteRepository CarWriteRepository { get; }
		Task<int> CommitAsync();
		int Commit();
	}
}
