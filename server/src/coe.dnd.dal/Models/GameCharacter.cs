using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace coe.dnd.dal.Models;

[Table("game_characters")]
public partial class GameCharacter
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Column("game_id")]
    public int GameId { get; set; }
    
    [ForeignKey(nameof(GameId))]
    [InverseProperty("GameCharacters")]
    public virtual Game Game { get; set; }

    [Column("character_id")]
    public int CharacterId { get; set; }

    [ForeignKey(nameof(CharacterId))]
    [InverseProperty("GameCharacters")]
    public virtual Character Character { get; set; }
}
