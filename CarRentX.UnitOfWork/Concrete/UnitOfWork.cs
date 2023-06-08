using CarRentX.ContextDb;
using CarRentX.Repository.Abstact;
using CarRentX.Repository.Concrete;
using CarRentX.UnitOfWork.Abstract;
using Microsoft.EntityFrameworkCore;

namespace CarRentX.UnitOfWork.Concrete
{
	// TContext tipinde bir DbContext'e bağlı olarak çalışan bir UnitOfWork sınıfı
	public class UnitOfWork : IUnitOfWork
	{
		private readonly RentCarXEfDbContext _context;
		private CarReadRepository _carReadRepository;
		private CarWriteRepository _carWriteRepository;

		public UnitOfWork(RentCarXEfDbContext context)
		{
			_context = context;
		}
		public ICarReadRepository CarReadRepository => _carReadRepository = _carReadRepository ?? new CarReadRepository(_context);
		public ICarWriteRepository CarWriteRepository => _carWriteRepository=_carWriteRepository?? new CarWriteRepository(_context);


		// Değişiklikleri veritabanına kaydeder ve kaydedilen değişiklik sayısını döndürür (Asenkron)
		public async Task<int> CommitAsync() => await _context.SaveChangesAsync();

		// Değişiklikleri veritabanına kaydeder ve kaydedilen değişiklik sayısını döndürür (Non-Asenkron)
		public int Commit() => _context.SaveChanges();

		// Kullanılan kaynakları serbest bırakır
		public void Dispose() => _context.Dispose();

	}
}
