using AutoMapper;
using coe.dnd.api.Extensions;
using coe.dnd.api.ViewModels.GameMasters;
using coe.dnd.services.DataTransferObjects;

namespace coe.dnd.api.Profiles;

public class GameMasterProfile : Profile
{
    public GameMasterProfile()
    {
        CreateMap<GameMasterViewModel, GameMasterDto>();
        CreateMap<CreateGameMasterViewModel, GameMasterDto>();
        CreateMap<UpdateGameMasterViewModel, GameMasterDto>().IgnoreAllNull();

        CreateMap<GameMasterDto, GameMasterViewModel>();
        CreateMap<GameMasterDto, CreateGameMasterViewModel>();
        CreateMap<GameMasterDto, UpdateGameMasterViewModel>();
    }
}