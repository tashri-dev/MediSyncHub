using AppointmentBooking.Application.Extensions;
using AppointmentBooking.Endpoints.Extenstion;
using AppointmentBooking.Infrastructure.Data.Database;
using AppointmentConfirmation.DAL.Database;
using AppointmentConfirmation.Services.Extensions;
using AppointmentManagement.Shell.Infrastructure.Adapters.Persistence;
using AppointmentManagement.Shell.Infrastructure.Extensions;
using AppointmentManagement.Shell.Presentation.API.Extensions;
using DoctorAvailability.API.Extensions;
using DoctorAvailability.Business.Data;
using DoctorAvailability.Business.Extensions;
using MediSyncHub.SharedKernel.Exetinsions;
using MediSyncHub.Bootstrapper.Middleware;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;
// Add services to the container
services.AddControllers();
services.AddOpenApiDocument(config =>
{
    config.Title = "MediSyncHub API";
    config.Version = "v1";
    config.Description = "MediSyncHub API";
});

// Add shared infrastructure
services.AddSharedInfrastructure(builder.Configuration);

// Add modules
services.AddAvailabilityModule(builder.Configuration);
services.AddAppointmentBookingModule(builder.Configuration);
services.AddAppointmentConfirmationModule(builder.Configuration);
services.AddManagementModule(builder.Configuration);
var app = builder.Build();
// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseOpenApi();
    app.UseSwaggerUi();
}

// Add middleware
app.UseMiddleware<RequestLoggingMiddleware>();
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

// Run database migrations
await MigrateDb();

//configure the subscribed event handlers
app.ConfigureAppointmentBookingEventBus();
app.ConfigureDoctorAvailabilityEventBus();
app.ConfigureAppointmentBookedEventBus();
app.ConfigureAppointmentManamgemntEventBus();
app.Run();


async Task MigrateDb()
{
    using var scope = app.Services.CreateScope();
    var scopeServiceProvider = scope.ServiceProvider;
    try
    {
        var availabilityContext = scopeServiceProvider.GetRequiredService<DoctorAvailabilityDbContext>();
        await availabilityContext.Database.MigrateAsync();

        var bookingContext = scopeServiceProvider.GetRequiredService<BookingDbContext>();
        await bookingContext.Database.MigrateAsync();

        var confirmationDbContext = scopeServiceProvider.GetRequiredService<ConfirmationDbContext>();
        await confirmationDbContext.Database.MigrateAsync();

        var managementDbContext = scopeServiceProvider.GetRequiredService<ManagementDbContext>();
        await managementDbContext.Database.MigrateAsync();
    }
    catch (Exception ex)
    {
        var logger = scopeServiceProvider.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred while migrating the database");
        throw;
    }
}