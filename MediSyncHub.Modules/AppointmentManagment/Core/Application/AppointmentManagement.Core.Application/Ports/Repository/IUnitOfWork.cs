namespace AppointmentManagement.Core.Application.Ports.Repository;

public interface IUnitOfWork
{
    Task<bool> SaveChangesAsync(CancellationToken cancellationToken = default);
}