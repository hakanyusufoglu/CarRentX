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
			services.AddScoped<ICarReadRepository,CarReadRepository>();
			services.AddScoped<ICarWriteRepository,CarWriteRepository>();
			return services;

		}
	}
}
