using AppointmentManagement.Core.Application.Ports.UseCases.GetUpcomingAppointmnets.Dtos;
using MediatR;

namespace AppointmentManagement.Core.Application.Ports.UseCases.GetUpcomingAppointmnets;

public record GetUpcomingAppointmentQuery : IRequest<IEnumerable<AppointmentDto>>;