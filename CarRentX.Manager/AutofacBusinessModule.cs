using Autofac;
using Autofac.Extras.DynamicProxy;
using CarRentX.Aspect.Interceptors;
using CarRentX.ContextDb;
using CarRentX.Manager.Abstact;
using CarRentX.Manager.Concrete;
using CarRentX.Mapping.Abstract;
using CarRentX.Service.Abstract;
using CarRentX.Service.Concrete;
using CarRentX.UnitOfWork;
using CarRentX.UnitOfWork.Abstract;
using CarRentX.UnitOfWork.Concrete;
using CarRentX.Validation.Car;
using CarRentX.ViewModel.Car;
using Castle.DynamicProxy;
using FluentValidation;
using Microsoft.Extensions.Configuration;

namespace CarRentX.Manager
{
	public class AutofacBusinessModule:Module
	{
		private readonly IConfiguration _configuration;

		public AutofacBusinessModule(IConfiguration configuration)
		{
			_configuration = configuration;
		}

		protected override void Load(ContainerBuilder builder)
		{
			
			builder.RegisterModule(new AutofacDataAccessesModule(_configuration));
			// DbContext'i kaydet

			builder.RegisterType<CarRentX.Mapping.Concrete.Mapster>()
				.As<IMapping>()
				.InstancePerLifetimeScope();

			builder.RegisterType<CarService>()
				.As<ICarService>()
				.InstancePerLifetimeScope();

			builder.RegisterType<CarManager>()
				.As<ICarManager>()
				.InstancePerLifetimeScope();

			builder.RegisterType<CustomerService>()
				.As<ICustomerService>()
				.InstancePerLifetimeScope();

			builder.RegisterType<CustomerManager>()
				.As<ICustomerManager>()
				.InstancePerLifetimeScope();

			builder.RegisterType<BrandService>()
				.As<IBrandService>()
				.InstancePerLifetimeScope();

			builder.RegisterType<BrandManager>()
				.As<IBrandManager>()
				.InstancePerLifetimeScope();

			builder.RegisterType<ColorService>()
				.As<IColorService>()
				.InstancePerLifetimeScope();

			builder.RegisterType<ColorManager>()
				.As<IColorManager>()
				.InstancePerLifetimeScope();

			builder.RegisterType<RentalService>()
				.As<IRentalService>()
				.InstancePerLifetimeScope();

			builder.RegisterType<RentalManager>()
				.As<IRentalManager>()
				.InstancePerLifetimeScope();

			builder.RegisterType<CarViewModelValidator>()
				.As<IValidator<CarViewModel>>()
				.InstancePerLifetimeScope();

			var assembly = System.Reflection.Assembly.GetExecutingAssembly();

			builder.RegisterAssemblyTypes(assembly).AsImplementedInterfaces()
				.EnableInterfaceInterceptors(new ProxyGenerationOptions()
				{
					Selector = new AspectInterceptorSelector()
				}).SingleInstance();

		}
	}
}
