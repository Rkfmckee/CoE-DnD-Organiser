namespace coe.dnd.services.DataTransferObjects;

public class GameDto
{
    public int Id { get; set; }
    public string Details { get; set; }
    public GameMasterDto GameMaster { get; set; }
    public CampaignDto Campaign { get; set; }
    public IList<CharacterDto> Characters { get; set; }
}