using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRentX.UnitOfWork.Abstract
{
	public interface IUnitOfWork:IDisposable
	{
		Task<int> SaveAsync();
		int Save();
	}
}
