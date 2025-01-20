using MediSyncHub.Modules.AppointmentBookingModule.Domain.Entities;
using MediSyncHub.Modules.AppointmentBookingModule.Domain.Repository;
using MediSyncHub.Modules.AppointmentBookingModule.Infrastructure.Data.Database;

namespace MediSyncHub.Modules.AppointmentBookingModule.Infrastructure.Data.Repository;

public class AppointmentRepository(BookingDbContext context) : IAppointmentRepository
{
    public async Task AddAsync(Appointment appointment, CancellationToken cancellationToken)
    {
        await context.Appointments.AddAsync(appointment);
    }
}