using AutoMapper;

namespace Restaurant.Server.Mappers
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
