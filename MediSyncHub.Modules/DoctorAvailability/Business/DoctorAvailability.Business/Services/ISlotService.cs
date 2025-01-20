using MediSyncHub.Modules.DoctorAvailabilityModule.Business.Dtos;

namespace MediSyncHub.Modules.DoctorAvailabilityModule.Business.Services;

public interface ISlotService
{
    Task<SlotDto> CreateSlotAsync(CreateSlotDto slot, CancellationToken cancellationToken = default);
    Task<SlotDto> GetSlotAsync(Guid id, CancellationToken cancellationToken = default);
    Task<IEnumerable<SlotDto>> GetAllSlotsAsync(CancellationToken cancellationToken = default);
}
