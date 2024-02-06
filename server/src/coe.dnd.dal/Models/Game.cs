using System;
using System.Collections.Generic;

namespace coe.dnd.dal.Models;

public class Game : Entity
{
    public string Details { get; set; }

    public DateTime? Created { get; set; }

    public int? GameMasterId { get; set; }

    public int? CampaignId { get; set; }

    public virtual Campaign Campaign { get; set; }

    public virtual ICollection<GameCharacter> GameCharacters { get; set; } = new List<GameCharacter>();

    public virtual GameMaster GameMaster { get; set; }
}
