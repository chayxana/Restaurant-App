namespace Restaurant.Abstractions.Facades
{
    public interface IAutoMapperFacade
    {
        TDestination Map<TDestination>(object source);

        TDestination Map<TDestination, TSource>(TSource source);
    }
}
