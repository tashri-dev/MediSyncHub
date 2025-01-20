using AppointmentManagement.Core.Application.Ports.Repository;
using AppointmentManagement.Core.Application.Ports.UseCases.CompleteAppointmnet;
using AppointmentManagement.Core.Domain.Entities;
using MediatR;
using MediSyncHub.SharedKernel.Events.EventBus;
using MediSyncHub.SharedKernel.Events.IntegrationEvents.AppointmentManagement;
using Microsoft.Extensions.Logging;

namespace AppointmentManagement.Core.Application.Ports.UseCases.CancelAppointment;

public record CancelAppointmentCommand(Guid AppointmentId) : IRequest<ManipulateAppointmentResult>;

public class CancelAppointmentEventHandler(
    IAppointmentRepository appointmentRepository,
    IEventBus eventBus,
    IUnitOfWork unitOfWork,
    ILogger<CompleteAppointmentHandler> logger)
    : IRequestHandler<CancelAppointmentCommand, ManipulateAppointmentResult>
{
    public async Task<ManipulateAppointmentResult> Handle(CancelAppointmentCommand request,
        CancellationToken cancellationToken)
    {
        logger.LogInformation("completing appointment {AppointmentId}", request.AppointmentId);
        var appointment = await appointmentRepository.GetByIdAsync(request.AppointmentId, cancellationToken);
        await PublishEventsAsync(appointment, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        logger.LogInformation("Appointment {AppointmentId} booked", appointment.Id);
        return new ManipulateAppointmentResult(appointment.Id);
    }

    private async Task PublishEventsAsync(Appointment appointment,
        CancellationToken cancellationToken = default)
    {
        // Integration events
        var integrationEvents = new object[]
        {
            new AppointmentCancelledIntegrationEvent(
                appointment.Id),
        };

        var integrationEventTasks = integrationEvents.Select(e => eventBus.PublishAsync(e, cancellationToken));
        await Task.WhenAll(integrationEventTasks);
    }
}