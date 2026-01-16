using Microsoft.EntityFrameworkCore;
using Api.Models.Entities;

namespace Api.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Trustee> Trustees { get; set; }
    public DbSet<Initiative> Initiatives { get; set; }
    public DbSet<ContactSubmission> ContactSubmissions { get; set; }
    public DbSet<SiteContent> SiteContents { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Trustee configuration
        modelBuilder.Entity<Trustee>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.FullName)
                .IsRequired()
                .HasMaxLength(150);
            entity.Property(e => e.RoleTitle)
                .IsRequired()
                .HasMaxLength(80);
            entity.Property(e => e.Bio)
                .HasMaxLength(1000);
            entity.Property(e => e.PhotoUrl)
                .HasMaxLength(500);
            entity.Property(e => e.DisplayOrder)
                .HasDefaultValue(0);
            entity.Property(e => e.IsActive)
                .HasDefaultValue(true);
            entity.Property(e => e.CreatedAt)
                .IsRequired();
        });

        // Initiative configuration
        modelBuilder.Entity<Initiative>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Title)
                .IsRequired()
                .HasMaxLength(150);
            entity.Property(e => e.ShortDescription)
                .IsRequired()
                .HasMaxLength(1000);
            entity.Property(e => e.Status)
                .IsRequired()
                .HasMaxLength(30);
            entity.Property(e => e.DisplayOrder)
                .HasDefaultValue(0);
            entity.Property(e => e.IsActive)
                .HasDefaultValue(true);
            entity.Property(e => e.CreatedAt)
                .IsRequired();
        });

        // ContactSubmission configuration
        modelBuilder.Entity<ContactSubmission>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(120);
            entity.Property(e => e.Email)
                .HasMaxLength(150);
            entity.Property(e => e.Phone)
                .HasMaxLength(30);
            entity.Property(e => e.Message)
                .IsRequired()
                .HasMaxLength(2000);
            entity.Property(e => e.SourcePage)
                .HasMaxLength(50);
            entity.Property(e => e.IsRead)
                .HasDefaultValue(false);
            entity.Property(e => e.CreatedAt)
                .IsRequired();
        });

        // SiteContent configuration
        modelBuilder.Entity<SiteContent>(entity =>
        {
            entity.HasKey(e => e.Key);
            entity.Property(e => e.Key)
                .HasMaxLength(120);
            entity.Property(e => e.Value)
                .IsRequired();
            entity.Property(e => e.UpdatedAt)
                .IsRequired();
        });
    }
}

