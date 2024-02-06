using System;
using System.Collections.Generic;

namespace coe.dnd.dal.Models;

public partial class Player
{
    public int Id { get; set; }

    public string EmailAddress { get; set; }

    public string Password { get; set; }

    public string Name { get; set; }

    public DateOnly? DateOfBirth { get; set; }

    public DateTime Created { get; set; }

    public virtual ICollection<Character> Characters { get; set; } = new List<Character>();

    public virtual ICollection<GameMaster> GameMasters { get; set; } = new List<GameMaster>();
}
