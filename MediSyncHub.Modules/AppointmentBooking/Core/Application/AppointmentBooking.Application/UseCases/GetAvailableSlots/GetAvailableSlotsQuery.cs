using AppointmentBooking.Application.UseCases.GetAvailableSlots.Dtos;
using MediatR;

namespace AppointmentBooking.Application.UseCases.GetAvailableSlots;

public record GetAvailableSlotsQuery : IRequest<IEnumerable<SlotDto>>;