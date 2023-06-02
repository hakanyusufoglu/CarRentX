using CarRentX.BaseEntity;
using Microsoft.EntityFrameworkCore;

namespace CarRentX.BaseRepository.Abstract
{
	// Genel veritabanı işlemlerini gerçekleştirmek için IRepository arabirimini tanımlar.
	// TEntity, bir varlık nesnesini temsil ederken, T ise varlığın benzersiz bir kimlik türünü ifade eder.
	public interface IRepository<TEntity, T> where TEntity : BaseEntity<T>
	{
		// Veritabanındaki TEntity türündeki varlıkları temsil eden DbSet'i alır.
		DbSet<TEntity> Table { get; }
	}
}
