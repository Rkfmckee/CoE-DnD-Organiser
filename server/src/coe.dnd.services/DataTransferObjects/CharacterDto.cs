namespace coe.dnd.services.DataTransferObjects;

public class CharacterDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Race { get; set; }
    public string ClassLevels { get; set; }
    public int? PlayerId { get; set; }
    public PlayerDto Player { get; set; }
}