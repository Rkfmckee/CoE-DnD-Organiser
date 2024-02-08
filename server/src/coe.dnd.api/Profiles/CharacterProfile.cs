using AutoMapper;
using coe.dnd.api.Extensions;
using coe.dnd.api.ViewModels.Characters;
using coe.dnd.services.DataTransferObjects;

namespace coe.dnd.api.Profiles;

public class CharacterProfile : Profile
{
    public CharacterProfile()
    {
        CreateMap<CharacterViewModel, CharacterDto>();
        CreateMap<CreateCharacterViewModel, CharacterDto>();
        CreateMap<UpdateCharacterViewModel, CharacterDto>().IgnoreAllNull();

        CreateMap<CharacterDto, CharacterViewModel>();
        CreateMap<CharacterDto, CreateCharacterViewModel>();
        CreateMap<CharacterDto, UpdateCharacterViewModel>();
    }
}