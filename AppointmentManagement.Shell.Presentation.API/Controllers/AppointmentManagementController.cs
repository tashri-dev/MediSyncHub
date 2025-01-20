using AppointmentManagement.Core.Application.Ports.UseCases;
using AppointmentManagement.Core.Application.Ports.UseCases.CancelAppointment;
using AppointmentManagement.Core.Application.Ports.UseCases.CompleteAppointmnet;
using AppointmentManagement.Core.Application.Ports.UseCases.GetUpcomingAppointmnets;
using AppointmentManagement.Core.Application.Ports.UseCases.GetUpcomingAppointmnets.Dtos;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AppointmentManagement.Shell.Presentation.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AppointmentManagementController(IMediator mediator) : ControllerBase
{
    [HttpPost("complete-appointment")]
    public async Task<ActionResult<ManipulateAppointmentResult>> CompleteAppointment(
        CompleteAppointmentCommand command)
    {
        var result = await mediator.Send(command);
        return Ok(result);
    }

    [HttpPost("cancel-appointment")]
    public async Task<ActionResult<ManipulateAppointmentResult>> CancelAppointmnet(
        CancelAppointmentCommand command)
    {
        var result = await mediator.Send(command);
        return Ok(result);
    }

    [HttpGet("list-upcoming-appointments")]
    public async Task<ActionResult<IEnumerable<AppointmentDto>>> ListAvailableSlots()
    {
        GetUpcomingAppointmentQuery query = new();
        var result = await mediator.Send(query);
        return Ok(result);
    }
}