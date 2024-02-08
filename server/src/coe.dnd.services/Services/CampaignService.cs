using AutoMapper;
using coe.dnd.dal.Interfaces;
using coe.dnd.dal.Models;
using coe.dnd.dal.Specifications.Campaigns;
using coe.dnd.services.DataTransferObjects;
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

    public bool CampaignExists(int id)
    {
        return _database.Get<Campaign>()
            .Where(new CampaignByIdSpec(id))
            .Any();
    }
    
    public Campaign GetCampaign(int id)
    {
        return _database.Get<Campaign>()
            .Where(new CampaignByIdSpec(id))
            .SingleOrDefault();
    }
    
    public CampaignDto GetCampaignData(int id)
    {
        return _mapper.Map<CampaignDto>(GetCampaign(id));
    }

    public IList<Campaign> GetCampaigns(string name = null, string theme = null, string writer = null)
    {
        return _database.Get<Campaign>()
            .Where(new CampaignSearchSpec(name, theme, writer))
            .ToList();
    }
    
    public IList<CampaignDto> GetCampaignsData(string name = null, string theme = null, string writer = null)
    {
        return _mapper.Map<IList<CampaignDto>>(GetCampaigns(name, theme, writer));
    }

    public void CreateCampaign(CampaignDto campaignData)
    {
        var campaign = _mapper.Map<Campaign>(campaignData);
        campaign.Created = DateTime.Now;
        
        _database.Add(campaign);
        _database.SaveChanges();
    }

    public void UpdateCampaign(int id, CampaignDto campaignData)
    {
        var campaign = GetCampaign(id);
        
        _mapper.Map(campaignData, campaign);
        _database.SaveChanges();
    }

    public void DeleteCampaign(int id)
    {
        var campaign = GetCampaign(id);
        
        _database.Delete(campaign);
        _database.SaveChanges();
    }
}