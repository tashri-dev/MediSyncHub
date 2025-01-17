using MediSyncHub.Modules.AppointmentBookingModule.Application.Extensions;
using MediSyncHub.Modules.AppointmentBookingModule.Endpoints.Extenstion;
using MediSyncHub.Modules.AppointmentBookingModule.Infrastructure.Data.Database;
using MediSyncHub.Modules.DoctorAvailabilityModule.API.Extensions;
using MediSyncHub.Modules.DoctorAvailabilityModule.Business.Data;
using MediSyncHub.Modules.DoctorAvailabilityModule.Business.Extensions;
using MediSyncHub.SharedKernel.Exetinsions;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllers();
builder.Services.AddOpenApiDocument(config =>
{
    config.Title = "MediSyncHub API";
    config.Version = "v1";
    config.Description = "MediSyncHub API";
});

// Add shared infrastructure
builder.Services.AddSharedInfrastructure(builder.Configuration);

// Add modules
builder.Services.AddAvailabilityModule(builder.Configuration);
builder.Services.AddAppointmentBookingModule(builder.Configuration);

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
    var services = scope.ServiceProvider;
    try
    {
        var availabilityContext = services.GetRequiredService<DoctorAvailabilityDbContext>();
        await availabilityContext.Database.MigrateAsync();

        var bookingContext = services.GetRequiredService<BookingDbContext>();
        await bookingContext.Database.MigrateAsync();
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred while migrating the database");
        throw;
    }
}
//configure the subscribed event handlers

app.ConfigureAppointmentBookingEventBus();
app.ConfigureDoctorAvailabilityEventBus();

app.Run();