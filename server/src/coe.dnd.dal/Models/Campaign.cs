using System;
using System.Collections.Generic;

namespace coe.dnd.dal.Models;

public class Campaign : Entity
{
    public string Name { get; set; }

    public string Theme { get; set; }

    public string Details { get; set; }

    public string Writer { get; set; }

    public DateTime Created { get; set; }

    public virtual ICollection<Game> Games { get; set; } = new List<Game>();
}
