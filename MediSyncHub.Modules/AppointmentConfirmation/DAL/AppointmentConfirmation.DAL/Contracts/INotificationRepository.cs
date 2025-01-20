using AppointmentConfirmation.DAL.models;

namespace AppointmentConfirmation.DAL.Contracts;

public interface INotificationRepository
{
    Task AddAsync(Notification notification, CancellationToken cancellationToken);
}