using AutoMapper;
using Menu.API.DataTransferObjects;
using Menu.API.Models;

namespace Menu.API.Mappers
{
    public class ModelsMapperProfile : Profile
    {
        public ModelsMapperProfile()
        {
            CreateMap<Food, FoodDto>();

            CreateMap<FoodDto, Food>();

            CreateMap<Category, CategoryDto>();

            CreateMap<CategoryDto, Category>();

            CreateMap<FoodPicture, FoodPictureDto>()
                .ForMember(x => x.FilePath,
                    map => map.MapFrom(m => Folders.UploadFilesPath + m.FileName));

            CreateMap<FoodPictureDto, FoodPicture>()
                .ForMember(x => x.FileName,
                    map => map.MapFrom(m => m.FilePath));
        }
    }
}
