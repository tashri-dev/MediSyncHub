using MediSyncHub.Modules.DoctorAvailabilityModule.Data.Entities;

namespace MediSyncHub.Modules.DoctorAvailabilityModule.Data.Repository;

public interface ISlotRepository
{
    Task<IEnumerable<Slot>> GetAvailableSlotsAsync();
    Task<Slot> GetByIdAsync(Guid id);
    Task AddAsync(Slot slot);
    Task UpdateAsync(Slot slot);
}
