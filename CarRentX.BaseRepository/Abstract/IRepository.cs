using CarRentX.BaseEntity;
using Microsoft.EntityFrameworkCore;

namespace CarRentX.BaseRepository.Abstract
{
	public  interface IRepository<TEntity, T> where TEntity:BaseEntity<T>
	{
		DbSet<TEntity> Table { get; }
	}
}
