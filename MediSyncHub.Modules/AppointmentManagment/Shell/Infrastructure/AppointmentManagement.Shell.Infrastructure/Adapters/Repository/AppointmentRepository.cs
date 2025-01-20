using AppointmentManagement.Core.Application.Ports.Repository;
using AppointmentManagement.Core.Domain.Entities;
using AppointmentManagement.Shell.Infrastructure.Adapters.Persistence;
using Microsoft.EntityFrameworkCore;

namespace AppointmentManagement.Shell.Infrastructure.Adapters.Repository;

public class AppointmentRepository(ManagementDbContext dbContext) : IAppointmentRepository
{
    public async Task<IEnumerable<Appointment>> GetUpcomingAppointmentsAsync(
        CancellationToken cancellationToken = default)
    {
        var appointments = await dbContext.Appointments
            .Where(a => a.AppointmentTime > DateTime.UtcNow
                        && a.Status == Status.Pending).ToListAsync(cancellationToken);
        return appointments;
    }

    public async Task<Appointment> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var appointment = await dbContext.Appointments.FindAsync(id, cancellationToken);
        return appointment ?? throw new Exception("Appointment not found");
    }

    public async Task AddAsync(Appointment appointment, CancellationToken cancellationToken = default)
    {
        await dbContext.Appointments.AddAsync(appointment, cancellationToken);
    }

    public void Update(Appointment appointment)
    {
        dbContext.Appointments.Update(appointment);
    }
}