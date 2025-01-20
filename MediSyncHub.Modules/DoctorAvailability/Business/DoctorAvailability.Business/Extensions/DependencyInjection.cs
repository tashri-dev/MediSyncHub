using DoctorAvailability.Business.Data;
using DoctorAvailability.Business.IntegrationEvents.Handlers;
using DoctorAvailability.Business.Repositories;
using DoctorAvailability.Business.Services;
using DoctorAvailability.Data.Repository;
using MediSyncHub.SharedKernel.Events.EventBus;
using MediSyncHub.SharedKernel.Events.IntegrationEvents.DoctorAvailability;
using MediSyncHub.SharedKernel.Shared;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace DoctorAvailability.Business.Extensions;

public static class DependencyInjection
{
    public static IServiceCollection AddBusinessLayer(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString(Constants.DB_CONNECTION_STRING);
        services.AddDbContext<DoctorAvailabilityDbContext>(options => { options.UseNpgsql(connectionString); });
        services.AddScoped<ISlotRepository, SlotRepository>();
        services.AddScoped<ISlotService, SlotService>();
        services.AddScoped<SlotReservationIntegrationEventHandler>();
        return services;
    }

    public static WebApplication ConfigureDoctorAvailabilityEventBus(this WebApplication app)
    {
        using (var scope = app.Services.CreateScope())
        {
            var eventBus = scope.ServiceProvider.GetRequiredService<IEventBus>();
            eventBus.Subscribe<SlotIntegrationEvents, SlotReservationIntegrationEventHandler>();
        }

        return app;
    }
}