using CarRentX.UnitOfWork.Abstract;
using Microsoft.EntityFrameworkCore;

namespace CarRentX.UnitOfWork.Concrete
{
	// TContext tipinde bir DbContext'e bağlı olarak çalışan bir UnitOfWork sınıfı
	public class UnitOfWork<TContext> : IUnitOfWork where TContext : DbContext
	{
		private readonly TContext _context;

		public UnitOfWork(TContext context)
		{
			_context = context;
		}
		// Değişiklikleri veritabanına kaydeder ve kaydedilen değişiklik sayısını döndürür (Asenkron)
		public async Task<int> SaveAsync() => await _context.SaveChangesAsync();

		// Değişiklikleri veritabanına kaydeder ve kaydedilen değişiklik sayısını döndürür (Non-Asenkron)
		public int Save() => _context.SaveChanges();

		// Kullanılan kaynakları serbest bırakır
		public void Dispose()=> _context.Dispose();

	}
}
