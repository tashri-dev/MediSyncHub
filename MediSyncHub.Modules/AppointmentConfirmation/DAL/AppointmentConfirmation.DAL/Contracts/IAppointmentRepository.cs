using AppointmentConfirmation.DAL.models;

namespace AppointmentConfirmation.DAL.Contracts;

public interface IAppointmentRepository
{
    Task AddAsync(Appointment appointment, CancellationToken cancellationToken);
}