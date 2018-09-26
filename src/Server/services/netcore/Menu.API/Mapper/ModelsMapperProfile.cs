using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Menu.API.Models;

namespace Menu.API.Mappers
{
    public class ModelsMapperProfile : Profile
    {
        public ModelsMapperProfile()
        {
            CreateMap<Food, FoodDto>()
                .ForMember(x => x.CategoryDto,
                    map => map.MapFrom(x => Mapper.Map<CategoryDto>(x.Category)))
                .ForMember(x => x.Picture,
                    map => map.MapFrom(x => Folders.UploadFilesPath + x.Picture));

            CreateMap<FoodDto, Food>()
                .ForMember(x => x.Category,
                    map => map.MapFrom(x => Mapper.Map<Category>(x.CategoryDto)))
                .ForMember(x => x.Picture,
                    map => map.MapFrom(x => x.Picture.Contains(Folders.UploadFilesPath)
                        ? x.Picture.Replace(Folders.UploadFilesPath, "")
                        : x.Picture));

            CreateMap<Category, CategoryDto>();

            CreateMap<CategoryDto, Category>();
        }
    }
}
