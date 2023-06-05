using CarRentX.BaseEntity;
using CarRentX.BaseRepository.Abstract;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRentX.BaseRepository.Concrete
{
	public class ReadRepository<TEntity, TContext, T> : IReadRepository<TEntity, T>, IRepository<TEntity, T> 
		where TEntity:BaseEntity<T> 
		where TContext : DbContext
	{
		protected  TContext _context { get; }
		public ReadRepository(TContext context)
		{
			_context= context;
		}
		public DbSet<TEntity> Table => _context.Set<TEntity>();

		public async Task<IQueryable<TEntity>> GetAll(bool tracking = false)
		{
			var query = Table.AsQueryable();
			if (!tracking)
				query = query.AsNoTracking();
			return query;
		}

		public async Task<TEntity> GetByIdAsync(T id, bool tracking = false)
		{
			var query = Table.AsQueryable();
			if (!tracking)
				query = Table.AsNoTracking();
			return await query.FirstOrDefaultAsync(data => data.Id.Equals(id));
		}
	}
}
