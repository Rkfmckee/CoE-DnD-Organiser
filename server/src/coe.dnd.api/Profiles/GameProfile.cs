using AutoMapper;
using coe.dnd.api.Extensions;
using coe.dnd.api.ViewModels.Games;
using coe.dnd.services.DataTransferObjects;

namespace coe.dnd.api.Profiles;

public class GameProfile : Profile
{
    public GameProfile()
    {
        CreateMap<GameViewModel, GameDto>();
        CreateMap<CreateGameViewModel, GameDto>();
        CreateMap<UpdateGameViewModel, GameDto>().IgnoreAllNull();

        CreateMap<GameDto, GameViewModel>();
        CreateMap<GameDto, CreateGameViewModel>();
        CreateMap<GameDto, UpdateGameViewModel>();
    }
}