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

    public async Task<Appointment> GetByIdAsync(Guid appointmentId, CancellationToken cancellationToken)
    {
        var appointment = await context.Appointments.FindAsync(appointmentId);
        return appointment ?? throw new Exception("Appointment not found");
    }

    public void Update(Appointment appointment)
    {
        context.Appointments.Update(appointment);
    }
}