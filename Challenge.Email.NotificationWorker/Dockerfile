#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/runtime:8.0 AS base
USER app
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Challenge.Email.NotificationWorker/Challenge.Email.NotificationWorker.csproj", "Challenge.Email.NotificationWorker/"]
COPY ["Challenge.Infrastructure/Challenge.Infrastructure.csproj", "Challenge.Infrastructure/"]
COPY ["Challenge.Domain/Challenge.Domain.csproj", "Challenge.Domain/"]
COPY ["Challenge.EfPostgreSqlStorge/Challenge.EfPostgreSqlStorge.csproj", "Challenge.EfPostgreSqlStorge/"]
COPY ["Challenge.EfStorage/Challenge.EfStorage.csproj", "Challenge.EfStorage/"]
RUN dotnet restore "./Challenge.Email.NotificationWorker/Challenge.Email.NotificationWorker.csproj"
COPY . .
WORKDIR "/src/Challenge.Email.NotificationWorker"
RUN dotnet build "./Challenge.Email.NotificationWorker.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Challenge.Email.NotificationWorker.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Challenge.Email.NotificationWorker.dll"]