using AppointmentBooking.Domain.Entities;

namespace AppointmentBooking.Domain.Repository;

public interface IAppointmentRepository
{
    Task AddAsync(Appointment appointment, CancellationToken cancellationToken);
    Task<Appointment> GetByIdAsync(Guid appointmentId, CancellationToken cancellationToken);
    void Update(Appointment appointment);
}