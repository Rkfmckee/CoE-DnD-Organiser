namespace coe.dnd.services.DataTransferObjects;

public class PlayerDto
{
    public int Id { get; set; }
    public string EmailAddress { get; set; }
    public string Name { get; set; }
    public string Password { get; set; }
    public DateOnly? DateOfBirth { get; set; }
}