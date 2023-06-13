using CarRentX.Entity.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CarRentX.ContextDb.Configuration
{
	//Veri tabanı tablolarının bu şekilde configuration yapabileceğimi göstermek için eklenmiştir.
	public class BrandConfiguration : IEntityTypeConfiguration<Brand>
	{
		public void Configure(EntityTypeBuilder<Brand> builder)
		{
			builder.HasKey(x => x.Id);
			builder.Property(x => x.Id).UseIdentityColumn(1, 1);
		}
	}
}
