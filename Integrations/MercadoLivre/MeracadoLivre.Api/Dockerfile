#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Integrations/MercadoLivre/MeracadoLivre.Api/MeracadoLivre.Api.csproj", "Integrations/MercadoLivre/MeracadoLivre.Api/"]
COPY ["Integrations/MercadoLivre/MercadoLivre.Core/MercadoLivre.Core.csproj", "Integrations/MercadoLivre/MercadoLivre.Core/"]
COPY ["CrossCutting/CrossCutting.csproj", "CrossCutting/"]
COPY ["Integrations/MercadoLivre/MercadoLivre.Data/MercadoLivre.Data.csproj", "Integrations/MercadoLivre/MercadoLivre.Data/"]
RUN dotnet restore "./Integrations/MercadoLivre/MeracadoLivre.Api/MeracadoLivre.Api.csproj"
COPY . .
WORKDIR "/src/Integrations/MercadoLivre/MeracadoLivre.Api"
RUN dotnet build "./MeracadoLivre.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./MeracadoLivre.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MeracadoLivre.Api.dll"]