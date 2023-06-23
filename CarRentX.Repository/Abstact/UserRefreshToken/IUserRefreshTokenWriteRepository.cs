using CarRentX.BaseRepository.Abstract;
using CarRentX.Entity.Concrete;

namespace CarRentX.Repository.Abstact
{
	public interface IUserRefreshTokenWriteRepository:IWriteRepository<UserRefreshToken,int>
	{
	}
}
