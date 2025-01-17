using MediSyncHub.Modules.DoctorAvailabilityModule.Business.Data;
using MediSyncHub.Modules.DoctorAvailabilityModule.Business.Repositories;
using MediSyncHub.Modules.DoctorAvailabilityModule.Business.Services;
using MediSyncHub.Modules.DoctorAvailabilityModule.Data.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MediSyncHub.Modules.DoctorAvailabilityModule.Business.Extensions;

public static class DependencyInjection
{
    public static IServiceCollection AddBusinessLayer(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DoctorAvailabilityConnectionString");
        services.AddDbContext<DoctorAvailabilityDbContext>(options =>
        {
            options.UseNpgsql(connectionString);
        });
        services.AddScoped<ISlotRepository, SlotRepository>();
        services.AddScoped<ISlotService, SlotService>();
        return services;
    }
    
}
