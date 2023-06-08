using CarRentX.Mapping.Abstract;
using Mapster;

namespace CarRentX.Mapping.Concrete
{
	public class Mapster : IMapping
	{
		public TDestination Map<TSource, TDestination>(TSource source)
		{
			return source.Adapt<TDestination>();
		}
	}
}
