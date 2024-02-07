using coe.dnd.dal.Models;

namespace coe.dnd.services.Interfaces;

public interface ICampaignService
{
    Campaign GetCampaign(int id);

    IList<Campaign> GetCampaigns(string name = null, string theme = null, string writer = null);

    void CreateCampaign(Campaign campaign);
    
    void UpdateCampaign(int id, Campaign campaign);

    void DeleteCampaign(int id);
}