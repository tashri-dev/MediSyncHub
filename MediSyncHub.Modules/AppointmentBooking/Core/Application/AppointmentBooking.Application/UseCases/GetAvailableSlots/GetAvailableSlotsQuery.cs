using MediatR;
using MediSyncHub.Modules.AppointmentBookingModule.Application.UseCases.GetAvailableSlots.Dtos;

namespace MediSyncHub.Modules.AppointmentBookingModule.Application.UseCases.GetAvailableSlots;

public record GetAvailableSlotsQuery : IRequest<IEnumerable<SlotDto>>;
