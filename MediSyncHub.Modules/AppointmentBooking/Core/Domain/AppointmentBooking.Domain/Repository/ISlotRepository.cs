using MediSyncHub.Modules.AppointmentBookingModule.Domain.Entities;

namespace MediSyncHub.Modules.AppointmentBookingModule.Domain.Repository;

public interface ISlotRepository
{
    Task<IEnumerable<Slot>> GetAvailableSlotsAsync(CancellationToken cancellationToken = default);
    Task<Slot> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task AddAsync(Slot slot, CancellationToken cancellationToken = default);
    Task UpdateAsync(Slot slot);
}