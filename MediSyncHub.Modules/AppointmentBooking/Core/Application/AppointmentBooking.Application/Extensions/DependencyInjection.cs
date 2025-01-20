using AppointmentBooking.Application.IntegrationEvents.Handlers;
using MediSyncHub.SharedKernel.Events.EventBus;
using MediSyncHub.SharedKernel.Events.IntegrationEvents.DoctorAvailability;
using MediSyncHub.SharedKernel.Handlers;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace AppointmentBooking.Application.Extensions;

public static class DependencyInjection
{
    public static IServiceCollection RegisterApplication(this IServiceCollection services)
    {
        // Register MediatR handlers
        services.AddMediatR(cfg =>
            cfg.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly));

        // Register event handlers
        services.AddScoped<SlotCreatedIntegrationEventHandler>();

        return services;
    }

    public static WebApplication ConfigureAppointmentBookingEventBus(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var eventBus = scope.ServiceProvider.GetRequiredService<IEventBus>();
        eventBus.Subscribe<SlotCreatedIntegrationEvent, SlotCreatedIntegrationEventHandler>();
        return app;
    }
}