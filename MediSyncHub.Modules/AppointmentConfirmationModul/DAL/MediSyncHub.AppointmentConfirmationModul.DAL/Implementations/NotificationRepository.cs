using MediSyncHub.AppointmentConfirmationModule.DAL.Database;
using MediSyncHub.AppointmentConfirmationModule.DAL.models;

namespace MediSyncHub.AppointmentConfirmationModule.DAL.Repository;

internal class NotificationRepository(ConfirmationDbContext context) : INotificationRepository
{
    public async Task AddAsync(Notification notification)
    {
        await context.Notifications.AddAsync(notification);
    }
}