using coe.dnd.dal.Models;
using coe.dnd.services.DataTransferObjects;

namespace coe.dnd.services.Interfaces;

public interface ICampaignService
{
    bool CampaignExists(int id);
    Campaign GetCampaign(int id);
    CampaignDto GetCampaignData(int id);
    IList<Campaign> GetCampaigns(string name = null, string theme = null, string writer = null);
    IList<CampaignDto> GetCampaignsData(string name = null, string theme = null, string writer = null);
    void CreateCampaign(CampaignDto campaignData);
    void UpdateCampaign(int id, CampaignDto campaignData);
    void DeleteCampaign(int id);
}