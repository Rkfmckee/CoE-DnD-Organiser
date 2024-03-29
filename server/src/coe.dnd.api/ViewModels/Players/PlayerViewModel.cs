namespace coe.dnd.api.ViewModels.Players;

public class PlayerViewModel
{
    public int Id { get; set; }
    public string EmailAddress { get; set; }
    public string Name { get; set; }
    public DateOnly? DateOfBirth { get; set; }
}