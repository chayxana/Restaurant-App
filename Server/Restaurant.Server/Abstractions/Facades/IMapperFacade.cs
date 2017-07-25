using System.Linq;
using Restaurant.Server.Models;

namespace Restaurant.Server.Abstractions.Facades
{
    public interface IMapperFacade
    {
        TDestination Map<TDestination, TSource>(TSource source);

        TDestination Map<TDestination>(object source);
    }
}
