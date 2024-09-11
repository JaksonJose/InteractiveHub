#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
#EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Integrations/Whatsapp/Whatsapp.Api/Whatsapp.Api.csproj", "Integrations/Whatsapp/Whatsapp.Api/"]
COPY ["Shared/AdaptiveKitCore/AdaptiveKitCore.csproj", "Shared/AdaptiveKitCore/"]
COPY ["Integrations/Whatsapp/whatsapp.Core/whatsapp.Core.csproj", "Integrations/Whatsapp/whatsapp.Core/"]
COPY ["CrossCutting/CrossCutting.csproj", "CrossCutting/"]
COPY ["Integrations/Whatsapp/Whatsapp.Repository/Whatsapp.Repository.csproj", "Integrations/Whatsapp/Whatsapp.Repository/"]
RUN dotnet restore "./Integrations/Whatsapp/Whatsapp.Api/Whatsapp.Api.csproj"
COPY . .
WORKDIR "/src/Integrations/Whatsapp/Whatsapp.Api"
RUN dotnet build "./Whatsapp.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Whatsapp.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Whatsapp.Api.dll"]