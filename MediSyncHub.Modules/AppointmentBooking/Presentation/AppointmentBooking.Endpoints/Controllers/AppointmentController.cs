using MediatR;
using MediSyncHub.Modules.AppointmentBookingModule.Application.UseCases.BookAppointment;
using MediSyncHub.Modules.AppointmentBookingModule.Application.UseCases.GetAvailableSlots;
using MediSyncHub.Modules.AppointmentBookingModule.Application.UseCases.GetAvailableSlots.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace MediSyncHub.Modules.AppointmentBookingModule.Endpoints.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AppointmentsController(IMediator mediator) : ControllerBase
{
    [HttpPost("book-appointment")]
    public async Task<ActionResult<BookAppointmentResult>> BookAppointment(
        BookAppointmentCommand command)
    {
        var result = await mediator.Send(command);
        return Ok(result);
    }

    [HttpGet("list-available-slots")]
    public async Task<ActionResult<IEnumerable<SlotDto>>> ListAvailableSlots()
    {
        GetAvailableSlotsQuery query = new();
        var result = await mediator.Send(query);
        return Ok(result);
    }
}