using AutoMapper;
using coe.dnd.dal.Models;
using coe.dnd.services.DataTransferObjects;

namespace coe.dnd.services.Profiles;

public class CharacterProfile : Profile
{
    public CharacterProfile()
    {
        CreateMap<Character, CharacterDto>()
            .ForMember(d => d.Player, s => s.MapFrom(x => x.Player));

        CreateMap<CharacterDto, Character>()
            .ForMember(d => d.Id, o => o.Ignore())
            .ForMember(d => d.PlayerId, o =>
            {
                o.PreCondition(src => src.PlayerId != null);
                o.MapFrom(src => src.PlayerId);
            });
        
        CreateMap<CharacterDto, GameCharacter>()
            .ForMember(d => d.Id, o => o.Ignore())
            .ForMember(d => d.CharacterId, o => o.MapFrom(x => x.Id));
    }
}