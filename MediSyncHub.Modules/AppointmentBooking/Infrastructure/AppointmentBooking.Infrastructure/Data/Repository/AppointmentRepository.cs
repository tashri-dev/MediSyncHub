using AppointmentBooking.Domain.Entities;
using AppointmentBooking.Domain.Repository;
using AppointmentBooking.Infrastructure.Data.Database;


namespace AppointmentBooking.Infrastructure.Data.Repository;

public class AppointmentRepository(BookingDbContext context) : IAppointmentRepository
{
    public async Task AddAsync(Appointment appointment, CancellationToken cancellationToken)
    {
        await context.Appointments.AddAsync(appointment);
    }
}