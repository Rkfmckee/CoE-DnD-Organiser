namespace coe.dnd.api.ViewModels.Players;

public class CreatePlayerViewModel
{
    public string EmailAddress { get; set; }
    public string Name { get; set; }
    public DateOnly? DateOfBirth { get; set; }
    public string Password { get; set; }
}