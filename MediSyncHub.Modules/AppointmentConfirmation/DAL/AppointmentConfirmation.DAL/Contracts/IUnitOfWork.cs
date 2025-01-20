namespace AppointmentConfirmation.DAL.Contracts;

public interface IUnitOfWork
{
    Task<bool> SaveChangesAsync(CancellationToken cancellationToken = default);
}