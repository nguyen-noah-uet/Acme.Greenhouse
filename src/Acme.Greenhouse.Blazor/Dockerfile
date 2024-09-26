# See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

# This stage is used when running from VS in fast mode (Default for Debug configuration)
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081


# This stage is used to build the service project
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/Acme.Greenhouse.Blazor/Acme.Greenhouse.Blazor.csproj", "src/Acme.Greenhouse.Blazor/"]
COPY ["src/Acme.Greenhouse.Application/Acme.Greenhouse.Application.csproj", "src/Acme.Greenhouse.Application/"]
COPY ["src/Acme.Greenhouse.Domain/Acme.Greenhouse.Domain.csproj", "src/Acme.Greenhouse.Domain/"]
COPY ["src/Acme.Greenhouse.Domain.Shared/Acme.Greenhouse.Domain.Shared.csproj", "src/Acme.Greenhouse.Domain.Shared/"]
COPY ["src/Acme.Greenhouse.Application.Contracts/Acme.Greenhouse.Application.Contracts.csproj", "src/Acme.Greenhouse.Application.Contracts/"]
COPY ["src/Acme.Greenhouse.HttpApi/Acme.Greenhouse.HttpApi.csproj", "src/Acme.Greenhouse.HttpApi/"]
COPY ["src/Acme.Greenhouse.EntityFrameworkCore/Acme.Greenhouse.EntityFrameworkCore.csproj", "src/Acme.Greenhouse.EntityFrameworkCore/"]
RUN dotnet restore "./src/Acme.Greenhouse.Blazor/Acme.Greenhouse.Blazor.csproj"
COPY . .
WORKDIR "/src/src/Acme.Greenhouse.Blazor"
RUN dotnet build "./Acme.Greenhouse.Blazor.csproj" -c $BUILD_CONFIGURATION -o /app/build

# This stage is used to publish the service project to be copied to the final stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Acme.Greenhouse.Blazor.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# This stage is used in production or when running from VS in regular mode (Default when not using the Debug configuration)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Acme.Greenhouse.Blazor.dll"]