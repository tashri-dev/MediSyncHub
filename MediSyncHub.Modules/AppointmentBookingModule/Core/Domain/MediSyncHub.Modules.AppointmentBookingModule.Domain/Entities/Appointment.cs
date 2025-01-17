using MediSyncHub.Modules.AppointmentBookingModule.Domain.Events;
using MediSyncHub.SharedKernel.Data;
using MediSyncHub.SharedKernel.Data.Events;

namespace MediSyncHub.Modules.AppointmentBookingModule.Domain.Entities;

public class Appointment : BaseEntity<Guid>
{
    public Guid SlotId { get; private set; }
    public Guid PatientId { get; private set; }
    public string PatientName { get; private set; }
    public DateTime ReservedAt { get; private set; } = DateTime.UtcNow;
    private readonly List<IDomainEvent> _domainEvents = new();
    public IReadOnlyCollection<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();

    private Appointment() { }

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

        appointment._domainEvents.Add(new AppointmentBookedDomainEvent(appointment));
        return appointment;
    }

}
