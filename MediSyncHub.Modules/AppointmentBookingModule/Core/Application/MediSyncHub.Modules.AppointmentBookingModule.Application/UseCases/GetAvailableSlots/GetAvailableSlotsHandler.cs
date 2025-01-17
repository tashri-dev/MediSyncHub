using MediatR;
using MediSyncHub.Modules.AppointmentBookingModule.Application.UseCases.GetAvailableSlots.Dtos;
using MediSyncHub.Modules.AppointmentBookingModule.Domain.Repository;

namespace MediSyncHub.Modules.AppointmentBookingModule.Application.UseCases.GetAvailableSlots
{
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
}