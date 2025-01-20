using AppointmentManagement.Core.Domain.Entities;

namespace AppointmentManagement.Core.Application.Ports.Repository;

public interface IAppointmentRepository
{
    Task<IEnumerable<Appointment>> GetUpcomingAppointmentsAsync(CancellationToken cancellationToken = default);
    Task<Appointment> GetByIdAsync(Guid appointmentId, CancellationToken cancellationToken = default);
    Task AddAsync(Appointment appointment, CancellationToken cancellationToken = default);
    void Update(Appointment appointment);
}