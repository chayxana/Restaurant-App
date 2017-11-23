using System.Diagnostics.CodeAnalysis;
using AutoMapper;

namespace Restaurant.Core.Mappers
{
    [ExcludeFromCodeCoverage]
    public static class AutoMapperConfiguration
    {
        public static void Configure()
        {
            Mapper.Initialize(x => { x.AddProfile<ViewModelToDataTransferObjectsProfile>(); });
        }
    }
}