namespace coe.dnd.services.DataTransferObjects;

public class GameMasterDto
{
    public int Id { get; set; }
    public string PlanningNotes { get; set; }
    public int PlayerId { get; set; }
    public PlayerDto Player { get; set; }
}