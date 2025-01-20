using DoctorAvailability.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace DoctorAvailability.Business.Data;

public class DoctorAvailabilityDbContext(DbContextOptions<DoctorAvailabilityDbContext> options)
    : DbContext(options)
{
    public DbSet<Slot> Slots { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Slot>(builder =>
        {
            builder.ToTable("Slots", "DoctorAvailability");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Time).IsRequired();
            builder.Property(x => x.Cost).HasColumnType("decimal(18,2)");
        });
    }
}