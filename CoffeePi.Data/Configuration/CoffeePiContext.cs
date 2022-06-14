using CoffeePi.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace CoffeePi.Data.Configuration;

public class CoffeePiContext : DbContext
{
    public DbSet<CoffeeRoutine> CoffeeRoutines { get; set; }
    public DbSet<ExecutedRoutine> ExecutedRoutines { get; set; }

    public CoffeePiContext(DbContextOptions<CoffeePiContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Sets the deletion behavior to restricted (data can't be deleted if a forgein key still points to it)
        foreach (IMutableForeignKey relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
        {
            relationship.DeleteBehavior = DeleteBehavior.Restrict;
        }

        modelBuilder
            .Entity<CoffeeRoutine>()
            .HasDiscriminator<string>("Discriminator")
            .HasValue<SingleRoutine>(nameof(SingleRoutine))
            .HasValue<DailyRoutine>(nameof(DailyRoutine))
            .HasValue<WeeklyRoutine>(nameof(WeeklyRoutine));

        modelBuilder
            .Entity<DailyRoutine>()
            .Property(e => e.DaysOfWeek)
            .HasConversion(
                e => string.Join(',', e.Select(x => x.ToString("D")).ToArray()),
                e => e.Split(new[] { ',' })
                .Select(x => Enum.Parse<DayOfWeek>(x))
                .Cast<DayOfWeek>()
                .ToList()
            );
    }
}
