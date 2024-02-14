using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace coe.dnd.dal.Models;

[Table("characters")]
public partial class Character
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Required]
    [Column("name", TypeName = "character varying")]
    public string Name { get; set; }

    [Required]
    [Column("race", TypeName = "character varying")]
    public string Race { get; set; }

    [Required]
    [Column("class_levels", TypeName = "character varying")]
    public string ClassLevels { get; set; }

    [Column("created", TypeName = "timestamp without time zone")]
    public DateTime Created { get; set; }

    [Column("player_id")]
    public int PlayerId { get; set; }
    
    [ForeignKey(nameof(PlayerId))]
    [InverseProperty("Characters")]
    public virtual Player Player { get; set; }

    [InverseProperty("Character")]
    public virtual ICollection<GameCharacter> GameCharacters { get; set; } = new List<GameCharacter>();
}
