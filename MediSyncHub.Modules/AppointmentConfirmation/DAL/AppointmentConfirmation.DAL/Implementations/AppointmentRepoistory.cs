using AppointmentConfirmation.DAL.Contracts;
using AppointmentConfirmation.DAL.Database;
using AppointmentConfirmation.DAL.models;

namespace AppointmentConfirmation.DAL.Implementations;

internal class AppointmentRepoistory(ConfirmationDbContext context) : IAppointmentRepository
{
    public async Task AddAsync(Appointment appointment, CancellationToken cancellationToken)
    {
        await context.Appointments.AddAsync(appointment, cancellationToken);
    }
}