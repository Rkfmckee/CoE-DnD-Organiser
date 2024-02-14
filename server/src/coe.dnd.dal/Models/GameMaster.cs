using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace coe.dnd.dal.Models;

[Table("game_masters")]
public partial class GameMaster
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("planning_notes", TypeName = "character varying")]
    public string PlanningNotes { get; set; }

    [Column("created", TypeName = "timestamp without time zone")]
    public DateTime? Created { get; set; }

    [Column("player_id")]
    public int PlayerId { get; set; }
    
    [ForeignKey(nameof(PlayerId))]
    [InverseProperty("GameMasters")]
    public virtual Player Player { get; set; }

    [InverseProperty("GameMaster")]
    public virtual ICollection<Game> Games { get; set; } = new List<Game>();
}
