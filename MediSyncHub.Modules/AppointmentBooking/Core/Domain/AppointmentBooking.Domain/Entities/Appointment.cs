﻿using MediSyncHub.SharedKernel.Data;
using MediSyncHub.SharedKernel.Data.Events;

namespace AppointmentBooking.Domain.Entities;

public class Appointment : BaseEntity<Guid>
{
    public Guid SlotId { get; private set; }
    public Guid PatientId { get; private set; }
    public string PatientName { get; private set; }
    public DateTime ReservedAt { get; private set; } = DateTime.UtcNow;
    public Status Status { get; private set; }
    public Slot Slot { get; private set; }

    private Appointment()
    {
    }

    public void MarkAsCancelled()
    {
        if (Status == Status.Cancelled)
            throw new InvalidOperationException("Appointment is already cancelled");
        Status = Status.Cancelled;
        UpdatedAt = DateTime.UtcNow;
    }

    public void MarkAsCompleted()
    {
        if (Status == Status.Completed)
            throw new InvalidOperationException("Appointment is already completed");
        Status = Status.Completed;
        UpdatedAt = DateTime.UtcNow;
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
            CreatedAt = DateTime.UtcNow,
            Status = Status.Pending
        };
        return appointment;
    }
}