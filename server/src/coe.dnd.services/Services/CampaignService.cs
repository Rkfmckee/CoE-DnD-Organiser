using AutoMapper;
using coe.dnd.dal.Interfaces;
using coe.dnd.dal.Models;
using coe.dnd.dal.Specifications.Campaigns;
using coe.dnd.services.Interfaces;
using Unosquare.EntityFramework.Specification.Common.Extensions;

namespace coe.dnd.services.Services;

public class CampaignService : ICampaignService
{
    private readonly IDndOrganiserDatabase _database;
    private readonly IMapper _mapper;

    public CampaignService(IDndOrganiserDatabase database, IMapper mapper)
    {
        _database = database;
        _mapper = mapper;
    }
    
    public Campaign GetCampaign(int id)
    {
        var campaign = _database.Get<Campaign>()
            .Where(new CampaignByIdSpec(id))
            .SingleOrDefault();

        return campaign;
    }

    public IList<Campaign> GetCampaigns(string name = null, string theme = null, string writer = null)
    {
        var campaigns = _database.Get<Campaign>()
            .Where(new CampaignSearchSpec(name, theme, writer))
            .ToList();

        return campaigns;
    }

    public void CreateCampaign(Campaign campaign)
    {
    }

    public void UpdateCampaign(int id, Campaign campaign)
    {
    }

    public void DeleteCampaign(int id)
    {
    }
}