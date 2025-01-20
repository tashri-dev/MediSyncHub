using Microsoft.Extensions.DependencyInjection;

namespace AppointmentManagement.Core.Application.Extensions;

public static class DependencyInjection
{
    public static IServiceCollection RegisterApplicationService(this IServiceCollection services)
    {
        // Register MediatR handlers
        services.AddMediatR(cfg =>
            cfg.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly));
        return services;
    }
}