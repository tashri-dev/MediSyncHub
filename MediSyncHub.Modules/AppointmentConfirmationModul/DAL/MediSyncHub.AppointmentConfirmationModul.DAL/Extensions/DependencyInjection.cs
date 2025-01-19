using MediSyncHub.AppointmentConfirmationModule.DAL.Database;
using MediSyncHub.AppointmentConfirmationModule.DAL.Repository;
using MediSyncHub.SharedKernel.Shared;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MediSyncHub.AppointmentConfirmationModule.DAL.Extensions;

public static class DependencyInjection
{
    public static IServiceCollection RegisterDal(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IAppointmentRepository, AppointmentRepoistory>();
        services.AddScoped<INotificationRepository, NotificationRepository>();
        //add dbContext
        // Register DbContext
        var connectionString = configuration.GetConnectionString(Constants.DB_CONNECTION_STRING);
        services.AddDbContext<ConfirmationDbContext>(options =>
            options.UseNpgsql(
                connectionString,
                b => b.MigrationsHistoryTable("__EFMigrationsHistory", "booking")));

        services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<ConfirmationDbContext>());
        return services;
    }
}