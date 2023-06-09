using CarRentX.ContextDb;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CarRentX.Repository
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
			return services;
		}
	}
}
