using coe.dnd.dal.Interfaces;
using coe.dnd.dal.Models;
using Microsoft.EntityFrameworkCore;

namespace coe.dnd.dal.Contexts;

public class DndOrganiserContext : BaseContext, IDndOrganiserDatabase
{
    public DndOrganiserContext(DbContextOptions option) : base(option) { }
    public DndOrganiserContext(string connectionString) : base(connectionString) { }

    public virtual DbSet<Campaign> Campaigns { get; set; }
    public virtual DbSet<Character> Characters { get; set; }
    public virtual DbSet<Game> Games { get; set; }
    public virtual DbSet<GameCharacter> GameCharacters { get; set; }
    public virtual DbSet<GameMaster> GameMasters { get; set; }
    public virtual DbSet<Player> Players { get; set; }
}
