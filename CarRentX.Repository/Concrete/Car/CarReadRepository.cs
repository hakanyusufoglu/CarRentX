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
		public IEnumerable<Car> GetCarsByColorId(string id)
		{
			var query = _context.Cars.Where(b => b.ColorId == id);
			return query;
		}
		public IEnumerable<Car> GetCarsByBrandId(int id)
		{
			var query = _context.Cars.Where(b => b.BrandId == id);
			return query;
		}
	}
}