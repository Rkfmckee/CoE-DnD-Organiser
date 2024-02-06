using System.Diagnostics;
using coe.dnd.dal.Models;

namespace coe.dnd.api.ViewModels.Campaigns;

public class CampaignViewModel
{
    #region Properties
    
    public int Id { get; set; }
    public string Name { get; set; }
    public string Theme { get; set; }
    public string Details { get; set; }
    public string Writer { get; set; }
    
    #endregion

    #region Populate

    public void Populate(Campaign campaign)
    {
        Id = campaign.Id;
        Name = campaign.Name;
        Theme = campaign.Theme;
        Details = campaign.Details;
        Writer = campaign.Writer;
    }

    #endregion
}