using AppointmentConfirmation.DAL.Contracts;
using AppointmentConfirmation.DAL.models;
using MediSyncHub.SharedKernel.Events.IntegrationEvents.AppointmentBooking;
using MediSyncHub.SharedKernel.Handlers;
using Microsoft.Extensions.Logging;

namespace AppointmentConfirmation.Services.EventHandler;

public class AppointmentBookedEventHandler(
    IAppointmentRepository appointmentRepository,
    INotificationRepository notificationRepository,
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
        await AddNotification(@event, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        logger.LogInformation(
            "New appointment notification sent to doctor {DoctorName}. " +
            "Patient: {PatientName}, " +
            "Time: {AppointmentTime}, " +
            "Booking reference: {AppointmentId}",
            @event.DoctorName,
            @event.PatientName,
            @event.AppointmentTime,
            @event.AppointmentId);

        //notification sent to doctor
        logger.LogInformation(
            "you have a new appointment with {PatientName} at {AppointmentTime}. ",
            @event.PatientName,
            @event.AppointmentTime
        );

        //notification sent to patient
        logger.LogInformation(
            "you have successfully booked an appointment with {DoctorName} at {AppointmentTime}. please be there 30 minutes before the appointment time.",
            @event.DoctorName,
            @event.AppointmentTime
        );
    }

    private async Task AddNotification(AppointmentBookedIntegrationEvent @event, CancellationToken cancellationToken)
    {
        //create a notification
        var notification = Notification.Create(
            @event.AppointmentId,
            @event.PatientId,
            @event.PatientName,
            @event.DoctorId,
            @event.DoctorName
        );

        //add the notification to the repository
        await notificationRepository.AddAsync(notification, cancellationToken);
    }

    private async Task AddAppointment(AppointmentBookedIntegrationEvent @event, CancellationToken cancellationToken)
    {
        //create an appointment
        var appointment = Appointment.Create(
            @event.AppointmentId,
            @event.SlotId,
            @event.PatientId,
            @event.PatientName,
            @event.DoctorId,
            @event.DoctorName,
            @event.AppointmentTime,
            @event.CreatedAt
        );

        //add the appointment to the repository
        await appointmentRepository.AddAsync(appointment, cancellationToken);
    }
}