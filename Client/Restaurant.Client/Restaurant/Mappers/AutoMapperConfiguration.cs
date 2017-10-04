using AutoMapper;

namespace Restaurant.Mappers
{
    public static class AutoMapperConfiguration
    {
        public static void Configure()
        {
            Mapper.Initialize(x =>
            {
                x.AddProfile<ViewModelToDataTransferObjectsProfile>();
            });
        }
    }
}
