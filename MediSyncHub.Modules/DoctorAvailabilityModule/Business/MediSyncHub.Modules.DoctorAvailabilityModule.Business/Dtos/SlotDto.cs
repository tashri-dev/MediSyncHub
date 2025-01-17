using MediSyncHub.Modules.DoctorAvailabilityModule.Data.Entities;

namespace MediSyncHub.Modules.DoctorAvailabilityModule.Business.Dtos;

public record CreateSlotDto
{
    public DateTime Time { get; set; }
    public decimal Cost { get; set; }

    public static implicit operator Slot(CreateSlotDto dto)
    {
        return Slot.Create(dto.Time, dto.Cost);
    }

    public static explicit operator SlotDto(CreateSlotDto dto)
    {
        Slot slot = dto;
        return (SlotDto)slot;
    }
}

public class SlotDto
{
    public Guid Id { get; set; }
    public DateTime Time { get; set; }
    public bool IsReserved;
    public decimal Cost { get; set; }

    public static implicit operator SlotDto(Slot slot)
    {
        return new SlotDto
        {
            Id = slot.Id,
            Time = slot.Time,
            Cost = slot.Cost
        };
    }
}