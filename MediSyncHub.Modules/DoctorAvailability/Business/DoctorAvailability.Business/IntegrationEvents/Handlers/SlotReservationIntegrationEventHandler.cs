using DoctorAvailability.Data.Repository;
using MediSyncHub.SharedKernel.Events.IntegrationEvents.DoctorAvailability;
using MediSyncHub.SharedKernel.Handlers;
using Microsoft.Extensions.Logging;

namespace DoctorAvailability.Business.IntegrationEvents.Handlers;

public class SlotReservationIntegrationEventHandler(
    ISlotRepository slotRepository,
    ILogger<SlotReservationIntegrationEventHandler> logger)
    : IIntegrationEventHandler<SlotIntegrationEvents>
{
    public async Task HandleAsync(SlotIntegrationEvents events, CancellationToken cancellationToken = default)
    {
        var slot = await slotRepository.GetByIdAsync(events.SlotId, cancellationToken);

        slot.MarkAsReserved();
        await slotRepository.UpdateAsync(slot, cancellationToken);

        logger.LogInformation(
            "Slot {SlotId} reservation status updated to {IsReserved}",
            events.SlotId,
            events.IsReserved);
    }
}