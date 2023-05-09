using AutoMapper;
using Infrastructure.Models;
using VK_test.Models;

namespace VK_test.Helpers
{
    public class AutoMapperProfile: Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Users, UserInfo>()
                .ForMember(
                    dest => dest.UserGroup,
                    opt => opt.MapFrom(src => src.UsersGroup!.Code)
                )
                .ForMember(
                    dest => dest.UserState,
                    opt => opt.MapFrom(src => src.UsersState!.Code)
                );
        }
    }
}
