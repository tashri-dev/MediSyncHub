using MediSyncHub.Modules.DoctorAvailabilityModule.Business.Dtos;
using MediSyncHub.Modules.DoctorAvailabilityModule.Data.Entities;
using MediSyncHub.Modules.DoctorAvailabilityModule.Data.Repository;

namespace MediSyncHub.Modules.DoctorAvailabilityModule.Business.Services;

internal class SlotService(ISlotRepository slotRepository) : ISlotService
{
    public async Task<SlotDto> CreateSlotAsync(
        DateTime time,
        Guid doctorId,
        string doctorName,
        decimal cost)
    {
        var slot = Slot.Create(time, cost);
        await slotRepository.AddAsync(slot);

        return slot;
    }

    public async Task<SlotDto> CreateSlotAsync(CreateSlotDto slot, CancellationToken cancellationToken = default)
    {
        await slotRepository.AddAsync(slot);
        return (SlotDto)slot;
    }

    public async Task<IEnumerable<SlotDto>> GetAvailableSlotsAsync(CancellationToken cancellationToken = default)
    {
        var slots = await slotRepository.GetAvailableSlotsAsync(cancellationToken);
        return slots.Select(slot => (SlotDto)slot);
    }

    public async Task<SlotDto> GetSlotAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var slot = await slotRepository.GetByIdAsync(id, cancellationToken);
        return new SlotDto()
        {
            Id = slot.Id,
            IsReserved = slot.IsReserved,
            Time = slot.Time,
            Cost = slot.Cost
        };
    }

    public async Task GetAllSlotsAsync(CancellationToken cancellationToken = default) =>
        await slotRepository.GetAllSlotsAsync(cancellationToken);
}