using coe.dnd.dal.Models;
using coe.dnd.services.DataTransferObjects;

namespace coe.dnd.services.Interfaces;

public interface ICampaignService
{
    bool CampaignExists(int id);
    CampaignDto GetCampaign(int id);
    IList<CampaignDto> GetCampaigns(string name = null, string theme = null, string writer = null);
    void CreateCampaign(CampaignDto campaignData);
    void UpdateCampaign(int id, CampaignDto campaignData);
    void DeleteCampaign(int id);
}