using AppointmentManagement.Core.Application.Ports.Repository;
using AppointmentManagement.Core.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace AppointmentManagement.Shell.Infrastructure.Adapters.Persistence;

public class ManagementDbContext(DbContextOptions<ManagementDbContext> options) : DbContext(options), IUnitOfWork
{
    public DbSet<Appointment> Appointments { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Appointment>(builder =>
        {
            builder.ToTable("Appointments", "Management");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.SlotId).IsRequired();
            builder.Property(x => x.PatientId).IsRequired();
            builder.Property(x => x.PatientName).IsRequired();
            builder.Property(x => x.ReservedAt).IsRequired();
            builder.Property(x => x.Status).HasConversion<int>()
                .HasDefaultValue(Status.Pending);
        });
    }

    public async Task<bool> SaveChangesAsync(CancellationToken cancellationToken = default) =>
        await base.SaveChangesAsync(cancellationToken) > 0;
}