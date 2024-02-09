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
    
    public CampaignDto GetCampaign(int id)
    {
        return _mapper
            .ProjectTo<CampaignDto>(GetCampaignQueryable(id))
            .SingleOrDefault();
    }
    
    public IList<CampaignDto> GetCampaigns(string name = null, string theme = null, string writer = null)
    {
        return _mapper
            .ProjectTo<CampaignDto>(GetCampaignsQueryable(name, theme, writer))
            .ToList();
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
        var campaign = GetCampaignObject(id);
        
        _mapper.Map(campaignData, campaign);
        _database.SaveChanges();
    }

    public void DeleteCampaign(int id)
    {
        var campaign = GetCampaignObject(id);
        
        _database.Delete(campaign);
        _database.SaveChanges();
    }
    
    private IQueryable<Campaign> GetCampaignQueryable(int id)
    {
        return _database.Get<Campaign>()
            .Where(new CampaignByIdSpec(id));
    }
    
    private Campaign GetCampaignObject(int id)
    {
        return GetCampaignQueryable(id).SingleOrDefault();
    }
    
    private IQueryable<Campaign> GetCampaignsQueryable(string name = null, string theme = null, string writer = null)
    {
        return _database.Get<Campaign>()
            .Where(new CampaignSearchSpec(name, theme, writer));
    }
}