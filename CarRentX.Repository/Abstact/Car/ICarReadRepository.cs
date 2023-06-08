using CarRentX.BaseRepository.Abstract;
using CarRentX.Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRentX.Repository.Abstact
{
	public interface ICarReadRepository:IReadRepository<Car,int>
	{
	}
}
