namespace MediSyncHub.Modules.AppointmentBookingModule.Domain.Repository;

public interface IUnitOfWork
{
    Task<bool> SaveChangesAsync(CancellationToken cancellationToken = default);
}