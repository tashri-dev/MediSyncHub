using MediSyncHub.SharedKernel.Data;

namespace MediSyncHub.AppointmentConfirmationModule.DAL.models;

public class Appointment : BaseEntity<Guid>
{
    public Guid SlotId { get; private set; }
    public Guid PatientId { get; private set; }
    public string PatientName { get; private set; }
    public Guid DoctorId { get; private set; }
    public string DoctorName { get; private set; }
    public DateTime ReservedAt { get; private set; } = DateTime.UtcNow;
    public DateTime AppointmentDate { get; private set; }
    private Appointment()
    {
    }

    public static Appointment Create(
        Guid appointmentId,
        Guid slotId,
        Guid patientId,
        string patientName,
        Guid doctorId,
        string doctorName,
        DateTime appointmentDate,
        DateTime createdAt
        )
    {
        var appointment = new Appointment
        {
            Id = appointmentId,
            SlotId = slotId,
            PatientId = patientId,
            PatientName = patientName,
            ReservedAt = DateTime.UtcNow,
            DoctorId = doctorId,
            DoctorName = doctorName,
            CreatedAt = createdAt,
            AppointmentDate = appointmentDate
        };
        return appointment;
    }
}