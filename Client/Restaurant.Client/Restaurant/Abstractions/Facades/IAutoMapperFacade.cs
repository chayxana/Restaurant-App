namespace Restaurant.Abstractions.Facades
{
	public interface IAutoMapperFacade
	{
		TDestination Map<TDestination>(object source);

		TDestination Map<TSource, TDestination>(TSource source);
	}
}