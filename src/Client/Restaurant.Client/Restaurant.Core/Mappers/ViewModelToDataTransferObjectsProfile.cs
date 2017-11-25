using System.Diagnostics.CodeAnalysis;
using AutoMapper;
using Restaurant.Abstractions.ViewModels;
using Restaurant.Common.DataTransferObjects;
using Restaurant.Core.ViewModels;
using Restaurant.ViewModels;

namespace Restaurant.Core.Mappers
{
    [ExcludeFromCodeCoverage]
    public class ViewModelToDataTransferObjectsProfile : Profile
    {
        public ViewModelToDataTransferObjectsProfile()
        {
            //CreateMap<SignUpViewModel, RegisterDto>()
            //    .ForMember(x => x.UserName, map => map.MapFrom(x => x.UserName))
            //    .ForMember(x => x.Email, map => map.MapFrom(x => x.Email))
            //    .ForMember(x => x.Password, map => map.MapFrom(vm => vm.Password))
            //    .ForMember(x => x.ConfirmPassword, map => map.MapFrom(vm => vm.ConfirmPassword));

            CreateMap<SignInViewModel, LoginDto>()
                .ForMember(x => x.Login, map => map.MapFrom(vm => vm.Email))
                .ForMember(x => x.Password, map => map.MapFrom(vm => vm.Password));

            CreateMap<IOrderViewModel, OrderItemDto>()
                .ForMember(x => x.Quantity, map => map.MapFrom(x => x.Quantity))
                .ForMember(x => x.FoodId, map => map.MapFrom(x => x.Food.Id));
        }
    }
}