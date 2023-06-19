using CarRentX.ContextDb;
using CarRentX.UnitOfWork.Abstract;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using CarRentX.Repository.Abstact;
using CarRentX.BaseRepository.Concrete;
using CarRentX.Repository.Concrete;
using CarRentX.BaseRepository.Abstract;
using CarRentX.Entity.Concrete;

namespace CarRentX.UnitOfWork
{
	public static class DataAccessServiceRegistration
	{
		public static IServiceCollection AddDataAccessService(this IServiceCollection services, IConfiguration configuration)
		{
			services.AddDbContext<RentCarXEfDbContext>(options =>
			{
				options.UseSqlServer(configuration.GetConnectionString("RentCarXConnectionTestString"));
				options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
			});
			services.AddScoped<IUnitOfWork,UnitOfWork.Concrete.UnitOfWork>();
			
			services.AddScoped<IReadRepository<Car,int>,ReadRepository<Car, RentCarXEfDbContext,int>>();
			services.AddScoped<IWriteRepository<Car,int>,WriteRepository<Car, RentCarXEfDbContext,int>>();

			services.AddScoped<IReadRepository<Rental, int>, ReadRepository<Rental, RentCarXEfDbContext, int>>();
			services.AddScoped<IWriteRepository<Rental, int>, WriteRepository<Rental, RentCarXEfDbContext, int>>();

			services.AddScoped<IReadRepository<Brand, int>, ReadRepository<Brand, RentCarXEfDbContext, int>>();
			services.AddScoped<IWriteRepository<Brand, int>, WriteRepository<Brand, RentCarXEfDbContext, int>>();

			services.AddScoped<IReadRepository<Color, int>, ReadRepository<Color, RentCarXEfDbContext, int>>();
			services.AddScoped<IWriteRepository<Color, int>, WriteRepository<Color, RentCarXEfDbContext, int>>();

			services.AddScoped<IReadRepository<Customer, int>, ReadRepository<Customer, RentCarXEfDbContext, int>>();
			services.AddScoped<IWriteRepository<Customer, int>, WriteRepository<Customer, RentCarXEfDbContext, int>>();

			services.AddScoped<ICarReadRepository,CarReadRepository>();
			services.AddScoped<ICarWriteRepository,CarWriteRepository>();

			services.AddScoped<IRentalReadRepository, RentalReadRepository>();
			services.AddScoped<IRentalWriteRepository, RentalWriteRepository>();

			services.AddScoped<IBrandReadRepository, BrandReadRepository>();
			services.AddScoped<IBrandWriteRepository, BrandWriteRepository>();

			services.AddScoped<IColorReadRepository, ColorReadRepository>();
			services.AddScoped<IColorWriteRepository, ColorWriteRepository>();

			services.AddScoped<ICustomerReadRepository, CustomerReadRepository>();
			services.AddScoped<ICustomerWriteRepository, CustomerWriteRepository>();
			return services;

		}
	}
}
