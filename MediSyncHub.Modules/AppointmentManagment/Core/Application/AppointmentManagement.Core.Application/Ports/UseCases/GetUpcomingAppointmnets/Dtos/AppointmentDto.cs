using AppointmentManagement.Core.Domain.Entities;

namespace AppointmentManagement.Core.Application.Ports.UseCases.GetUpcomingAppointmnets.Dtos;

public record AppointmentDto(Guid AppointmentId, string PatientName, DateTime AppointmentTime)
{
    public static AppointmentDto FromAppointment(Appointment appointment)
    {
        return new AppointmentDto(appointment.Id, appointment.PatientName, appointment.AppointmentTime);
    }
}