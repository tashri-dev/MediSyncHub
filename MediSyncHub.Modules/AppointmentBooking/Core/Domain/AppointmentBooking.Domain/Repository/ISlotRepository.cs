using AppointmentBooking.Domain.Entities;

namespace AppointmentBooking.Domain.Repository;

public interface ISlotRepository
{
    Task<IEnumerable<Slot>> GetAvailableSlotsAsync(CancellationToken cancellationToken = default);
    Task<Slot> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task AddAsync(Slot slot, CancellationToken cancellationToken = default);
    Task UpdateAsync(Slot slot);
}