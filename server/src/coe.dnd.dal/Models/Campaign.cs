using System;
using System.Collections.Generic;

namespace coe.dnd.dal.Models;

public partial class Campaign
{
    public int Id { get; set; }

    public string Name { get; set; }

    public string Theme { get; set; }

    public string Details { get; set; }

    public string Writer { get; set; }

    public DateTime Created { get; set; }

    public virtual ICollection<Game> Games { get; set; } = new List<Game>();
}
