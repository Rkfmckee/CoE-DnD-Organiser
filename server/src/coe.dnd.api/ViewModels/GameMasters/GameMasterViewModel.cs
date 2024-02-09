using coe.dnd.api.ViewModels.Players;

namespace coe.dnd.api.ViewModels.GameMasters;

public class GameMasterViewModel
{
    public int Id { get; set; }
    public string PlanningNotes { get; set; }
    public PlayerViewModel Player { get; set; }
}