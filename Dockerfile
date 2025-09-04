FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# Copy csproj and restore
COPY ["iot-home-automation-backend/iot-home-automation-backend.csproj", "iot-home-automation-backend/"]
RUN dotnet restore "iot-home-automation-backend/iot-home-automation-backend.csproj"

# Copy all source files
COPY . .
WORKDIR "/src/iot-home-automation-backend"
RUN dotnet build "iot-home-automation-backend.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
RUN dotnet publish "iot-home-automation-backend.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "iot-home-automation-backend.dll"]
