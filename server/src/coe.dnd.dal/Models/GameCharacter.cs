using System;
using System.Collections.Generic;

namespace coe.dnd.dal.Models;

public class GameCharacter : Entity
{
    public int? GameId { get; set; }

    public int? CharacterId { get; set; }

    public virtual Character Character { get; set; }

    public virtual Game Game { get; set; }
}
