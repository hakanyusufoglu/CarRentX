using CarRentX.ContextDb.Configuration;
using CarRentX.Entity.Concrete;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CarRentX.ContextDb
{
	public class RentCarXEfDbContext:IdentityDbContext<UserApp,RoleApp,string>
	{
		public RentCarXEfDbContext(DbContextOptions options):base(options)
		{

		}
		public virtual DbSet<Car> Cars { get; set; }
		public virtual DbSet<Brand> Brands { get; set; }
		public virtual DbSet<Color> Colors { get; set; }
		public virtual DbSet<Customer> Customers { get; set; }
		public virtual DbSet<Rental> Rentals { get; set; }
		public virtual DbSet<UserRefreshToken> UserRefreshTokens { get; set; }
		protected override void OnModelCreating(ModelBuilder builder)
		{
			builder.ApplyConfiguration(new BrandConfiguration());
			builder.ApplyConfiguration(new CarConfiguration());
		base.OnModelCreating(builder);
		}
	}
}
