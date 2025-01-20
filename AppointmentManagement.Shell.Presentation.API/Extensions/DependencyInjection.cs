using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using AppointmentManagement.Core.Application.Extensions;
using AppointmentManagement.Shell.Infrastructure.Extensions;

namespace AppointmentManagement.Shell.Presentation.API.Extensions;

public static class DependencyInjection
{
    public static IServiceCollection AddManagementModule(this IServiceCollection services, IConfiguration configuration)
    {
        services.RegisterApplicationService();
        services.RegisterInfrastructure(configuration);
        return services;
    }
}