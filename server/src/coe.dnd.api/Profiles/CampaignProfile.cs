using AutoMapper;
using coe.dnd.api.ViewModels.Campaigns;
using coe.dnd.dal.Models;

namespace coe.dnd.api.Profiles;

public class CampaignProfile : Profile
{
    public CampaignProfile()
    {
        CreateMap<Campaign, CampaignViewModel>();
        CreateMap<Campaign, CreateCampaignViewModel>();
        CreateMap<Campaign, UpdateCampaignViewModel>();

        CreateMap<CampaignViewModel, Campaign>();
        CreateMap<CreateCampaignViewModel, Campaign>();
        CreateMap<UpdateCampaignViewModel, Campaign>();
    }
}