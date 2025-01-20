using AppointmentManagement.Core.Application.Ports.Repository;
using AppointmentManagement.Shell.Infrastructure.Adapters.Persistence;
using AppointmentManagement.Shell.Infrastructure.Adapters.Repository;
using AppointmentManagement.Shell.Infrastructure.EventHandlers;
using MediSyncHub.SharedKernel.Events.EventBus;
using MediSyncHub.SharedKernel.Events.IntegrationEvents.AppointmentBooking;
using MediSyncHub.SharedKernel.Shared;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AppointmentManagement.Shell.Infrastructure.Extensions;

public static class DependencyInjection
{
    public static IServiceCollection RegisterInfrastructure(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddScoped<IAppointmentRepository, AppointmentRepository>();

        //add dbContext
        // Register DbContext
        var connectionString = configuration.GetConnectionString(Constants.DB_CONNECTION_STRING);
        services.AddDbContext<ManagementDbContext>(options =>
            options.UseNpgsql(
                connectionString,
                b => b.MigrationsHistoryTable("__EFMigrationsHistory", "management")));

        services.AddScoped<IUnitOfWork>(provider =>
            provider.GetService<ManagementDbContext>());
        // Register event handlers
        services.AddScoped<AppointmentBookedEventHandler>();
        return services;
    }

    public static WebApplication ConfigureAppointmentManamgemntEventBus(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var eventBus = scope.ServiceProvider.GetRequiredService<IEventBus>();
        eventBus.Subscribe<AppointmentBookedIntegrationEvent, AppointmentBookedEventHandler>();
        return app;
    }
}