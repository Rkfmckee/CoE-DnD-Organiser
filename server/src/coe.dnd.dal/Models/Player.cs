using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace coe.dnd.dal.Models;

[Table("players")]
public partial class Player
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Required]
    [Column("email_address", TypeName = "character varying")]
    public string EmailAddress { get; set; }

    [Required]
    [Column("password", TypeName = "character varying")]
    public string Password { get; set; }

    [Column("name", TypeName = "character varying")]
    public string Name { get; set; }

    [Column("date_of_birth")]
    public DateOnly? DateOfBirth { get; set; }

    [Column("created", TypeName = "timestamp without time zone")]
    public DateTime Created { get; set; }

    [InverseProperty("Player")]
    public virtual ICollection<Character> Characters { get; set; } = new List<Character>();

    [InverseProperty("Player")]
    public virtual ICollection<GameMaster> GameMasters { get; set; } = new List<GameMaster>();
}
