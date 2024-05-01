FROM node:latest
RUN npm install --global yarn
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

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
RUN dotnet restore "src/Acme.Greenhouse.Blazor/Acme.Greenhouse.Blazor.csproj"
COPY . .
WORKDIR "/src/src/Acme.Greenhouse.Blazor"
RUN dotnet tool install -g Volo.Abp.Cli
RUN yarn install

RUN dotnet build "Acme.Greenhouse.Blazor.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Acme.Greenhouse.Blazor.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Acme.Greenhouse.Blazor.dll"]
