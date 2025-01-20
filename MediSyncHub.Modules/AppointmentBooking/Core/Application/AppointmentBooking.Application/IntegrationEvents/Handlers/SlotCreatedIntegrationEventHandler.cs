using AppointmentBooking.Domain.Entities;
using AppointmentBooking.Domain.Repository;
using MediSyncHub.SharedKernel.Events.IntegrationEvents.DoctorAvailability;
using MediSyncHub.SharedKernel.Handlers;
using Microsoft.Extensions.Logging;

namespace AppointmentBooking.Application.IntegrationEvents.Handlers;

public class SlotCreatedIntegrationEventHandler(
    ISlotRepository slotRepository,
    IUnitOfWork unitOfWork,
    ILogger<SlotCreatedIntegrationEventHandler> logger)
    : IIntegrationEventHandler<SlotCreatedIntegrationEvent>
{
    public async Task HandleAsync(SlotCreatedIntegrationEvent @event, CancellationToken cancellationToken = default)
    {
        try
        {
            var slotReplica = Slot.Create(
                @event.SlotId,
                @event.Time,
                @event.Cost,
                @event.CreatedAt);

            await slotRepository.AddAsync(slotReplica, cancellationToken);
            await unitOfWork.SaveChangesAsync(cancellationToken);
            logger.LogInformation(
                "Slot replica created for slot {SlotId}",
                slotReplica.Id);
        }
        catch (Exception e)
        {
            logger.LogError(
                e,
                "Error creating slot replica for slot {SlotId}",
                @event.SlotId);
            throw;
        }
    }
}