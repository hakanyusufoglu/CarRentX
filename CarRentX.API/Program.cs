using Autofac;
using Autofac.Core;
using Autofac.Extensions.DependencyInjection;
using CarRentX.ContextDb;
using CarRentX.Entity.Concrete;
using CarRentX.Manager;
using Microsoft.AspNetCore.Identity;
using System;

namespace CarRentX.API
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			builder.Services.AddIdentity<UserApp, IdentityRole>(options => {
				options.User.RequireUniqueEmail = true;
				options.Password.RequireNonAlphanumeric = false;
			}).AddEntityFrameworkStores<RentCarXEfDbContext>().AddDefaultTokenProviders();

			//Add AutoFac
			builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory())
				.ConfigureContainer<ContainerBuilder>(builder =>
				{
					// Add services to the container.
					var configuration = new ConfigurationBuilder()
						.SetBasePath(Directory.GetCurrentDirectory())
						.AddJsonFile("appsettings.json")
						.Build();
					builder.RegisterModule(new AutofacBusinessModule(configuration));
				});
	
			builder.Services.AddControllers();
			// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
			builder.Services.AddEndpointsApiExplorer();
			builder.Services.AddSwaggerGen();

			var app = builder.Build();

			// Configure the HTTP request pipeline.
			if (app.Environment.IsDevelopment())
			{
				app.UseSwagger();
				app.UseSwaggerUI();
			}

			app.UseHttpsRedirection();

			app.UseAuthorization();


			app.MapControllers();

			app.Run();
		}
	}
}
