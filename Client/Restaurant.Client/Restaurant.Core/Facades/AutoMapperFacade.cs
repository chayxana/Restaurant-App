using AutoMapper;
using Restaurant.Abstractions.Facades;

namespace Restaurant.Core.Facades
{
    public class AutoMapperFacade : IAutoMapperFacade
    {
        public TDestination Map<TDestination>(object source)
        {
            return Mapper.Map<TDestination>(source);
        }

        public TDestination Map<TSource, TDestination>(TSource source)
        {
            return Mapper.Map<TSource, TDestination>(source);
        }
    }
}