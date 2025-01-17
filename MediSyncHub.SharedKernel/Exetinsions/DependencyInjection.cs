using MediSyncHub.SharedKernel.Data;
using MediSyncHub.SharedKernel.Events.EventBus;
using MediSyncHub.SharedKernel.Events;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;

namespace MediSyncHub.SharedKernel.Exetinsions;

public static class DependencyInjection
{
    public static IServiceCollection AddSharedInfrastructure(
            this IServiceCollection services,
            IConfiguration configuration)
    {
        services.AddSingleton<IEventBus, InMemoryEventBus>();
        services.AddSingleton<DbConnectionFactory>(
            new DbConnectionFactory(configuration.GetConnectionString("DefaultConnection")));

        return services;
    }
}
