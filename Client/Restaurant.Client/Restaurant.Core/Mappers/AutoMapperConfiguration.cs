using AutoMapper;

namespace Restaurant.Core.Mappers
{
	public static class AutoMapperConfiguration
	{
		public static void Configure()
		{
			Mapper.Initialize(x => { x.AddProfile<ViewModelToDataTransferObjectsProfile>(); });
		}
	}
}