using DoctorAvailability.Business.Dtos;
using DoctorAvailability.Data.Entities;
using DoctorAvailability.Data.Repository;
using MediSyncHub.SharedKernel.Events.EventBus;
using MediSyncHub.SharedKernel.Events.IntegrationEvents.DoctorAvailability;
using Microsoft.Extensions.Logging;

namespace DoctorAvailability.Business.Services;

internal class SlotService(
    ISlotRepository slotRepository,
    IEventBus eventBus,
    ILogger<SlotService> logger)
    : ISlotService
{
    public async Task<SlotDto> CreateSlotAsync(CreateSlotDto slotDto, CancellationToken cancellationToken = default)
    {
        var slot = (Slot)slotDto;
        await slotRepository.AddAsync(slot, cancellationToken);

        // Publish integration event
        var slotCreatedIntegrationEvent = new SlotCreatedIntegrationEvent(
            slot.Id,
            slot.Time,
            slot.Cost,
            slot.CreatedAt);

        try
        {
            await eventBus.PublishAsync(slotCreatedIntegrationEvent, cancellationToken);
            logger.LogInformation(
                "Published SlotCreatedIntegrationEvent for slot {SlotId}",
                slot.Id);
        }
        catch (Exception ex)
        {
            logger.LogError(
                ex,
                "Error publishing SlotCreatedIntegrationEvent for slot {SlotId}",
                slot.Id);
        }

        return (SlotDto)slot;
    }

    public async Task<SlotDto> GetSlotAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var slot = await slotRepository.GetByIdAsync(id, cancellationToken);
        return (SlotDto)slot;
    }

    public async Task<IEnumerable<SlotDto>> GetAllSlotsAsync(CancellationToken cancellationToken = default)
    {
        var slots = await slotRepository.GetAllSlotsAsync(cancellationToken);
        return slots.Select(slot => (SlotDto)slot);
    }
}