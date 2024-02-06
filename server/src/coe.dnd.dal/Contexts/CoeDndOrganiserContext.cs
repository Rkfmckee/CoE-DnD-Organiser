using coe.dnd.dal.Models;
using Microsoft.EntityFrameworkCore;

namespace coe.dnd.dal.Contexts;

public partial class CoeDndOrganiserContext : DbContext
{
    public CoeDndOrganiserContext()
    {
    }

    public CoeDndOrganiserContext(DbContextOptions<CoeDndOrganiserContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Campaign> Campaigns { get; set; }

    public virtual DbSet<Character> Characters { get; set; }

    public virtual DbSet<FlywaySchemaHistory> FlywaySchemaHistories { get; set; }

    public virtual DbSet<Game> Games { get; set; }

    public virtual DbSet<GameCharacter> GameCharacters { get; set; }

    public virtual DbSet<GameMaster> GameMasters { get; set; }

    public virtual DbSet<Player> Players { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Server=localhost,5432;Database=coe-dnd-organiser;User Id=user;Password=password;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Campaign>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("campaigns_pkey");

            entity.ToTable("campaigns");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Created)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created");
            entity.Property(e => e.Details)
                .HasColumnType("character varying")
                .HasColumnName("details");
            entity.Property(e => e.Name)
                .IsRequired()
                .HasColumnType("character varying")
                .HasColumnName("name");
            entity.Property(e => e.Theme)
                .IsRequired()
                .HasColumnType("character varying")
                .HasColumnName("theme");
            entity.Property(e => e.Writer)
                .IsRequired()
                .HasColumnType("character varying")
                .HasColumnName("writer");
        });

        modelBuilder.Entity<Character>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("characters_pkey");

            entity.ToTable("characters");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ClassLevels)
                .IsRequired()
                .HasColumnType("character varying")
                .HasColumnName("class_levels");
            entity.Property(e => e.Created)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created");
            entity.Property(e => e.Name)
                .IsRequired()
                .HasColumnType("character varying")
                .HasColumnName("name");
            entity.Property(e => e.PlayerId).HasColumnName("player_id");
            entity.Property(e => e.Race)
                .IsRequired()
                .HasColumnType("character varying")
                .HasColumnName("race");

            entity.HasOne(d => d.Player).WithMany(p => p.Characters)
                .HasForeignKey(d => d.PlayerId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("fk_player");
        });

        modelBuilder.Entity<FlywaySchemaHistory>(entity =>
        {
            entity.HasKey(e => e.InstalledRank).HasName("flyway_schema_history_pk");

            entity.ToTable("flyway_schema_history");

            entity.HasIndex(e => e.Success, "flyway_schema_history_s_idx");

            entity.Property(e => e.InstalledRank)
                .ValueGeneratedNever()
                .HasColumnName("installed_rank");
            entity.Property(e => e.Checksum).HasColumnName("checksum");
            entity.Property(e => e.Description)
                .IsRequired()
                .HasMaxLength(200)
                .HasColumnName("description");
            entity.Property(e => e.ExecutionTime).HasColumnName("execution_time");
            entity.Property(e => e.InstalledBy)
                .IsRequired()
                .HasMaxLength(100)
                .HasColumnName("installed_by");
            entity.Property(e => e.InstalledOn)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("installed_on");
            entity.Property(e => e.Script)
                .IsRequired()
                .HasMaxLength(1000)
                .HasColumnName("script");
            entity.Property(e => e.Success).HasColumnName("success");
            entity.Property(e => e.Type)
                .IsRequired()
                .HasMaxLength(20)
                .HasColumnName("type");
            entity.Property(e => e.Version)
                .HasMaxLength(50)
                .HasColumnName("version");
        });

        modelBuilder.Entity<Game>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("games_pkey");

            entity.ToTable("games");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CampaignId).HasColumnName("campaign_id");
            entity.Property(e => e.Created)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created");
            entity.Property(e => e.Details)
                .HasColumnType("character varying")
                .HasColumnName("details");
            entity.Property(e => e.GameMasterId).HasColumnName("game_master_id");

            entity.HasOne(d => d.Campaign).WithMany(p => p.Games)
                .HasForeignKey(d => d.CampaignId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("fk_campaign");

            entity.HasOne(d => d.GameMaster).WithMany(p => p.Games)
                .HasForeignKey(d => d.GameMasterId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("fk_game_master");
        });

        modelBuilder.Entity<GameCharacter>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("game_characters_pkey");

            entity.ToTable("game_characters");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CharacterId).HasColumnName("character_id");
            entity.Property(e => e.GameId).HasColumnName("game_id");

            entity.HasOne(d => d.Character).WithMany(p => p.GameCharacters)
                .HasForeignKey(d => d.CharacterId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("fk_character");

            entity.HasOne(d => d.Game).WithMany(p => p.GameCharacters)
                .HasForeignKey(d => d.GameId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("fk_game");
        });

        modelBuilder.Entity<GameMaster>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("game_masters_pkey");

            entity.ToTable("game_masters");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Created)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created");
            entity.Property(e => e.PlanningNotes)
                .HasColumnType("character varying")
                .HasColumnName("planning_notes");
            entity.Property(e => e.PlayerId).HasColumnName("player_id");

            entity.HasOne(d => d.Player).WithMany(p => p.GameMasters)
                .HasForeignKey(d => d.PlayerId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("fk_player");
        });

        modelBuilder.Entity<Player>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("players_pkey");

            entity.ToTable("players");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Created)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created");
            entity.Property(e => e.DateOfBirth).HasColumnName("date_of_birth");
            entity.Property(e => e.EmailAddress)
                .IsRequired()
                .HasColumnType("character varying")
                .HasColumnName("email_address");
            entity.Property(e => e.Name)
                .HasColumnType("character varying")
                .HasColumnName("name");
            entity.Property(e => e.Password)
                .IsRequired()
                .HasColumnType("character varying")
                .HasColumnName("password");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
