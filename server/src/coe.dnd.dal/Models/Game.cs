using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace coe.dnd.dal.Models;

[Table("games")]
public partial class Game
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("details", TypeName = "character varying")]
    public string Details { get; set; }

    [Column("created", TypeName = "timestamp without time zone")]
    public DateTime? Created { get; set; }

    [Column("game_master_id")]
    public int GameMasterId { get; set; }

    [Column("campaign_id")]
    public int? CampaignId { get; set; }

    [ForeignKey("CampaignId")]
    [InverseProperty("Games")]
    public virtual Campaign Campaign { get; set; }

    [InverseProperty("Game")]
    public virtual ICollection<GameCharacter> GameCharacters { get; set; } = new List<GameCharacter>();

    [ForeignKey("GameMasterId")]
    [InverseProperty("Games")]
    public virtual GameMaster GameMaster { get; set; }
}
