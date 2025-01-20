using MediSyncHub.SharedKernel.Data;
using MediSyncHub.SharedKernel.Data.Events;

namespace AppointmentBooking.Domain.Entities;

public class Appointment : BaseEntity<Guid>
{
    public Guid SlotId { get; private set; }
    public Guid PatientId { get; private set; }
    public string PatientName { get; private set; }
    public DateTime ReservedAt { get; private set; } = DateTime.UtcNow;

    private Appointment()
    {
    }

    public static Appointment Create(
        Guid slotId,
        Guid patientId,
        string patientName)
    {
        var appointment = new Appointment
        {
            Id = Guid.NewGuid(),
            SlotId = slotId,
            PatientId = patientId,
            PatientName = patientName,
            ReservedAt = DateTime.UtcNow,
            CreatedAt = DateTime.UtcNow
        };
        return appointment;
    }
}