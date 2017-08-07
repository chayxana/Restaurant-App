using AutoMapper;

namespace Restaurant.Server.Api.Mappers
{
    public static class AutoMapperConfiguration
    {
        public static void Configure()
        {
            Mapper.Initialize(x =>
            {
                x.AddProfile<RestaurantModelsToDtoProfile>();
            });
        }
    }
}
