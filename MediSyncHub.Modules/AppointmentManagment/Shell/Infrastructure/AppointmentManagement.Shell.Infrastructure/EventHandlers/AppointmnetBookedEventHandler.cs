using AppointmentManagement.Core.Application.Ports.Repository;
using AppointmentManagement.Core.Domain.Entities;
using MediSyncHub.SharedKernel.Events.IntegrationEvents.AppointmentBooking;
using MediSyncHub.SharedKernel.Handlers;
using Microsoft.Extensions.Logging;

namespace AppointmentManagement.Shell.Infrastructure.EventHandlers;

public class AppointmentBookedEventHandler(
    IAppointmentRepository appointmentRepository,
    IUnitOfWork unitOfWork,
    ILogger<AppointmentBookedEventHandler> logger)
    : IIntegrationEventHandler<AppointmentBookedIntegrationEvent>
{
    public async Task HandleAsync(AppointmentBookedIntegrationEvent @event,
        CancellationToken cancellationToken = default)
    {
        //log the event
        logger.LogInformation("Appointment booked for patient {PatientId}", @event.PatientId);
        await AddAppointment(@event, cancellationToken);
        if (!await unitOfWork.SaveChangesAsync(cancellationToken))
        {
            //log failure
            logger.LogError("Appointment {AppointmentId} not added to db",
                @event.AppointmentId);
        }

        //log that appointment added
        logger.LogInformation(
            "Appointment {AppointmentId} added",
            @event.AppointmentId);
    }


    private async Task AddAppointment(AppointmentBookedIntegrationEvent @event, CancellationToken cancellationToken)
    {
        //create an appointment
        var appointment = Appointment.Create(
            @event.AppointmentId,
            @event.SlotId,
            @event.PatientId,
            @event.PatientName,
            @event.AppointmentTime,
            @event.CreatedAt,
            @event.BookedAt
        );

        //add the appointment to the repository
        await appointmentRepository.AddAsync(appointment, cancellationToken);
    }
}