using CarRentX.BaseEntity;

namespace CarRentX.BaseRepository.Abstract
{
	public interface IReadRepository<TEntity, T> : IRepository<TEntity, T> where TEntity : BaseEntity<T>
	{
		// Varlık nesnelerinin tümünü (bir IQueryable<TEntity> olarak) asenkron olarak almak için kullanılır.
		// 'tracking' parametresi, izleme (tracking) davranışını belirler. Varsayılan olarak true olarak ayarlanır.
		// Eğer tracking = true olarak ayarlanırsa, alınan varlık nesneleri değiştirildiğinde otomatik olarak takip edilir.
		// Eğer tracking = false olarak ayarlanırsa, varlık nesnelerinin değişiklikleri izlenmez ve takip edilmez.
		// Bu metod genellikle veritabanından varlık nesnelerini okuma işlemleri için kullanılır.
		// Geri dönen IQueryable<TEntity> sonucu, sorgulamaların yapılabileceği ve sonuçların kullanılabileceği bir sorgu nesnesidir.
		IQueryable<TEntity> GetAll(bool tracking = false);
		Task<TEntity> GetByIdAsync(T id,bool tracking=false);
	}
}
