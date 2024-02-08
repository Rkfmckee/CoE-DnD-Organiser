using AutoMapper;
using coe.dnd.dal.Models;
using coe.dnd.services.DataTransferObjects;
using coe.dnd.services.Extensions;

namespace coe.dnd.services.Profiles;

public class PlayerProfile : Profile
{
    public PlayerProfile()
    {
        CreateMap<Player, PlayerDto>();

        CreateMap<PlayerDto, Player>()
            .ForMember(d => d.Id, o => o.Ignore())
            .IgnoreAllNull();
    }
}