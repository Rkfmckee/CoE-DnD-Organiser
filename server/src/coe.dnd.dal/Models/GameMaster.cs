using System;
using System.Collections.Generic;

namespace coe.dnd.dal.Models;

public partial class GameMaster
{
    public int Id { get; set; }

    public string PlanningNotes { get; set; }

    public DateTime? Created { get; set; }

    public int? PlayerId { get; set; }

    public virtual ICollection<Game> Games { get; set; } = new List<Game>();

    public virtual Player Player { get; set; }
}
