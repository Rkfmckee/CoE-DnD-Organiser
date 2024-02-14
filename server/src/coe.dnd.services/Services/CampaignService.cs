using AutoMapper;
using coe.dnd.dal.Interfaces;
using coe.dnd.dal.Models;
using coe.dnd.dal.Specifications.Campaigns;
using coe.dnd.services.DataTransferObjects;
using coe.dnd.services.Interfaces;
using Microsoft.EntityFrameworkCore;
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

    public async Task<bool> CampaignExistsAsync(int id)
    {
        return await _database.Get<Campaign>()
            .Where(new CampaignByIdSpec(id))
            .AnyAsync();
    }
    
    public async Task<CampaignDto> GetCampaignAsync(int id)
    {
        return await _mapper
            .ProjectTo<CampaignDto>(GetCampaignQuery(id))
            .SingleOrDefaultAsync();
    }
    
    public async Task<IList<CampaignDto>> GetCampaignsAsync(string name = null, string theme = null, string writer = null)
    {
        return await _mapper
            .ProjectTo<CampaignDto>(GetCampaignsQuery(name, theme, writer))
            .ToListAsync();
    }

    public async Task CreateCampaignAsync(CampaignDto campaignData)
    {
        var campaign = _mapper.Map<Campaign>(campaignData);
        campaign.Created = DateTime.UtcNow;
        
        _database.Add(campaign);
        await _database.SaveChangesAsync();
    }

    public async Task UpdateCampaignAsync(int id, CampaignDto campaignData)
    {
        var campaign = await GetCampaignFromQueryAsync(id);
        
        _mapper.Map(campaignData, campaign);
        await _database.SaveChangesAsync();
    }

    public async Task DeleteCampaignAsync(int id)
    {
        var campaign = await GetCampaignFromQueryAsync(id);
        
        _database.Delete(campaign);
        await _database.SaveChangesAsync();
    }
    
    private IQueryable<Campaign> GetCampaignQuery(int id)
    {
        return _database.Get<Campaign>()
            .Where(new CampaignByIdSpec(id));
    }
    
    private async Task<Campaign> GetCampaignFromQueryAsync(int id)
    {
        return await GetCampaignQuery(id).SingleOrDefaultAsync();
    }
    
    private IQueryable<Campaign> GetCampaignsQuery(string name = null, string theme = null, string writer = null)
    {
        return _database.Get<Campaign>()
            .Where(new CampaignSearchSpec(name, theme, writer));
    }
}