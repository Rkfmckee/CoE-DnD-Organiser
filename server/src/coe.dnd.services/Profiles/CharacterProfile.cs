using AutoMapper;
using coe.dnd.dal.Models;
using coe.dnd.services.DataTransferObjects;
using coe.dnd.services.Extensions;

namespace coe.dnd.services.Profiles;

public class CharacterProfile : Profile
{
    public CharacterProfile()
    {
        CreateMap<Character, CharacterDto>()
            .ForMember(d => d.Player, s => s.MapFrom(x => x.Player));

        CreateMap<CharacterDto, Character>()
            .ForMember(d => d.Id, o => o.Ignore())
            .ForMember(d => d.PlayerId, o => o.MapFrom(x => x.Player.Id))
            .IgnoreAllNull();
        CreateMap<CharacterDto, GameCharacter>()
            .ForMember(d => d.Id, o => o.Ignore())
            .ForMember(d => d.CharacterId, o => o.MapFrom(x => x.Id));
    }
}