using MediSyncHub.AppointmentConfirmationModule.DAL.Database;
using MediSyncHub.Modules.AppointmentBookingModule.Application.Extensions;
using MediSyncHub.Modules.AppointmentBookingModule.Endpoints.Extenstion;
using MediSyncHub.Modules.AppointmentBookingModule.Infrastructure.Data.Database;
using MediSyncHub.Modules.DoctorAvailabilityModule.API.Extensions;
using MediSyncHub.Modules.DoctorAvailabilityModule.Business.Data;
using MediSyncHub.Modules.DoctorAvailabilityModule.Business.Extensions;
using MediSyncHub.SharedKernel.Exetinsions;
using Microsoft.EntityFrameworkCore;
using MediSyncHub.AppointmentConfirmationModule.Services.Extensions;


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

var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseOpenApi();
    app.UseSwaggerUi();
}

// Then the rest of your pipeline
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

// Run database migrations
using (var scope = app.Services.CreateScope())
{
    var scopeServiceProvider = scope.ServiceProvider;
    try
    {
        var availabilityContext = scopeServiceProvider.GetRequiredService<DoctorAvailabilityDbContext>();
        await availabilityContext.Database.MigrateAsync();

        var bookingContext = scopeServiceProvider.GetRequiredService<BookingDbContext>();
        await bookingContext.Database.MigrateAsync();

        var confirmationDbContext = scopeServiceProvider.GetRequiredService<ConfirmationDbContext>();
        await confirmationDbContext.Database.MigrateAsync();
    }
    catch (Exception ex)
    {
        var logger = scopeServiceProvider.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred while migrating the database");
        throw;
    }
}
//configure the subscribed event handlers

app.ConfigureAppointmentBookingEventBus();
app.ConfigureDoctorAvailabilityEventBus();
app.ConfigureAppointmentBookedEventBus();
app.Run();