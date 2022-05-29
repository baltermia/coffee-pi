using CoffeePi.Database.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace CoffeePi.Database.Configuration;

public class CoffeePiContext : DbContext
{
    public DbSet<PlannedRoutine> PlannedRoutines { get; set; }

    public CoffeePiContext(DbContextOptions<CoffeePiContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Sets the deletion behavior to restricted (data can't be deleted if a forgein key still points to it)
        foreach (IMutableForeignKey relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
        {
            relationship.DeleteBehavior = DeleteBehavior.Restrict;
        }
    }
}
