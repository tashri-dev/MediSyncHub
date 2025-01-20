using AppointmentConfirmation.DAL.Contracts;
using AppointmentConfirmation.DAL.Database;
using AppointmentConfirmation.DAL.models;

namespace AppointmentConfirmation.DAL.Implementations;

internal class NotificationRepository(ConfirmationDbContext context) : INotificationRepository
{
    public async Task AddAsync(Notification notification, CancellationToken cancellationToken)
    {
        await context.Notifications.AddAsync(notification, cancellationToken);
    }
}