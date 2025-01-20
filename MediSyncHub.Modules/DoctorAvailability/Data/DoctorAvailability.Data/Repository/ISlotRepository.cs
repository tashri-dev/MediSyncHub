using MediSyncHub.Modules.DoctorAvailabilityModule.Data.Entities;

namespace MediSyncHub.Modules.DoctorAvailabilityModule.Data.Repository;

public interface ISlotRepository
{
    Task<IEnumerable<Slot>> GetAvailableSlotsAsync(CancellationToken cancellationToken = default); 
    Task<IEnumerable<Slot>> GetAllSlotsAsync(CancellationToken cancellationToken = default);
    Task<Slot> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task AddAsync(Slot slot, CancellationToken cancellationToken = default);
    Task UpdateAsync(Slot slot, CancellationToken cancellationToken = default);
}