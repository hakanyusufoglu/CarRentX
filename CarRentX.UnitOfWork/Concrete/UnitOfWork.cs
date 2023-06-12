using CarRentX.ContextDb;
using CarRentX.Repository.Abstact;
using CarRentX.Repository.Concrete;
using CarRentX.UnitOfWork.Abstract;
using Microsoft.EntityFrameworkCore.Storage;

namespace CarRentX.UnitOfWork.Concrete
{
	// TContext tipinde bir DbContext'e bağlı olarak çalışan bir UnitOfWork sınıfı
	public class UnitOfWork : IUnitOfWork
	{
		private readonly RentCarXEfDbContext _context;
		private IDbContextTransaction _transaction;
		private CarReadRepository _carReadRepository;
		private CarWriteRepository _carWriteRepository;

		public UnitOfWork(RentCarXEfDbContext context)
		{
			_context = context;
		}

		// CarReadRepository'nin bir örneğini döndürür (Lazy initialization)
		public ICarReadRepository CarReadRepository => _carReadRepository = _carReadRepository ?? new CarReadRepository(_context);

		// CarWriteRepository'nin bir örneğini döndürür (Lazy initialization)
		public ICarWriteRepository CarWriteRepository => _carWriteRepository = _carWriteRepository ?? new CarWriteRepository(_context);

		// Değişiklikleri veritabanına kaydeder ve kaydedilen değişiklik sayısını döndürür (Asenkron)
		public async Task<int> CommitAsync()
		{
			var result = await _context.SaveChangesAsync(); // Yapılan değişiklikleri veritabanına kaydeder ve kaydedilen değişiklik sayısını döndürür (Asenkron)
			await _transaction.CommitAsync(); // Transaction'ı commit eder (Asenkron)
			return result;
		}

		// Değişiklikleri veritabanına kaydeder ve kaydedilen değişiklik sayısını döndürür (Non-Asenkron)
		public int Commit()
		{
			var result = _context.SaveChanges(); // Yapılan değişiklikleri veritabanına kaydeder ve kaydedilen değişiklik sayısını döndürür (Non-Asenkron)
			_transaction.Commit(); // Transaction'ı commit eder (Non-Asenkron)
			return result;
		}

		// Yeni bir transaction başlatır (Asenkron)
		public async Task BeginTransactionAsync()
		{
			_transaction = await _context.Database.BeginTransactionAsync(); // Yeni bir transaction başlatır (Asenkron)
		}

		// Yeni bir transaction başlatır (Non-Asenkron)
		public void BeginTransaction()
		{
			_transaction = _context.Database.BeginTransaction(); // Yeni bir transaction başlatır (Non-Asenkron)
		}

		// Transaction'ı rollback eder (Asenkron)
		public async Task RollbackAsync()
		{
			await _transaction.RollbackAsync(); // Transaction'ı rollback eder (Asenkron)
		}

		// Transaction'ı rollback eder (Non-Asenkron)
		public void Rollback()
		{
			_transaction.Rollback(); // Transaction'ı rollback eder (Non-Asenkron)
		}

		// Kullanılan kaynakları serbest bırakır
		public void Dispose()
		{
			_transaction?.Dispose(); // Transaction'ı dispose eder
			_context.Dispose(); // Context'i dispose eder
		}
	}
}
