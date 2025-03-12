#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Templates/Presentation/IdentityServer.UI.Web/IdentityServer.UI.Web.csproj", "Templates/Presentation/IdentityServer.UI.Web/"]
RUN dotnet restore "Templates/Presentation/IdentityServer.UI.Web/IdentityServer.UI.Web.csproj"
COPY . .
WORKDIR "/src/Templates/Presentation/IdentityServer.UI.Web"
RUN dotnet build "IdentityServer.UI.Web.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "IdentityServer.UI.Web.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "IdentityServer.UI.Web.dll"]