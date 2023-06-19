using CarRentX.Entity.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CarRentX.ContextDb.Configuration
{
	internal class CarConfiguration : IEntityTypeConfiguration<Car>
	{
		public void Configure(EntityTypeBuilder<Car> builder)
		{
			builder.Property(c => c.DailyPrice).HasPrecision(18, 2);
		}
	}
}
