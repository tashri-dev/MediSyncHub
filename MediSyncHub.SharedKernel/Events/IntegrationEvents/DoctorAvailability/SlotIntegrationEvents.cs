namespace MediSyncHub.SharedKernel.Events.IntegrationEvents.DoctorAvailability;

public record SlotIntegrationEvents(
    Guid SlotId,
    bool IsReserved,
    DateTime UpdatedAt
);

public record SlotCreatedIntegrationEvent(
    Guid SlotId,
    DateTime Time,
    decimal Cost,
    DateTime CreatedAt
);