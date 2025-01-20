using MediSyncHub.SharedKernel.Data;

namespace AppointmentManagement.Core.Domain.Entities;

public class Appointment : BaseEntity<Guid>
{
    public Guid SlotId { get; private set; }
    public Guid PatientId { get; private set; }
    public string PatientName { get; private set; }
    public DateTime ReservedAt { get; private set; } = DateTime.UtcNow;
    public Status Status { get; private set; }
    public DateTime AppointmentTime { get; private set; }

    private Appointment()
    {
    }

    public void MarkAsCancelled()
    {
        if (Status == Status.Cancelled)
            throw new InvalidOperationException("Appointment is already cancelled");
        if (Status == Status.Completed)
            throw new InvalidOperationException("cannot cancel a completed appointment.");
        Status = Status.Cancelled;
        UpdatedAt = DateTime.UtcNow;
    }

    public void MarkAsCompleted()
    {
        if (Status == Status.Cancelled)
            throw new InvalidOperationException("Cannot complete a canceled appointment.");
        if (AppointmentTime > DateTime.UtcNow)
            throw new InvalidOperationException("Cannot complete an appointment that hasn't occurred yet.");

        Status = Status.Completed;
        UpdatedAt = DateTime.UtcNow;
    }


    public static Appointment Create(
        Guid appointmentId,
        Guid slotId,
        Guid patientId,
        string patientName,
        DateTime appointmentTime,
        DateTime createdAt,
        DateTime reservedAt)
    {
        var appointment = new Appointment
        {
            Id = appointmentId,
            SlotId = slotId,
            PatientId = patientId,
            PatientName = patientName,
            ReservedAt = reservedAt,
            CreatedAt = createdAt,
            AppointmentTime = appointmentTime,
            Status = Status.Pending
        };
        return appointment;
    }
}