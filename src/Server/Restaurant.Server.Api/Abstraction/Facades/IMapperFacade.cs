namespace Restaurant.Server.Api.Abstraction.Facades
{
	public interface IMapperFacade
	{
		TDestination Map<TDestination, TSource>(TSource source);

		TDestination Map<TDestination>(object source);
	}
}