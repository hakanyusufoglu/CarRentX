namespace CarRentX.Mapping.Abstract
{
	public interface IMapping
	{
		/// <summary>
		/// TSource : Kaynak olarak göstereceğimiz nesne.
		/// TDestination : Bizim için ulaşmak istediğimiz nesne.
		/// Bir Entity'i bir DTO nesnesine maplemek istersek; TDestination-->DTO, TSource-->Entity 'e karşılık gelir.
		/// </summary>
		TDestination Map<TSource, TDestination>(TSource source);
	}
}
