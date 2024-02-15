using AutoMapper;
using coe.dnd.dal.Models;
using coe.dnd.services.DataTransferObjects;

namespace coe.dnd.services.Profiles;

public class GameProfile : Profile
{
    public GameProfile()
    {
        CreateMap<Game, GameDto>()
            .ForMember(d => d.Characters, o => o.MapFrom(s => s.GameCharacters.Select(y => y.Character)));

        CreateMap<GameDto, Game>()
            .ForMember(d => d.Id, o => o.Ignore())
            .ForMember(d => d.GameMasterId, o =>
            {
                o.PreCondition(s => s.GameMasterId != null);
                o.MapFrom(s => s.GameMasterId);
            })
            .ForMember(d => d.CampaignId, o =>
            {
                o.PreCondition(src => src.CampaignId != null);
                o.MapFrom(src => src.CampaignId);
            });;
        
        CreateMap<GameDto, GameCharacter>()
            .ForMember(d => d.Id, o => o.Ignore())
            .ForMember(d => d.GameId, o => o.MapFrom(x => x.Id));

        CreateMap<GameCharacterDto, GameCharacter>();
    }
}