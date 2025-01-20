using MediSyncHub.AppointmentConfirmationModule.DAL.Extensions;
using MediSyncHub.AppointmentConfirmationModule.Services.EventHandler;
using MediSyncHub.SharedKernel.Events.EventBus;
using MediSyncHub.SharedKernel.Events.IntegrationEvents.AppointmentBooking;
using MediSyncHub.SharedKernel.Events.IntegrationEvents.DoctorAvailability;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MediSyncHub.AppointmentConfirmationModule.Services.Extensions;

public static class DependencyInjection
{
    public static IServiceCollection AddAppointmentConfirmationModule(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.RegisterDal(configuration);
        services.AddScoped<AppointmentBookedEventHandler>();
        return services;
    }

    public static WebApplication ConfigureAppointmentBookedEventBus(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var eventBus = scope.ServiceProvider.GetRequiredService<IEventBus>();
        eventBus.Subscribe<AppointmentBookedIntegrationEvent, AppointmentBookedEventHandler>();
        return app;
    }
}