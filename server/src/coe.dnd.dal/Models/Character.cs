using System;
using System.Collections.Generic;

namespace coe.dnd.dal.Models;

public partial class Character
{
    public int Id { get; set; }

    public string Name { get; set; }

    public string Race { get; set; }

    public string ClassLevels { get; set; }

    public DateTime Created { get; set; }

    public int? PlayerId { get; set; }

    public virtual ICollection<GameCharacter> GameCharacters { get; set; } = new List<GameCharacter>();

    public virtual Player Player { get; set; }
}
