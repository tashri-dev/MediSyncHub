using MediatR;

namespace MediSyncHub.Modules.AppointmentBookingModule.Application.UseCases.BookAppointment;

public record BookAppointmentCommand(
    Guid SlotId,
    Guid PatientId,
    string PatientName
) : IRequest<BookAppointmentResult>;

public record BookAppointmentResult(Guid AppointmentId);