using AutoMapper;
using coe.dnd.dal.Models;
using coe.dnd.services.DataTransferObjects;

namespace coe.dnd.services.Profiles;

public class PlayerProfile : Profile
{
    public PlayerProfile()
    {
        CreateMap<Player, PlayerDto>();

        CreateMap<PlayerDto, Player>()
            .ForMember(d => d.Id, o => o.Ignore())
            .ForMember(d => d.EmailAddress, o =>
            {
                o.PreCondition(src => !string.IsNullOrEmpty(src.EmailAddress));
                o.MapFrom(src => src.EmailAddress);
            })
            .ForMember(d => d.Name, o =>
            {
                o.PreCondition(src => !string.IsNullOrEmpty(src.Name));
                o.MapFrom(src => src.Name);
            })
            .ForMember(d => d.DateOfBirth, o =>
            {
                o.PreCondition(src => src.DateOfBirth != null);
                o.MapFrom(src => src.DateOfBirth);
            })
            .ForMember(d => d.Password, o =>
            {
                o.PreCondition(src => !string.IsNullOrEmpty(src.Password));
                o.MapFrom(src => src.Password);
            });
    }
}