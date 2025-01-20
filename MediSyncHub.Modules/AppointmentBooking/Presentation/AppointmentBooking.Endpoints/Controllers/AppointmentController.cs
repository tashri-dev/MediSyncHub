using AppointmentBooking.Application.UseCases.BookAppointment;
using AppointmentBooking.Application.UseCases.GetAvailableSlots;
using AppointmentBooking.Application.UseCases.GetAvailableSlots.Dtos;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AppointmentBooking.Endpoints.Controllers;

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