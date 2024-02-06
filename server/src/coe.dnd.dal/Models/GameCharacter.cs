using System;
using System.Collections.Generic;

namespace coe.dnd.dal.Models;

public partial class GameCharacter
{
    public int Id { get; set; }

    public int? GameId { get; set; }

    public int? CharacterId { get; set; }

    public virtual Character Character { get; set; }

    public virtual Game Game { get; set; }
}
