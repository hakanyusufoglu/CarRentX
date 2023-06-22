using Autofac;
using CarRentX.BaseRepository.Concrete;
using CarRentX.ContextDb;
using CarRentX.Repository.Abstact;
using CarRentX.Repository.Concrete;
using CarRentX.UnitOfWork.Abstract;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using CarRentX.BaseRepository.Abstract;
using CarRentX.Entity.Concrete;

namespace CarRentX.UnitOfWork
{
	public class AutofacDataAccessesModule : Module
	{
		private readonly IConfiguration _configuration;

		public AutofacDataAccessesModule(IConfiguration configuration)
		{
			_configuration = configuration;
		}

		protected override void Load(ContainerBuilder builder)
		{

			builder.Register(c =>
			{
				var optionsBuilder = new DbContextOptionsBuilder<RentCarXEfDbContext>();
				optionsBuilder.UseSqlServer(_configuration.GetConnectionString("RentCarXConnectionTestString"));
				optionsBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
				return new RentCarXEfDbContext(optionsBuilder.Options);
			})
			   .AsSelf()
			   .InstancePerLifetimeScope();

			builder.RegisterType<UnitOfWork.Concrete.UnitOfWork>()
				.As<IUnitOfWork>()
				.InstancePerLifetimeScope();

			builder.RegisterGeneric(typeof(ReadRepository<,,>))
				.As(typeof(IReadRepository<,>))
				.InstancePerLifetimeScope();

			builder.RegisterGeneric(typeof(WriteRepository<,,>))
				.As(typeof(IWriteRepository<,>))
				.InstancePerLifetimeScope();

			builder.RegisterType<CarReadRepository>()
				.As<ICarReadRepository>()
				.InstancePerLifetimeScope();

			builder.RegisterType<CarWriteRepository>()
				.As<ICarWriteRepository>()
				.InstancePerLifetimeScope();

			builder.RegisterType<RentalReadRepository>()
				.As<IRentalReadRepository>()
				.InstancePerLifetimeScope();

			builder.RegisterType<RentalWriteRepository>()
				.As<IRentalWriteRepository>()
				.InstancePerLifetimeScope();

			builder.RegisterType<BrandReadRepository>()
				.As<IBrandReadRepository>()
				.InstancePerLifetimeScope();

			builder.RegisterType<BrandWriteRepository>()
				.As<IBrandWriteRepository>()
				.InstancePerLifetimeScope();

			builder.RegisterType<ColorReadRepository>()
				.As<IColorReadRepository>()
				.InstancePerLifetimeScope();

			builder.RegisterType<ColorWriteRepository>()
				.As<IColorWriteRepository>()
				.InstancePerLifetimeScope();

			builder.RegisterType<CustomerReadRepository>()
				.As<ICustomerReadRepository>()
				.InstancePerLifetimeScope();

			builder.RegisterType<CustomerWriteRepository>()
				.As<ICustomerWriteRepository>()
				.InstancePerLifetimeScope();
		}
	}
}
