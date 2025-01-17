namespace MediSyncHub.Modules.DoctorAvailabilityModule.Business.Services;


internal class SlotService:ISlotService
{
    private readonly ISlotRepository _slotRepository;

    public SlotService(ISlotRepository slotRepository)
    {
        _slotRepository = slotRepository;
    }

    public async Task<IEnumerable<SlotDto>> GetAvailableSlotsAsync()
    {
        var slots = await _slotRepository.GetAvailableSlotsAsync();
        return slots.Select(s => new SlotDto(
            s.Id,
            s.Time,
            s.DoctorId,
            s.DoctorName,
            s.IsReserved,
            s.Cost));
    }

    public async Task<SlotDto> CreateSlotAsync(
        DateTime time,
        Guid doctorId,
        string doctorName,
        decimal cost)
    {
        var slot = Slot.Create(time, doctorId, doctorName, cost);
        await _slotRepository.AddAsync(slot);

        return new SlotDto(
            slot.Id,
            slot.Time,
            slot.DoctorId,
            slot.DoctorName,
            slot.IsReserved,
            slot.Cost);
    }
}

