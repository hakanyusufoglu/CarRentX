using CarRentX.BaseRepository.Concrete;
using CarRentX.ContextDb;
using CarRentX.Entity.Concrete;
using CarRentX.Repository.Abstact;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace CarRentX.Repository.Concrete
{
	public class UserRefreshTokenReadRepository : ReadRepository<UserRefreshToken, RentCarXEfDbContext, int>, IUserRefreshTokenReadRepository
	{
		public UserRefreshTokenReadRepository(RentCarXEfDbContext context) : base(context)
		{
		}

		public IEnumerable<UserRefreshToken> Where(Expression<Func<UserRefreshToken, bool>> predicate)
		{
			//IQuaraybble servis katmanında örneğin product geldi where komutunu kullandım order by komutunu kullandım vs. bunları ramde tutulmasını sağlayıp direkt olarak veritabanına işlemesini engellemektedir.
			//ToList() metoduyla birlikte örneğin: product.where(x=>x.id>5).firstdefaukt().tolist() dediğimizde işlemler veritabanına yansır.
			//IEnumurable ise direkt yansıtır ve bu yüzden ıenumarable bir veri geldiğinde üzerine sorgu yazılmaması tavsiye edilir.

			//mümkün olduğunca ıqurayble kullanmalı.
			return _context.UserRefreshTokens.Where(predicate);
		}
	}
}
