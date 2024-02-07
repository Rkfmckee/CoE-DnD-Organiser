using coe.dnd.dal.Interfaces;
using coe.dnd.dal.Models;
using Microsoft.EntityFrameworkCore;

namespace coe.dnd.dal.Contexts;

public partial class DndOrganiserContext : BaseContext, IDndOrganiserDatabase
{
    public DndOrganiserContext(DbContextOptions option) : base(option) { }
    public DndOrganiserContext(string connectionString) : base(connectionString) { }

    public virtual DbSet<Campaign> Campaigns { get; set; }

    public virtual DbSet<Character> Characters { get; set; }
    
    public virtual DbSet<Game> Games { get; set; }

    public virtual DbSet<GameCharacter> GameCharacters { get; set; }

    public virtual DbSet<GameMaster> GameMasters { get; set; }

    public virtual DbSet<Player> Players { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Campaign>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("campaigns_pkey");
        });

        modelBuilder.Entity<Character>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("characters_pkey");

            entity.HasOne(d => d.Player).WithMany(p => p.Characters)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("fk_player");
        });

        modelBuilder.Entity<Game>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("games_pkey");

            entity.HasOne(d => d.Campaign).WithMany(p => p.Games)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("fk_campaign");

            entity.HasOne(d => d.GameMaster).WithMany(p => p.Games)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("fk_game_master");
        });

        modelBuilder.Entity<GameCharacter>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("game_characters_pkey");

            entity.HasOne(d => d.Character).WithMany(p => p.GameCharacters)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("fk_character");

            entity.HasOne(d => d.Game).WithMany(p => p.GameCharacters)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("fk_game");
        });

        modelBuilder.Entity<GameMaster>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("game_masters_pkey");

            entity.HasOne(d => d.Player).WithMany(p => p.GameMasters)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("fk_player");
        });

        modelBuilder.Entity<Player>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("players_pkey");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
