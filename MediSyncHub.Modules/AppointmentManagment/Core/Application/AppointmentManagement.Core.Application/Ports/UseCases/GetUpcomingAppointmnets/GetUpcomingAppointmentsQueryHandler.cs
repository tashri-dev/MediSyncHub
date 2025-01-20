using AppointmentManagement.Core.Application.Ports.Repository;
using AppointmentManagement.Core.Application.Ports.UseCases.CompleteAppointmnet;
using AppointmentManagement.Core.Application.Ports.UseCases.GetUpcomingAppointmnets.Dtos;
using MediatR;
using Microsoft.Extensions.Logging;

namespace AppointmentManagement.Core.Application.Ports.UseCases.GetUpcomingAppointmnets;

public class GetUpcomingAppointmentsQueryHandler(
    IAppointmentRepository appointmentRepository,
    ILogger<CompleteAppointmentHandler> logger)
    : IRequestHandler<GetUpcomingAppointmentQuery, IEnumerable<AppointmentDto>>
{
    public async Task<IEnumerable<AppointmentDto>> Handle(GetUpcomingAppointmentQuery request,
        CancellationToken cancellationToken)
    {
        logger.LogInformation("Getting upcoming appointments");
        var appointments = await appointmentRepository.GetUpcomingAppointmentsAsync(cancellationToken);
        return appointments.Select(a => new AppointmentDto(a.Id, a.PatientName, a.AppointmentTime));
    }
}