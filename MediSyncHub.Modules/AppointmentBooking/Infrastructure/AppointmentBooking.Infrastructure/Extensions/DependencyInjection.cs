using AppointmentBooking.Domain.Repository;
using AppointmentBooking.Infrastructure.Data.Database;
using AppointmentBooking.Infrastructure.Data.Repository;
using MediSyncHub.SharedKernel.Shared;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AppointmentBooking.Infrastructure.Extensions;

public static class DependencyInjection
{
    public static IServiceCollection RegisterInfrastructure(this IServiceCollection services,
        IConfiguration configuration)
    {
        // Register DbContext
        var connectionString = configuration.GetConnectionString(Constants.DB_CONNECTION_STRING);
        services.AddDbContext<BookingDbContext>(options =>
            options.UseNpgsql(
                connectionString,
                b => b.MigrationsHistoryTable("__EFMigrationsHistory", "booking")));

        // Register repositories
        services.AddScoped<ISlotRepository, SlotRepository>();
        services.AddScoped<IAppointmentRepository, AppointmentRepository>();
        services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<BookingDbContext>());
        return services;
    }
}