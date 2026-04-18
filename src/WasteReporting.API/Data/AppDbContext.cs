using Microsoft.EntityFrameworkCore;
using WasteReporting.API.Models;

namespace WasteReporting.API.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<User> Users { get; set; }
    public DbSet<Report> Reports { get; set; }
    public DbSet<CollectionPoint> CollectionPoints { get; set; }
    public DbSet<Recycler> Recyclers { get; set; }
    public DbSet<FinalDestination> FinalDestinations { get; set; }
    public DbSet<Waste> Wastes { get; set; }
    public DbSet<Collection> Collections { get; set; }
    public DbSet<CollectionWaste> CollectionWastes { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<User>()
            .HasIndex(u => u.Email)
            .IsUnique();

        modelBuilder.Entity<CollectionWaste>()
            .HasKey(cr => new { cr.CollectionId, cr.WasteId });

        modelBuilder.Entity<CollectionWaste>()
            .HasOne(cr => cr.Collection)
            .WithMany(c => c.CollectionWastes)
            .HasForeignKey(cr => cr.CollectionId);

        modelBuilder.Entity<CollectionWaste>()
            .HasOne(cr => cr.Waste)
            .WithMany()
            .HasForeignKey(cr => cr.WasteId);
    }
}
