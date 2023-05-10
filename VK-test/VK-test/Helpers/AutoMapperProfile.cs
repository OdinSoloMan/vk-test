using AutoMapper;
using Infrastructure.Models;
using VK_test.Models;

namespace VK_test.Helpers
{
    /// <summary>
    /// Кастомный маппер
    /// </summary>
    public class AutoMapperProfile : Profile
    {
        /// <summary>
        /// Конструктор AutoMapperProfile
        /// </summary>
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
