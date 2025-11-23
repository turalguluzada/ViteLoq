using AutoMapper;
using ViteLoq.Application.DTOs.UserManagement;
using ViteLoq.Domain.Entities;

namespace ViteLoq.Application.Mappings;

public class UserMappingProfile : Profile
{
    public UserMappingProfile()
    {
        CreateMap<CreateUserDto, AppUser>()
            .ForMember(dest => dest.UserName,
                opt => opt.MapFrom(src => string.IsNullOrWhiteSpace(src.UserName) ? src.Email : src.UserName))
            .ForMember(dest => dest.NormalizedUserName,
                opt => opt.MapFrom(src =>
                    (string.IsNullOrWhiteSpace(src.UserName) ? src.Email : src.UserName).ToUpperInvariant()))
            .ForMember(dest => dest.NormalizedEmail,
                opt => opt.MapFrom(src => src.Email.ToUpperInvariant()))
            .ForMember(dest => dest.Id, opt => opt.Ignore()); // service sets Id
            // .ForMember(dest => dest.CreatedAt, opt => opt.Ignore());
            CreateMap<UserDetailDto, UserDetail>()
                .ForMember(d => d.Id, opt => opt.Ignore());
            // .ForMember(d => d.CreatedAt, opt => opt.Ignore())
            // .ForMember(d => d.UpdatedAt, opt => opt.Ignore());

        // reverse / read mappings if needed:
        CreateMap<UserDetail, UserDetailDto>();
        CreateMap<AppUser, AppUserDto>();
        CreateMap<AppUser, UserProfileDto>();
        // Entity -> DTO (profile)
        // CreateMap<AppUser, UserProfileDto>()
        //     .ForMember(d => d.UserId, o => o.MapFrom(s => s.Id))
        //     .ForMember(d => d.UserName, o => o.MapFrom(s => s.UserName))
        //     .ForMember(d => d.Email, o => o.MapFrom(s => s.Email));
        // .ForMember(d => d.Detail, opt => opt.Ignore()); // fill detail separately
    }
}