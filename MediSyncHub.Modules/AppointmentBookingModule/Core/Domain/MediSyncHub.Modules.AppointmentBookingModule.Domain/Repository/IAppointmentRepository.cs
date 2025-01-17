using MediSyncHub.Modules.AppointmentBookingModule.Domain.Entities;

namespace MediSyncHub.Modules.AppointmentBookingModule.Domain.Repository;

public interface IAppointmentRepository
{
    Task AddAsync(Appointment appointment, CancellationToken cancellationToken);
}