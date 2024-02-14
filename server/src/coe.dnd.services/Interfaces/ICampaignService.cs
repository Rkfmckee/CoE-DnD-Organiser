using coe.dnd.services.DataTransferObjects;

namespace coe.dnd.services.Interfaces;

public interface ICampaignService
{
    Task<bool> CampaignExistsAsync(int id);
    Task<CampaignDto> GetCampaignAsync(int id);
    Task<IList<CampaignDto>> GetCampaignsAsync(string name = null, string theme = null, string writer = null);
    Task CreateCampaignAsync(CampaignDto campaignData);
    Task UpdateCampaignAsync(int id, CampaignDto campaignData);
    Task DeleteCampaignAsync(int id);
}