using AppointmentBooking.Application.UseCases.GetAvailableSlots.Dtos;
using AppointmentBooking.Domain.Repository;
using MediatR;

namespace AppointmentBooking.Application.UseCases.GetAvailableSlots;

public class GetAvailableSlotsHandler(ISlotRepository slotRepository)
    : IRequestHandler<GetAvailableSlotsQuery, IEnumerable<SlotDto>>
{
    public async Task<IEnumerable<SlotDto>> Handle(
        GetAvailableSlotsQuery request,
        CancellationToken cancellationToken)
    {
        var slots = await slotRepository.GetAvailableSlotsAsync(cancellationToken);
        return slots.Select(slot => (SlotDto)slot);
    }
}