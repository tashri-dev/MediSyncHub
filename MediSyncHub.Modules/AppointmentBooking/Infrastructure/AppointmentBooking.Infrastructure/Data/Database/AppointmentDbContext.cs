using AppointmentBooking.Domain.Entities;
using AppointmentBooking.Domain.Repository;
using Microsoft.EntityFrameworkCore;

namespace AppointmentBooking.Infrastructure.Data.Database;

public class BookingDbContext(DbContextOptions<BookingDbContext> options) : DbContext(options), IUnitOfWork
{
    public DbSet<Appointment> Appointments { get; set; }
    public DbSet<Slot> Slots { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Appointment>(builder =>
        {
            builder.ToTable("Appointments", "booking");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.SlotId).IsRequired();
            builder.Property(x => x.PatientId).IsRequired();
            builder.Property(x => x.PatientName).IsRequired();
            builder.Property(x => x.ReservedAt).IsRequired();
            builder.Property(x => x.Status).HasConversion<int>()
                .HasDefaultValue(Status.Pending);
        });

        modelBuilder.Entity<Slot>(builder =>
        {
            builder.ToTable("Slots", "booking");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Time).IsRequired();
            builder.Property(x => x.Cost).HasColumnType("decimal(18,2)");
        });
    }

    public async Task<bool> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        await base.SaveChangesAsync(cancellationToken);
        return true;
    }
}