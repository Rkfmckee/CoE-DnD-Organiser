namespace coe.dnd.services.DataTransferObjects;

public class GameDto
{
    public int Id { get; set; }
    public string Details { get; set; }
    public int? GameMasterId { get; set; }
    public GameMasterDto GameMaster { get; set; }
    public int? CampaignId { get; set; }
    public CampaignDto Campaign { get; set; }
    public IList<CharacterDto> Characters { get; set; }
}