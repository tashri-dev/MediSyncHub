using DoctorAvailability.Business.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


namespace DoctorAvailability.API.Extensions;

public static class DependencyInjection
{
    public static IServiceCollection AddAvailabilityModule(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddBusinessLayer(configuration);

        return services;
    }
}