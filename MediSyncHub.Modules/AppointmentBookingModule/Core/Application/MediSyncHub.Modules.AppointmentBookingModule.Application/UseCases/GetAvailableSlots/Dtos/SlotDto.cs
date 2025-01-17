using MediSyncHub.Modules.AppointmentBookingModule.Domain.Entities;

namespace MediSyncHub.Modules.AppointmentBookingModule.Application.UseCases.GetAvailableSlots.Dtos;

public record CreateSlotDto
{
    public Guid Id { get; set; }
    public DateTime Time { get; set; }
    public decimal Cost { get; set; }
    public DateTime CreatedAt { get; set; }

    public static implicit operator Slot(CreateSlotDto dto)
    {
        return Slot.Create(dto.Id, dto.Time, dto.Cost, dto.CreatedAt);
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