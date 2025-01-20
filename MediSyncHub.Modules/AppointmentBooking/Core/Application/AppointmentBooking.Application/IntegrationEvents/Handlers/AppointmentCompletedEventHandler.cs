using AppointmentBooking.Domain.Repository;
using MediSyncHub.SharedKernel.Events.IntegrationEvents.AppointmentManagement;
using MediSyncHub.SharedKernel.Handlers;
using Microsoft.Extensions.Logging;

namespace AppointmentBooking.Application.IntegrationEvents.Handlers;

public class AppointmentCompletedEventHandler(
    IAppointmentRepository appointmentRepository,
    IUnitOfWork unitOfWork,
    ILogger<AppointmentCompletedEventHandler> logger)
    : IIntegrationEventHandler<AppointmentCompletedIntegrationEvent>
{
    public async Task HandleAsync(AppointmentCompletedIntegrationEvent @event,
        CancellationToken cancellationToken = default)
    {
        try
        {
            var appointment = await appointmentRepository.GetByIdAsync(@event.AppointmentId, cancellationToken);
            appointment.MarkAsCompleted();
            appointmentRepository.Update(appointment);
            await unitOfWork.SaveChangesAsync(cancellationToken);
        }
        catch (Exception e)
        {
            logger.LogError(
                e,
                "Error completing appointment {AppointmentId}",
                @event.AppointmentId);
            throw;
        }
    }
}