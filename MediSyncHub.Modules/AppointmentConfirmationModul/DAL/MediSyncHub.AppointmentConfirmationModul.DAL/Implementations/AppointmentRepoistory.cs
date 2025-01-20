using MediSyncHub.AppointmentConfirmationModule.DAL.Database;
using MediSyncHub.AppointmentConfirmationModule.DAL.models;

namespace MediSyncHub.AppointmentConfirmationModule.DAL.Repository;

internal class AppointmentRepoistory(ConfirmationDbContext context) : IAppointmentRepository
{
    public async Task AddAsync(Appointment appointment, CancellationToken cancellationToken)
    {
        await context.Appointments.AddAsync(appointment, cancellationToken);
    }
}