using AppointmentBooking.Application.Extensions;
using AppointmentBooking.Infrastructure.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AppointmentBooking.Endpoints.Extenstion;

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