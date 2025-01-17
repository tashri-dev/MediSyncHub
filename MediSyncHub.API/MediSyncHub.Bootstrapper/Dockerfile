# See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

# This stage is used when running from VS in fast mode (Default for Debug configuration)
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081


# This stage is used to build the service project
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["MediSyncHub.API/MediSyncHub.Bootstrapper/MediSyncHub.Bootstrapper.csproj", "MediSyncHub.API/MediSyncHub.Bootstrapper/"]
COPY ["MediSyncHub.Modules/DoctorAvailabilityModule/Business/MediSyncHub.Modules.DoctorAvailabilityModule.Business/MediSyncHub.Modules.DoctorAvailabilityModule.Business.csproj", "MediSyncHub.Modules/DoctorAvailabilityModule/Business/MediSyncHub.Modules.DoctorAvailabilityModule.Business/"]
COPY ["MediSyncHub.Modules/DoctorAvailabilityModule/Data/MediSyncHub.Modules.DoctorAvailabilityModule.Data/MediSyncHub.Modules.DoctorAvailabilityModule.Data.csproj", "MediSyncHub.Modules/DoctorAvailabilityModule/Data/MediSyncHub.Modules.DoctorAvailabilityModule.Data/"]
COPY ["MediSyncHub.SharedKernel/MediSyncHub.SharedKernel.csproj", "MediSyncHub.SharedKernel/"]
COPY ["MediSyncHub.Modules/DoctorAvailabilityModule/Presentation/MediSyncHub.Modules.DoctorAvailabilityModule.API/MediSyncHub.Modules.DoctorAvailabilityModule.API.csproj", "MediSyncHub.Modules/DoctorAvailabilityModule/Presentation/MediSyncHub.Modules.DoctorAvailabilityModule.API/"]
RUN dotnet restore "./MediSyncHub.API/MediSyncHub.Bootstrapper/MediSyncHub.Bootstrapper.csproj"
COPY . .
WORKDIR "/src/MediSyncHub.API/MediSyncHub.Bootstrapper"
RUN dotnet build "./MediSyncHub.Bootstrapper.csproj" -c $BUILD_CONFIGURATION -o /app/build

# This stage is used to publish the service project to be copied to the final stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./MediSyncHub.Bootstrapper.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# This stage is used in production or when running from VS in regular mode (Default when not using the Debug configuration)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MediSyncHub.Bootstrapper.dll"]