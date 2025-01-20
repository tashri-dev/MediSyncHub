namespace MediSyncHub.AppointmentConfirmationModule.DAL.Repository;

public interface IUnitOfWork
{
    Task<bool> SaveChangesAsync(CancellationToken cancellationToken = default);
}