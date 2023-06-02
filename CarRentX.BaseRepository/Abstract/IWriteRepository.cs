using CarRentX.BaseEntity;

namespace CarRentX.BaseRepository.Abstract
{
	public interface IWriteRepository<TEntity, T> : IRepository<TEntity, T> where TEntity : BaseEntity<T>
	{
		// Asenkron olarak bir varlık nesnesini veritabanına eklemek için kullanılır.
		// Geri dönüş değeri, ekleme işleminin başarılı olup olmadığını belirtir.
		Task<bool> AddAsync(TEntity model);

		// Bir varlık nesnesini veritabanına eklemek için kullanılır.
		// Geri dönüş değeri, ekleme işleminin başarılı olup olmadığını belirtir.
		bool Add(TEntity model);

		// Bir varlık nesnesini veritabanında güncellemek için kullanılır.
		// Geri dönüş değeri, güncelleme işleminin başarılı olup olmadığını belirtir.
		bool Update(TEntity model);

		// Bir varlık nesnesini veritabanından silmek için kullanılır.
		// Geri dönüş değeri, silme işleminin başarılı olup olmadığını belirtir.
		bool Remove(TEntity model);

		// Asenkron olarak bir varlık nesnesini veritabanından silmek için kullanılır.
		// Geri dönüş değeri, silme işleminin başarılı olup olmadığını belirtir.
		// T tipinde bir kimlik değeri alır, bu kimlik değeriyle ilgili varlık nesnesi veritabanından bulunur ve silinir.
		Task<bool> RemoveAsync(T id);

	}
}
