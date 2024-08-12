#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Challenge.Api/Challenge.Api.csproj", "Challenge.Api/"]
COPY ["Challenge.Application/Challenge.Application.csproj", "Challenge.Application/"]
COPY ["Challenge.Domain/Challenge.Domain.csproj", "Challenge.Domain/"]
COPY ["Challenge.Infrastructure/Challenge.Infrastructure.csproj", "Challenge.Infrastructure/"]
COPY ["Challenge.EfPostgreSqlStorge/Challenge.EfPostgreSqlStorge.csproj", "Challenge.EfPostgreSqlStorge/"]
COPY ["Challenge.EfStorage/Challenge.EfStorage.csproj", "Challenge.EfStorage/"]
RUN dotnet restore "./Challenge.Api/Challenge.Api.csproj"
COPY . .
WORKDIR "/src/Challenge.Api"
RUN dotnet build "./Challenge.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Challenge.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Challenge.Api.dll"]