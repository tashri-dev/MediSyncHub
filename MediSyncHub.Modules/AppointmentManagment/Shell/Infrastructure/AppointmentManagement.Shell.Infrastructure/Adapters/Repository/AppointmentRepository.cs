using AppointmentManagement.Core.Application.Ports.Repository;
using AppointmentManagement.Core.Domain.Entities;
using AppointmentManagement.Shell.Infrastructure.Adapters.Persistence;
using Microsoft.EntityFrameworkCore;

namespace AppointmentManagement.Shell.Infrastructure.Adapters.Repository;

public class AppointmentRepository : IAppointmentRepository
{
    ManagementDbContext _dbContext;

    public AppointmentRepository(ManagementDbContext dbContext)
    {
        _dbContext = dbContext;
    }


    public async Task<IEnumerable<Appointment>> GetUpcomingAppointmentsAsync(
        CancellationToken cancellationToken = default)
    {
        var appointments = _dbContext.Appointments
            .Where(a => a.AppointmentTime > DateTime.Now
                        && a.Status == Status.Pending);
        return await appointments.ToListAsync(cancellationToken);
    }

    public async Task<Appointment> GetByIdAsync(Guid appointmentId, CancellationToken cancellationToken = default)
    {
        var appointment = await _dbContext.Appointments.FindAsync(appointmentId, cancellationToken);
        return appointment ?? throw new Exception("Appointment not found");
    }

    public async Task AddAsync(Appointment appointment, CancellationToken cancellationToken = default)
    {
        await _dbContext.Appointments.AddAsync(appointment, cancellationToken);
    }

    public void Update(Appointment appointment, CancellationToken cancellationToken = default)
    {
        _dbContext.Appointments.Update(appointment);
    }
}