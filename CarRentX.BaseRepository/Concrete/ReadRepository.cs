﻿using CarRentX.BaseEntity;
using CarRentX.BaseRepository.Abstract;
using Microsoft.EntityFrameworkCore;

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

		public IQueryable<TEntity> GetAll(bool tracking = false)
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
