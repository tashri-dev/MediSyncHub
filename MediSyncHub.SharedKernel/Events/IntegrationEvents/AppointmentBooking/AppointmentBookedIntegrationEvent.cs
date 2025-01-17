namespace MediSyncHub.SharedKernel.Events.IntegrationEvents.AppointmentBooking;

public record AppointmentBookedIntegrationEvent(
    Guid AppointmentId,
    Guid SlotId,
    Guid PatientId,
    string PatientName,
    DateTime BookedAt
);