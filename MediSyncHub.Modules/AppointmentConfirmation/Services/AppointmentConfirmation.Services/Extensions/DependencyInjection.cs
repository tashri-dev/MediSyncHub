using AppointmentConfirmation.DAL.Extensions;
using AppointmentConfirmation.Services.EventHandler;
using MediSyncHub.SharedKernel.Events.EventBus;
using MediSyncHub.SharedKernel.Events.IntegrationEvents.AppointmentBooking;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AppointmentConfirmation.Services.Extensions;

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