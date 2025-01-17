using MediSyncHub.Modules.DoctorAvailabilityModule.Data.Repository;
using MediSyncHub.SharedKernel.Events.IntegrationEvents.DoctorAvailability;
using MediSyncHub.SharedKernel.Handlers;
using Microsoft.Extensions.Logging;

namespace MediSyncHub.Modules.DoctorAvailabilityModule.Business.IntegrationEvents.Handlers;

public class SlotReservationIntegrationEventHandler(
    ISlotRepository slotRepository,
    ILogger<SlotReservationIntegrationEventHandler> logger)
    : IIntegrationEventHandler<SlotReservationIntegrationEvent>
{
    public async Task HandleAsync(SlotReservationIntegrationEvent @event, CancellationToken cancellationToken = default)
    {
        var slot = await slotRepository.GetByIdAsync(@event.SlotId, cancellationToken);

        slot.MarkAsReserved();
        await slotRepository.UpdateAsync(slot, cancellationToken);

        logger.LogInformation(
            "Slot {SlotId} reservation status updated to {IsReserved}",  
            @event.SlotId,
            @event.IsReserved);
    }
}