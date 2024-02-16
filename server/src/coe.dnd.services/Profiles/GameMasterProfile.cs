using AutoMapper;
using coe.dnd.dal.Models;
using coe.dnd.services.DataTransferObjects;

namespace coe.dnd.services.Profiles;

public class GameMasterProfile : Profile
{
    public GameMasterProfile()
    {
        CreateMap<GameMaster, GameMasterDto>();

        CreateMap<GameMasterDto, GameMaster>()
            .ForMember(d => d.Id, o => o.Ignore())
            .ForMember(d => d.PlayerId, o =>
            {
                o.PreCondition(src => src.PlayerId != null);
                o.MapFrom(src => src.PlayerId);
            })
            .ForMember(d => d.PlanningNotes, o =>
            {
                o.PreCondition(src => !string.IsNullOrEmpty(src.PlanningNotes));
                o.MapFrom(src => src.PlanningNotes);
            });
    }
}