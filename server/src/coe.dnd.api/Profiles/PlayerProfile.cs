using AutoMapper;
using coe.dnd.api.Extensions;
using coe.dnd.api.ViewModels.Players;
using coe.dnd.services.DataTransferObjects;

namespace coe.dnd.api.Profiles;

public class PlayerProfile : Profile
{
    public PlayerProfile()
    {
        CreateMap<PlayerViewModel, PlayerDto>();
        CreateMap<CreatePlayerViewModel, PlayerDto>();
        CreateMap<UpdatePlayerViewModel, PlayerDto>().IgnoreAllNull();

        CreateMap<PlayerDto, PlayerViewModel>();
        CreateMap<PlayerDto, CreatePlayerViewModel>();
        CreateMap<PlayerDto, UpdatePlayerViewModel>();
    }
}