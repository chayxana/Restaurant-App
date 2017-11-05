namespace Restaurant.Server.Api.Abstractions.Facades
{
	public interface IMapperFacade
	{
		TDestination Map<TDestination, TSource>(TSource source);

		TDestination Map<TDestination>(object source);
	}
}