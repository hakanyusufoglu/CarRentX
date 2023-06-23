using CarRentX.BaseRepository.Concrete;
using CarRentX.ContextDb;
using CarRentX.Entity.Concrete;
using CarRentX.Repository.Abstact;

namespace CarRentX.Repository.Concrete
{
	public class UserRefreshTokenWriteRepository : WriteRepository<UserRefreshToken, RentCarXEfDbContext, int>, IUserRefreshTokenWriteRepository
	{
		public UserRefreshTokenWriteRepository(RentCarXEfDbContext context) : base(context)
		{
		}
	}
}
