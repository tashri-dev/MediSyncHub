using MediSyncHub.Modules.AppointmentBookingModule.Domain.Events;
using MediSyncHub.SharedKernel.Data;
using MediSyncHub.SharedKernel.Data.Events;

namespace MediSyncHub.Modules.AppointmentBookingModule.Domain.Entities;

public class Slot : BaseEntity<Guid>
{
    public DateTime Time { get; private set; }
    public bool IsReserved { get; private set; }
    public decimal Cost { get; private set; }
    private readonly List<IDomainEvent> _domainEvents = new();
    public IReadOnlyCollection<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();

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
        _domainEvents.Add(new SlotReservedDomainEvent(Id));
    }
}