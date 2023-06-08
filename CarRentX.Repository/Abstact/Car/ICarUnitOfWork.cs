using CarRentX.BaseRepository.Abstract;
using CarRentX.Entity;
using CarRentX.UnitOfWork.Abstract;

namespace CarRentX.Repository.Abstact
{
	public interface ICarUnitOfWork:IUnitOfWork
	{
		// CarReadRepository adında IReadRepository<Car,int> tipinde bir özellik
		// CarReadRepository, Car türündeki nesnelerin okunması için kullanılacak olan bir interface'dir
		IReadRepository<Car,int> CarReadRepository { get; }

		// CarWriteRepository adında IWriteRepository<Car,int> tipinde bir özellik
		// CarWriteRepository, Car türündeki nesnelerin yazılması (eklenmesi/güncellenmesi/silinmesi) için kullanılacak olan bir interface'dir
		IWriteRepository<Car,int> CarWriteRepository { get; }

		// Değişikliklerin veritabanına kaydedilmesini sağlayan asenkron bir metot (Asenkron)
		Task<int> CommitAsync();

		// Değişikliklerin veritabanına kaydedilmesini sağlayan asenkron bir metot (Non-Asenkron)
		int Commit();
	}
}
