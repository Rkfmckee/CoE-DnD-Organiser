using coe.dnd.api.ViewModels.Campaigns;
using coe.dnd.api.ViewModels.Characters;
using coe.dnd.api.ViewModels.GameMasters;

namespace coe.dnd.api.ViewModels.Games;

public class GameViewModel
{
    public int Id { get; set; }
    public string Details { get; set; }
    public DateTime Created { get; set; }
    public GameMasterViewModel GameMaster { get; set; }
    public CampaignViewModel Campaign { get; set; }
    public List<CharacterViewModel> Characters { get; set; }
}