using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace NJUArchery_SQL_Component.ArcheryModels;

public partial class ArcherymanagementContext : DbContext
{
    public ArcherymanagementContext()
    {
    }

    public ArcherymanagementContext(DbContextOptions<ArcherymanagementContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Arrow> Arrows { get; set; }

    public virtual DbSet<Club> Clubs { get; set; }

    public virtual DbSet<Competition> Competitions { get; set; }

    public virtual DbSet<Equipment> Equipment { get; set; }

    public virtual DbSet<Game> Games { get; set; }

    public virtual DbSet<Player> Players { get; set; }

    public virtual DbSet<Round> Rounds { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseMySQL("server=127.0.0.1;port=3306;uid=root;pwd=Peng_2001;database=archerymanagement");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Arrow>(entity =>
        {
            entity.HasKey(e => e.ArrowId).HasName("PRIMARY");

            entity.ToTable("arrow");

            entity.HasIndex(e => e.RoundId, "source_round");

            entity.Property(e => e.ArrowId).HasColumnName("arrow_id");
            entity.Property(e => e.ArrowNum).HasColumnName("arrow_num");
            entity.Property(e => e.RoundId).HasColumnName("round_id");
            entity.Property(e => e.Score)
                .HasMaxLength(1)
                .IsFixedLength()
                .HasColumnName("score");

            entity.HasOne(d => d.Round).WithMany(p => p.Arrows)
                .HasForeignKey(d => d.RoundId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("source_round");
        });

        modelBuilder.Entity<Club>(entity =>
        {
            entity.HasKey(e => e.ClubId).HasName("PRIMARY");

            entity.ToTable("club");

            entity.Property(e => e.ClubId).HasColumnName("club_id");
            entity.Property(e => e.Location)
                .HasMaxLength(255)
                .HasColumnName("location");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name");
        });

        modelBuilder.Entity<Competition>(entity =>
        {
            entity.HasKey(e => e.CompetitionId).HasName("PRIMARY");

            entity.ToTable("competition");

            entity.Property(e => e.CompetitionId).HasColumnName("competition_id");
            entity.Property(e => e.CompetitionLevel)
                .HasMaxLength(255)
                .HasColumnName("competition_level");
            entity.Property(e => e.DateEnd)
                .HasColumnType("datetime")
                .HasColumnName("date_end");
            entity.Property(e => e.DateStart)
                .HasColumnType("datetime")
                .HasColumnName("date_start");
            entity.Property(e => e.Hosting)
                .HasMaxLength(255)
                .HasColumnName("hosting");
            entity.Property(e => e.PlayerNum).HasColumnName("player_num");
            entity.Property(e => e.Title)
                .HasMaxLength(255)
                .HasColumnName("title");
        });

        modelBuilder.Entity<Equipment>(entity =>
        {
            entity.HasKey(e => e.EquipmentId).HasName("PRIMARY");

            entity.ToTable("equipment");

            entity.HasIndex(e => e.ClubId, "club_owner");

            entity.HasIndex(e => e.PlayerId, "owner");

            entity.Property(e => e.EquipmentId).HasColumnName("equipment_id");
            entity.Property(e => e.ClubId)
                .HasComment("Owner")
                .HasColumnName("club_id");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name");
            entity.Property(e => e.Note)
                .HasMaxLength(255)
                .HasColumnName("note");
            entity.Property(e => e.PlayerId)
                .HasComment("Owner")
                .HasColumnName("player_id");
            entity.Property(e => e.Pounds)
                .HasComment("Only for bow and bow piece.")
                .HasColumnName("pounds");
            entity.Property(e => e.Type)
                .HasComment("To describe the type of this equipment, for example: Bow, BowString, etc. Decided by encoder from datareader.")
                .HasColumnName("type");

            entity.HasOne(d => d.Club).WithMany(p => p.Equipment)
                .HasForeignKey(d => d.ClubId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("club_owner");

            entity.HasOne(d => d.Player).WithMany(p => p.Equipment)
                .HasForeignKey(d => d.PlayerId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("owner");
        });

        modelBuilder.Entity<Game>(entity =>
        {
            entity.HasKey(e => e.GameId).HasName("PRIMARY");

            entity.ToTable("game");

            entity.HasIndex(e => e.PlayerBId, "player_B");

            entity.HasIndex(e => e.PlayerAId, "qualification");

            entity.HasIndex(e => e.CompetitionId, "source_competition");

            entity.Property(e => e.GameId).HasColumnName("game_id");
            entity.Property(e => e.ASetScore)
                .HasComment("matchplay")
                .HasColumnName("A_set_score");
            entity.Property(e => e.BSetScore)
                .HasComment("matchplay")
                .HasColumnName("B_set_score");
            entity.Property(e => e.CompetitionId).HasColumnName("competition_id");
            entity.Property(e => e.Distance).HasColumnName("distance");
            entity.Property(e => e.GameType)
                .HasColumnType("enum('qualification','matchplay')")
                .HasColumnName("game_type");
            entity.Property(e => e.Half)
                .HasComment("qualification")
                .HasColumnType("enum('first','second','notvalidation')")
                .HasColumnName("half");
            entity.Property(e => e.PlayerAId)
                .HasComment("\r\n")
                .HasColumnName("player_A_id");
            entity.Property(e => e.PlayerBId).HasColumnName("player_B_id");
            entity.Property(e => e.ScoreSum)
                .HasComment("qualification")
                .HasColumnName("score_sum");
            entity.Property(e => e.TargetType).HasColumnName("target_type");

            entity.HasOne(d => d.Competition).WithMany(p => p.Games)
                .HasForeignKey(d => d.CompetitionId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("source_competition");

            entity.HasOne(d => d.PlayerA).WithMany(p => p.GamePlayerAs)
                .HasForeignKey(d => d.PlayerAId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("player_A");

            entity.HasOne(d => d.PlayerB).WithMany(p => p.GamePlayerBs)
                .HasForeignKey(d => d.PlayerBId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("player_B");
        });

        modelBuilder.Entity<Player>(entity =>
        {
            entity.HasKey(e => e.PlayerId).HasName("PRIMARY");

            entity.ToTable("player");

            entity.HasIndex(e => e.ClubId, "source_club");

            entity.Property(e => e.PlayerId).HasColumnName("player_id");
            entity.Property(e => e.BirthDate)
                .HasColumnType("datetime")
                .HasColumnName("birth_date");
            entity.Property(e => e.BowType)
                .HasColumnType("enum('recurve','barebow','tradition','compound')")
                .HasColumnName("bow_type");
            entity.Property(e => e.ClubId).HasColumnName("club_id");
            entity.Property(e => e.FirstName)
                .HasMaxLength(255)
                .HasColumnName("first_name");
            entity.Property(e => e.Gender)
                .HasColumnType("enum('male','female')")
                .HasColumnName("gender");
            entity.Property(e => e.LastName)
                .HasMaxLength(255)
                .HasColumnName("last_name");

            entity.HasOne(d => d.Club).WithMany(p => p.Players)
                .HasForeignKey(d => d.ClubId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("source_club");
        });

        modelBuilder.Entity<Round>(entity =>
        {
            entity.HasKey(e => e.RoundId).HasName("PRIMARY");

            entity.ToTable("round");

            entity.HasIndex(e => e.GameId, "source_game");

            entity.HasIndex(e => e.PlayerId, "source_player");

            entity.Property(e => e.RoundId).HasColumnName("round_id");
            entity.Property(e => e.GameId).HasColumnName("game_id");
            entity.Property(e => e.PlayerId).HasColumnName("player_id");
            entity.Property(e => e.RoundNum).HasColumnName("round_num");
            entity.Property(e => e.ScoreSum).HasColumnName("score_sum");

            entity.HasOne(d => d.Game).WithMany(p => p.Rounds)
                .HasForeignKey(d => d.GameId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("source_game");

            entity.HasOne(d => d.Player).WithMany(p => p.Rounds)
                .HasForeignKey(d => d.PlayerId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("source_player");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
