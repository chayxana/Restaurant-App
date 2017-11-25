using System.Diagnostics.CodeAnalysis;
using AutoMapper;
using Restaurant.Server.Abstraction.Facades;

namespace Restaurant.Server.Core.Facades
{
	[ExcludeFromCodeCoverage]
	public class MapperFacade : IMapperFacade
	{
		public TDestination Map<TDestination, TSource>(TSource source)
		{
			return Mapper.Map<TDestination>(source);
		}

		public TDestination Map<TDestination>(object source)
		{
			return Mapper.Map<TDestination>(source);
		}
	}
}