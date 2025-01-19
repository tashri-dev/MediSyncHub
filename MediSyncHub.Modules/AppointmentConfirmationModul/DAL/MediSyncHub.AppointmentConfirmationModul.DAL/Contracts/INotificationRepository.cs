using MediSyncHub.AppointmentConfirmationModule.DAL.models;

namespace MediSyncHub.AppointmentConfirmationModule.DAL.Repository;

public interface INotificationRepository
{
    Task AddAsync(Notification notification);
}