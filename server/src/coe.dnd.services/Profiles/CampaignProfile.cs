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
            .ForMember(d => d.Id, o => o.Ignore());
    }
}