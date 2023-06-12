using CarRentX.ContextDb;
using CarRentX.Manager.Abstact;
using CarRentX.Manager.Concrete;
using CarRentX.Mapping.Abstract;
using CarRentX.Mapping.Concrete;
using CarRentX.Service.Abstract;
using CarRentX.Service.Concrete;
using CarRentX.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CarRentX.Manager
{
	public static class ManagerServiceRegistration
	{
		public static IServiceCollection AddManagerServices(this IServiceCollection services, IConfiguration configuration)
		{
			services.AddDataAccessService(configuration);
			services.AddDbContext<RentCarXEfDbContext>();
			services.AddScoped<IMapping, CarRentX.Mapping.Concrete.Mapster>();
			services.AddScoped<ICarService, CarService>();
			services.AddScoped<ICarManager, CarManager>();
			return services;
		}
	}
}
