using CarRentX.Entity.Concrete;
using Microsoft.EntityFrameworkCore;

namespace CarRentX.ContextDb
{
	public class RentCarXEfDbContext:DbContext
	{
		public RentCarXEfDbContext(DbContextOptions options):base(options)
		{

		}
		public virtual DbSet<Car> Cars { get; set; }
		public virtual DbSet<Brand> Brands { get; set; }
		public virtual DbSet<Color> Colors { get; set; }
	}
}
