namespace MediSyncHub.Modules.DoctorAvailabilityModule.Business.Services;

public interface ISlotService
{
    Task<SlotDto> CreateSlotAsync(DateTime time, Guid doctorId, global::System.String doctorName, global::System.Decimal cost);
    Task<IEnumerable<SlotDto>> GetAvailableSlotsAsync();
}
