using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Models;
using Models_DBModels;

#nullable disable

namespace DataLayerDBContext_DBContext
{
  public partial class NotFightClubUserContext : DbContext
  {
    public NotFightClubUserContext()
    {
    }

    public NotFightClubUserContext(DbContextOptions<NotFightClubUserContext> options)
        : base(options)
    {
    }

    public virtual DbSet<UserInfo> UserInfos { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
      if (!optionsBuilder.IsConfigured)
      {
        optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=NotFightClubUser;Trusted_Connection=True;");
        // optionsBuilder.UseSqlServer("Server=08162021dotnetuta.database.windows.net;Database=ShopDb;User Id=sqladmin;Password=Password12345;");

      }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

      modelBuilder.Entity<UserInfo>(entity =>
      {
        entity.HasKey(e => e.UserId)
                  .HasName("PK__UserInfo__1788CC4CF051EE23");

        entity.ToTable("UserInfo");

        entity.Property(e => e.UserId).HasDefaultValueSql("(newid())");

        entity.Property(e => e.Bucks).HasDefaultValueSql("((20))");

        entity.Property(e => e.Dob)
                  .HasColumnType("date")
                  .HasColumnName("DOB");

        entity.Property(e => e.Email).HasMaxLength(50);

        entity.Property(e => e.LastLogin).HasColumnType("date");

        entity.Property(e => e.Pword)
                  .IsRequired()
                  .HasMaxLength(100)
                  .HasColumnName("PWord");

        entity.Property(e => e.UserName)
                  .IsRequired()
                  .HasMaxLength(50);
      });

      OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
  }
}

