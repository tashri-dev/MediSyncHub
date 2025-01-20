using MediatR;
using MediSyncHub.Modules.AppointmentBookingModule.Domain.Entities;
using MediSyncHub.Modules.AppointmentBookingModule.Domain.Repository;
using MediSyncHub.SharedKernel.Events.EventBus;
using MediSyncHub.SharedKernel.Events.IntegrationEvents.AppointmentBooking;
using MediSyncHub.SharedKernel.Events.IntegrationEvents.DoctorAvailability;
using Microsoft.Extensions.Logging;

namespace MediSyncHub.Modules.AppointmentBookingModule.Application.UseCases.BookAppointment;

public class BookAppointmentHandler(
    IAppointmentRepository appointmentRepository,
    ISlotRepository slotRepository,
    IEventBus eventBus,
    IUnitOfWork unitOfWork,
    ILogger<BookAppointmentHandler> logger)
    : IRequestHandler<BookAppointmentCommand, BookAppointmentResult>
{
    private readonly ISlotRepository _slotRepository = slotRepository;

    public async Task<BookAppointmentResult> Handle(
        BookAppointmentCommand request,
        CancellationToken cancellationToken)
    {
        logger.LogInformation("Booking appointment for patient {PatientId}", request.PatientId);
        var slot = await GetAndValidateSlotAsync(request.SlotId);
        var appointment = await CreateAppointmentAsync(request, slot, cancellationToken);
        await PublishEventsAsync(appointment, slot, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        logger.LogInformation("Appointment {AppointmentId} booked", appointment.Id);
        return new BookAppointmentResult(appointment.Id);
    }

    private async Task<Slot> GetAndValidateSlotAsync(Guid slotId)
    {
        var slot = await _slotRepository.GetByIdAsync(slotId);
        if (slot == null)
            throw new Exception($"Slot with ID {slotId} not found");

        return slot;
    }

    private async Task<Appointment> CreateAppointmentAsync(
        BookAppointmentCommand request,
        Slot slot, CancellationToken cancellationToken)
    {
        var appointment = Appointment.Create(
            request.SlotId,
            request.PatientId,
            request.PatientName);

        slot.Reserve(request.PatientId);

        await appointmentRepository.AddAsync(appointment, cancellationToken);
        await _slotRepository.UpdateAsync(slot);

        return appointment;
    }

    private async Task PublishEventsAsync(Appointment appointment, Slot slot,
        CancellationToken cancellationToken = default)
    {
        // Integration events
        var integrationEvents = new object[]
        {
            new SlotReservationIntegrationEvent(
                slot.Id,
                true,
                DateTime.UtcNow),

            new AppointmentBookedIntegrationEvent(
                appointment.Id,
                appointment.SlotId,
                appointment.PatientId,
                appointment.PatientName,
                Guid.NewGuid(), //Todo: get doctor id from slot
                "Omar Taha", //Todo: get doctor name from slot
                appointment.ReservedAt,
                slot.Time,
                appointment.CreatedAt)
        };

        var integrationEventTasks = integrationEvents.Select(e => eventBus.PublishAsync(e, cancellationToken));
        await Task.WhenAll(integrationEventTasks);
    }
}