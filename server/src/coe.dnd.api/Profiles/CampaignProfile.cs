using AutoMapper;
using coe.dnd.api.Extensions;
using coe.dnd.api.ViewModels.Campaigns;
using coe.dnd.services.DataTransferObjects;

namespace coe.dnd.api.Profiles;

public class CampaignProfile : Profile
{
    public CampaignProfile()
    {
        CreateMap<CampaignViewModel, CampaignDto>();
        CreateMap<CreateCampaignViewModel, CampaignDto>();
        CreateMap<UpdateCampaignViewModel, CampaignDto>().IgnoreAllNull();

        CreateMap<CampaignDto, CampaignViewModel>();
        CreateMap<CampaignDto, CreateCampaignViewModel>();
        CreateMap<CampaignDto, UpdateCampaignViewModel>();
    }
}