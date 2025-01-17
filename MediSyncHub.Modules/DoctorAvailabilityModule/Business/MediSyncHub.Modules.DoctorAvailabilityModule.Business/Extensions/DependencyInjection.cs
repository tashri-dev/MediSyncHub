﻿using MediSyncHub.Modules.DoctorAvailabilityModule.Business.Data;
using MediSyncHub.Modules.DoctorAvailabilityModule.Business.IntegrationEvents.Handlers;
using MediSyncHub.Modules.DoctorAvailabilityModule.Business.Repositories;
using MediSyncHub.Modules.DoctorAvailabilityModule.Business.Services;
using MediSyncHub.Modules.DoctorAvailabilityModule.Data.Repository;
using MediSyncHub.SharedKernel.Events.EventBus;
using MediSyncHub.SharedKernel.Events.IntegrationEvents.DoctorAvailability;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MediSyncHub.Modules.DoctorAvailabilityModule.Business.Extensions;

public static class DependencyInjection
{
    public static IServiceCollection AddBusinessLayer(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");
        services.AddDbContext<DoctorAvailabilityDbContext>(options =>
        {
            options.UseNpgsql(connectionString);
        });
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
            eventBus.Subscribe<SlotReservationIntegrationEvent, SlotReservationIntegrationEventHandler>();
            
        }
        return app;
    }
}
