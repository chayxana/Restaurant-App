using System.Diagnostics.CodeAnalysis;
using AutoMapper;
using AutoMapper.Configuration;

namespace Restaurant.Core.Mappers
{
    [ExcludeFromCodeCoverage]
    public static class AutoMapperConfiguration
    {
        public static IMapper Configure()
        {
            var configurationExpression = new MapperConfigurationExpression();
            configurationExpression.AddProfile<ViewModelToDataTransferObjectsProfile>();

            var configuration = new MapperConfiguration(configurationExpression);
            return new Mapper(configuration);
        }
    }
}