using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace coe.dnd.dal.Models;

[Table("campaigns")]
public partial class Campaign
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Required]
    [Column("name", TypeName = "character varying")]
    public string Name { get; set; }

    [Required]
    [Column("theme", TypeName = "character varying")]
    public string Theme { get; set; }

    [Column("details", TypeName = "character varying")]
    public string Details { get; set; }

    [Required]
    [Column("writer", TypeName = "character varying")]
    public string Writer { get; set; }

    [Column("created", TypeName = "timestamp without time zone")]
    public DateTime Created { get; set; }

    [InverseProperty("Campaign")]
    public virtual ICollection<Game> Games { get; set; } = new List<Game>();
}
