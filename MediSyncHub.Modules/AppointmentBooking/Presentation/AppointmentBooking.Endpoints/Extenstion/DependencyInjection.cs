using MediSyncHub.Modules.AppointmentBookingModule.Application.Extensions;
using MediSyncHub.Modules.AppointmentBookingModule.Domain.Repository;
using MediSyncHub.Modules.AppointmentBookingModule.Infrastructure.Data.Database;
using MediSyncHub.Modules.AppointmentBookingModule.Infrastructure.Data.Repository;
using MediSyncHub.Modules.AppointmentBookingModule.Infrastructure.Extensions;
using MediSyncHub.SharedKernel.Events.IntegrationEvents.DoctorAvailability;
using MediSyncHub.SharedKernel.Handlers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MediSyncHub.Modules.AppointmentBookingModule.Endpoints.Extenstion;

public static class DependencyInjection
{
    public static IServiceCollection AddAppointmentBookingModule(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.RegisterInfrastructure(configuration);
        services.RegisterApplication();
        return services;
    }
}