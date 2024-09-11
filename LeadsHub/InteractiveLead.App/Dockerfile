#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["LeadsHub/InteractiveLead.App/InteractiveLead.App.csproj", "LeadsHub/InteractiveLead.App/"]
COPY ["CrossCutting/CrossCutting.csproj", "CrossCutting/"]
COPY ["Shared/AdaptiveKitCore/AdaptiveKitCore.csproj", "Shared/AdaptiveKitCore/"]
COPY ["LeadsHub/InteractiveLead.Core/InteractiveLead.Core.csproj", "LeadsHub/InteractiveLead.Core/"]
COPY ["LeadsHub/InteractiveLead.Data/InteractiveLead.Data.csproj", "LeadsHub/InteractiveLead.Data/"]
RUN dotnet restore "./LeadsHub/InteractiveLead.App/InteractiveLead.App.csproj"
COPY . .
WORKDIR "/src/LeadsHub/InteractiveLead.App"
RUN dotnet build "./InteractiveLead.App.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./InteractiveLead.App.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "InteractiveLead.App.dll"]