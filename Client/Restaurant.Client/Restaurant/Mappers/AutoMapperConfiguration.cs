using AutoMapper;

namespace Restaurant.Mappers
{
    public class AutoMapperConfiguration
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
