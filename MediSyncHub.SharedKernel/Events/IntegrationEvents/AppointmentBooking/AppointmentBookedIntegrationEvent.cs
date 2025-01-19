namespace MediSyncHub.SharedKernel.Events.IntegrationEvents.AppointmentBooking;

public record AppointmentBookedIntegrationEvent(
    Guid AppointmentId,
    Guid SlotId,
    Guid PatientId,
    string PatientName,
    Guid DoctorId,
    string DoctorName,
    DateTime BookedAt,
    DateTime AppointmentTime,
    DateTime CreatedAt
);