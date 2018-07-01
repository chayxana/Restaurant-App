using System.Diagnostics.CodeAnalysis;
using AutoMapper;

namespace Restaurant.Server.Api.Mappers
{
	[ExcludeFromCodeCoverage]
	public class AutoMapperConfiguration
	{
		public void Configure()
		{
            Mapper.Reset();
			Mapper.Initialize(x => { x.AddProfile<RestaurantModelsToDtoProfile>(); });
		}
	}
}