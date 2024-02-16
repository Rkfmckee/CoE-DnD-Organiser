using AutoMapper;
using coe.dnd.dal.Models;
using coe.dnd.services.DataTransferObjects;

namespace coe.dnd.services.Profiles;

public class CampaignProfile : Profile
{
    public CampaignProfile()
    {
        CreateMap<Campaign, CampaignDto>();

        CreateMap<CampaignDto, Campaign>()
            .ForMember(d => d.Id, o => o.Ignore())
            .ForMember(d => d.Name, o =>
            {
                o.PreCondition(src => !string.IsNullOrEmpty(src.Name));
                o.MapFrom(src => src.Name);
            })
            .ForMember(d => d.Theme, o =>
            {
                o.PreCondition(src => !string.IsNullOrEmpty(src.Theme));
                o.MapFrom(src => src.Theme);
            })
            .ForMember(d => d.Details, o =>
            {
                o.PreCondition(src => !string.IsNullOrEmpty(src.Details));
                o.MapFrom(src => src.Details);
            })
            .ForMember(d => d.Writer, o =>
            {
                o.PreCondition(src => !string.IsNullOrEmpty(src.Writer));
                o.MapFrom(src => src.Writer);
            });
    }
}