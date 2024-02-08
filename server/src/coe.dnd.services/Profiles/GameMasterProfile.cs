using AutoMapper;
using coe.dnd.dal.Models;
using coe.dnd.services.DataTransferObjects;
using coe.dnd.services.Extensions;

namespace coe.dnd.services.Profiles;

public class GameMasterProfile : Profile
{
    public GameMasterProfile()
    {
        CreateMap<GameMaster, GameMasterDto>();

        CreateMap<GameMasterDto, GameMaster>()
            .ForMember(d => d.Id, o => o.Ignore())
            .IgnoreAllNull();
    }
}