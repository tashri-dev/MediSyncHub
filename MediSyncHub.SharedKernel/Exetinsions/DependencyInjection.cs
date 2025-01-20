using MediSyncHub.SharedKernel.Data;
using MediSyncHub.SharedKernel.Events.EventBus;
using MediSyncHub.SharedKernel.Events;
using MediSyncHub.SharedKernel.Shared;
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
        var connectionString = configuration.GetConnectionString(Constants.DB_CONNECTION_STRING);
        services.AddSingleton<DbConnectionFactory>(
            new DbConnectionFactory(connectionString));

        return services;
    }
}