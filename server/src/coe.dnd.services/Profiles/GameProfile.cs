using AutoMapper;
using coe.dnd.dal.Models;
using coe.dnd.services.DataTransferObjects;
using coe.dnd.services.Extensions;

namespace coe.dnd.services.Profiles;

public class GameProfile : Profile
{
    public GameProfile()
    {
        CreateMap<Game, GameDto>()
            .ForMember(d => d.GameMaster, s => s.MapFrom(x => x.GameMaster))
            .ForMember(d => d.Campaign, s => s.MapFrom(x => x.Campaign));

        CreateMap<GameDto, Game>()
            .ForMember(d => d.Id, o => o.Ignore())
            .ForMember(d => d.GameMasterId, o => o.MapFrom(x => x.GameMaster.Id))
            .ForMember(d => d.CampaignId, o => o.MapFrom(x => x.Campaign.Id))
            .IgnoreAllNull();
        CreateMap<GameDto, GameCharacter>()
            .ForMember(d => d.Id, o => o.Ignore())
            .ForMember(d => d.GameId, o => o.MapFrom(x => x.Id));
    }
}