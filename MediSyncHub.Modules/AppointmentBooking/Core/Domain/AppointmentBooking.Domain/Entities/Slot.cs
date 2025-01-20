using MediSyncHub.SharedKernel.Data;
using MediSyncHub.SharedKernel.Data.Events;

namespace AppointmentBooking.Domain.Entities;

public class Slot : BaseEntity<Guid>
{
    public DateTime Time { get; private set; }
    public bool IsReserved { get; private set; }
    public decimal Cost { get; private set; }
    private Slot()
    {
    }

    public static Slot Create(
        Guid Id,
        DateTime time,
        decimal cost, DateTime createdAt)
    {
        return new Slot
        {
            Id = Id,
            Time = time,
            IsReserved = false,
            Cost = cost,
            CreatedAt = createdAt
        };
    }

    public void Reserve(Guid patientId)
    {
        if (IsReserved)
            throw new InvalidOperationException("Slot is already reserved");
        IsReserved = true;
    }
}