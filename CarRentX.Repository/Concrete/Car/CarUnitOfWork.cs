using CarRentX.BaseRepository.Abstract;
using CarRentX.BaseRepository.Concrete;
using CarRentX.ContextDb;
using CarRentX.Entity;
using CarRentX.Repository.Abstact;
using CarRentX.UnitOfWork.Concrete;

namespace CarRentX.Repository.Concrete
{
	public class CarUnitOfWork : UnitOfWork<RentCarXEfDbContext>, ICarUnitOfWork
	{
		private readonly RentCarXEfDbContext _context;
		public CarUnitOfWork(RentCarXEfDbContext context) : base(context)
		{
			_context = context;
		}

		private IReadRepository<Car, int> _carReadRepository;
		// CarReadRepository adında IReadRepository<Car,int> tipinde bir özellik
		// CarReadRepository, Car türündeki nesnelerin okunması için kullanılacak olan bir arabirimdir
		public IReadRepository<Car, int> CarReadRepository
		{
			//??= ifadesi, null birleştirme atama operatörüdür. Bu operatör, sol tarafındaki değişkenin null olması durumunda, sağ tarafındaki ifadeyi değer atama operatörü (=) ile atar
			get { return _carReadRepository ??= new ReadRepository<Car, RentCarXEfDbContext, int>(_context); }
		}

		private IWriteRepository<Car, int> _carWriteRepository;
		// CarWriteRepository adında IWriteRepository<Car,int> tipinde bir özellik
		// CarWriteRepository, Car türündeki nesnelerin yazılması (eklenmesi/güncellenmesi/silinmesi) için kullanılacak olan bir arabirimdir
		public IWriteRepository<Car, int> CarWriteRepository
		{
			get { return _carWriteRepository ??= new WriteRepository<Car, RentCarXEfDbContext, int>(_context); }
		}

		// Değişiklikleri veritabanına kaydederek gerçekleştirilen değişiklik sayısını döndüren metot
		public int Commit()
		{
			return _context.SaveChanges();
		}
		// Değişiklikleri asenkron olarak veritabanına kaydederek gerçekleştirilen değişiklik sayısını döndüren metot
		public async Task<int> CommitAsync()
		{
			return await _context.SaveChangesAsync();
		}
	}
}
