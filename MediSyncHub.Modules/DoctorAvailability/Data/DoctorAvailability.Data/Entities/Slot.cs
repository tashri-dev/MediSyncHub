using MediSyncHub.SharedKernel.Data;

namespace DoctorAvailability.Data.Entities;

public class Slot : BaseEntity<Guid>
{
    public DateTime Time { get; private set; }
    public bool IsReserved { get; private set; }
    public decimal Cost { get; private set; }

    private Slot()
    {
    }

    public static Slot Create(
        DateTime time,
        decimal cost)
    {
        return new Slot
        {
            Id = Guid.NewGuid(),
            Time = time,
            IsReserved = false,
            Cost = cost,
            CreatedAt = DateTime.UtcNow
        };
    }

    // This will be called by an integration event handler
    public void MarkAsReserved()
    {
        IsReserved = true;
        UpdatedAt = DateTime.UtcNow;
    }
}