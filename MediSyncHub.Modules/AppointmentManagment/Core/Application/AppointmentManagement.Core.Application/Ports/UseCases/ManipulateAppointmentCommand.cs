using MediatR;

namespace AppointmentManagement.Core.Application.Ports.UseCases;

public record ManipulateAppointmentResult(Guid AppointmentId);