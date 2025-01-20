using AppointmentConfirmation.DAL.Contracts;
using AppointmentConfirmation.DAL.models;
using Microsoft.EntityFrameworkCore;

namespace AppointmentConfirmation.DAL.Database;

public class ConfirmationDbContext(DbContextOptions<ConfirmationDbContext> options) : DbContext(options), IUnitOfWork
{
    public DbSet<Appointment> Appointments { get; set; }
    public DbSet<Notification> Notifications { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Appointment>(builder =>
        {
            builder.ToTable("Appointments", "Confirmation");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.SlotId).IsRequired();
            builder.Property(x => x.PatientId).IsRequired();
            builder.Property(x => x.PatientName).IsRequired();
            builder.Property(x => x.ReservedAt).IsRequired();
        });
        modelBuilder.Entity<Notification>(builder =>
        {
            builder.ToTable("Notifications", "Confirmation");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.AppointmentId).IsRequired();
        });
    }

    public async Task<bool> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return await base.SaveChangesAsync(cancellationToken) > 0;
    }
}