using CarRentX.BaseEntity;
using CarRentX.BaseRepository.Abstract;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace CarRentX.BaseRepository.Concrete
{
	public class WriteRepository<TEntity, TContext, T> : IWriteRepository<TEntity, T>, IRepository<TEntity, T>
		where TEntity : BaseEntity<T>
		where TContext : DbContext
	{
		protected TContext _context { get; }
		public WriteRepository(TContext context)
		{
			_context = context;
		}
		public DbSet<TEntity> Table => _context.Set<TEntity>();

		public async Task<bool> AddAsync(TEntity model)
		{
			EntityEntry<TEntity> entityEntry = await Table.AddAsync(model);
			return entityEntry.State == EntityState.Added;
		}

		public bool Add(TEntity model)
		{
			EntityEntry<TEntity> entityEntry = Table.Add(model);
			return entityEntry.State == EntityState.Added;
		}

		public bool Update(TEntity model)
		{
			EntityEntry<TEntity> entityEntry=Table.Update(model);
			return entityEntry.State == EntityState.Modified;
		}

		public bool Remove(TEntity model)
		{
			EntityEntry<TEntity> entityEntry=Table.Remove(model);
			return entityEntry?.State == EntityState.Deleted;
		}

		public async Task<bool> RemoveAsync(T id)
		{
			TEntity? model = await Table.FirstOrDefaultAsync(x => x.Id.Equals(id));
			if(model != null)
			{
				return Remove(model);
			}
			return false;
		}
	}
}
