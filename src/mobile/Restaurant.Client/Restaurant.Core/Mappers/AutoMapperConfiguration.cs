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
            Mapper.Initialize(configurationExpression);
            return Mapper.Instance;
        }
    }
}