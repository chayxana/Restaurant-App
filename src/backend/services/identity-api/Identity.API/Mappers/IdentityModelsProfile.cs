using System.Diagnostics.CodeAnalysis;
using AutoMapper;
using Identity.API.Model.DataTransferObjects;
using Identity.API.Model.Entities;

namespace Identity.API.Mappers
{
    [ExcludeFromCodeCoverage]
    public class IdentityModelsProfile : Profile
    {
        public IdentityModelsProfile()
        {
            CreateMap<UserProfile, UserProfileDto>();

            CreateMap<ApplicationUser, UserDto>()
                .ForMember(x => x.Email, map => map.MapFrom(x => x.Email))
                .ForMember(x => x.Profile, map => map.MapFrom(x => x.UserProfile));
        }
    }
}
