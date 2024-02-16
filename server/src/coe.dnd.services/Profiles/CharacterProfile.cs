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
            })
            .ForMember(d => d.Name, o =>
            {
                o.PreCondition(src => !string.IsNullOrEmpty(src.Name));
                o.MapFrom(src => src.Name);
            })
            .ForMember(d => d.Race, o =>
            {
                o.PreCondition(src => !string.IsNullOrEmpty(src.Race));
                o.MapFrom(src => src.Race);
            })
            .ForMember(d => d.ClassLevels, o =>
            {
                o.PreCondition(src => !string.IsNullOrEmpty(src.ClassLevels));
                o.MapFrom(src => src.ClassLevels);
            });

        CreateMap<CharacterDto, GameCharacter>()
            .ForMember(d => d.Id, o => o.Ignore())
            .ForMember(d => d.CharacterId, o => o.MapFrom(x => x.Id));
    }
}