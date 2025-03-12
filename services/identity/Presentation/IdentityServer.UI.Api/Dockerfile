# See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

# This stage is used when running from VS in fast mode (Default for Debug configuration)
FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443


# This stage is used to build the service project
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

RUN dotnet nuget add source "https://pkgs.dev.azure.com/alperbasda/58875974-b7db-4df6-8307-e5b5f3585520/_packaging/nuray_mind/nuget/v3/index.json" --name "nuray_mind" --username "azdo" --password "758WDbcoVPZwBU9ae0qHpPdGnXfcAxp49aTxBz9z9fIHpYOgq1Q4JQQJ99BAACAAAAAAAAAAAAASAZDOvrox" --store-password-in-clear-text --valid-authentication-types "basic"
RUN dotnet nuget remove source "nuget.org"
RUN dotnet nuget add source "https://api.nuget.org/v3/index.json" --name "nuget.org"


ARG ConnectionString

ENV DatabaseOptions__EfConnectionString=$ConnectionString


COPY ["services/identity/Presentation/IdentityServer.UI.Api/IdentityServer.UI.Api.csproj", "services/identity/Presentation/IdentityServer.UI.Api/"]
COPY ["services/identity/Common/Notification/Notification.csproj", "services/identity/Common/Notification/"]
COPY ["services/identity/CQRS/IdentityServer.Persistence/IdentityServer.Persistence.csproj", "services/identity/CQRS/IdentityServer.Persistence/"]
COPY ["services/identity/CQRS/IdentityServer.Application/IdentityServer.Application.csproj", "services/identity/CQRS/IdentityServer.Application/"]
COPY ["services/identity/CQRS/IdentityServer.Domain/IdentityServer.Domain.csproj", "services/identity/CQRS/IdentityServer.Domain/"]
COPY ["services/identity/Common/IdentityServer.Common/IdentityServer.Common.csproj", "services/identity/Common/IdentityServer.Common/"]
RUN dotnet restore "./services/identity/Presentation/IdentityServer.UI.Api/IdentityServer.UI.Api.csproj"
COPY . .
WORKDIR "/src/services/identity/Presentation/IdentityServer.UI.Api"
RUN dotnet build "./IdentityServer.UI.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

# This stage is used to publish the service project to be copied to the final stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./IdentityServer.UI.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# This stage is used in production or when running from VS in regular mode (Default when not using the Debug configuration)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "IdentityServer.UI.Api.dll"]