using CarRentX.BaseRepository.Concrete;
using CarRentX.ContextDb;
using CarRentX.Entity.Concrete;
using CarRentX.Repository.Abstact;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;

namespace CarRentX.Repository.Concrete
{
	public class CarReadRepository : ReadRepository<Car, RentCarXEfDbContext, int>, ICarReadRepository
	{
		public CarReadRepository(RentCarXEfDbContext context) : base(context)
		{
		}
		public IEnumerable<Car> GetCarsByColorId(int id)
		{
			var query = _context.Cars.Where(b => b.ColorId == id);
			return query;
		}
		public IEnumerable<Car> GetCarsByBrandId(int id)
		{
			var query = _context.Cars.Where(b => b.BrandId == id);
			return query;
		}

		public async Task<IEnumerable<Car>> GetAllWithBrandColorAsync()
		{
			var result = from car in _context.Cars
						 join brand in _context.Brands on car.BrandId equals brand.Id
						 join color in _context.Colors on car.ColorId equals color.Id

						 select new Car
						 {
							 Color=color,
							 Brand = brand,
							 DailyPrice = car.DailyPrice,
							 ColorId = car.ColorId,
							 BrandId = car.BrandId,
							 IsDeleted = car.IsDeleted,
							 CreatedDateTime = car.CreatedDateTime,
							 Description = car.Description,
							 Id = car.Id,
							 ModelYear = car.ModelYear,
							 Name = car.Name,
						 };
			return await result.ToListAsync();
		}
	}
}