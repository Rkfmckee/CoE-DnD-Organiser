using coe.dnd.api.ViewModels.Players;

namespace coe.dnd.api.ViewModels.Games;

public class GameViewModel
{
    public int Id { get; set; }
    public string Details { get; set; }
    public DateTime Created { get; set; }
    public PlayerViewModel GameMaster { get; set; }
    public List<PlayerViewModel> Players { get; set; }
}