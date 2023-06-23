using CarRentX.BaseRepository.Abstract;
using CarRentX.Entity.Concrete;
using System.Linq.Expressions;

namespace CarRentX.Repository.Abstact
{
	public interface IUserRefreshTokenReadRepository:IReadRepository<UserRefreshToken,int>
	{
		//FUNC = DELEGE entity alacak geriye bool dönecek     where(x=>x.id>5= x kısmı tentity x.id>5 bool kısmını temsil etmektedir.
		IEnumerable<UserRefreshToken> Where(Expression<Func<UserRefreshToken, bool>> predicate);
	}
}
