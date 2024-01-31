using coe.dnd.api.ViewModels.Players;

namespace coe.dnd.api.ViewModels.Characters;

public class CharacterViewModel
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Race { get; set; }
    public string ClassLevels { get; set; }
    public DateTime Created { get; set; }
    public PlayerViewModel Player { get; set; }
}