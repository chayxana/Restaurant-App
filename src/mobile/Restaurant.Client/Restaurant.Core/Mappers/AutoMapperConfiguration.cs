using System.Diagnostics.CodeAnalysis;
using AutoMapper;

namespace Restaurant.Core.Mappers
{
    [ExcludeFromCodeCoverage]
    public static class AutoMapperConfiguration
    {
        public static IMapper Configure()
        {
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<ViewModelToDataTransferObjectsProfile>();
            });
            return new Mapper(configuration);
        }
    }
}
