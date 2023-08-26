FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Mqtt.Service/Mqtt.Service.csproj", "Mqtt.Service/"]
COPY ["Mqtt.Measurement.Adapter/Mqtt.Measurement.Adapter.csproj", "Mqtt.Measurement.Adapter/"]
COPY ["Dude.Shared/Dude.Shared.csproj", "Dude.Shared/"]
COPY ["Mqtt.Domain/Mqtt.Domain.csproj", "Mqtt.Domain/"]
COPY ["Mqtt.Measurement.Service/Mqtt.Measurement.Service.csproj", "Mqtt.Measurement.Service/"]
COPY ["Mqtt.Repository/Mqtt.Repository.csproj", "Mqtt.Repository/"]
COPY ["Mqtt.Persistence/Mqtt.Persistence.csproj", "Mqtt.Persistence/"]
COPY ["Mqtt.Shared/Mqtt.Shared.csproj", "Mqtt.Shared/"]
RUN dotnet restore "Mqtt.Service/Mqtt.Service.csproj"
COPY . .
WORKDIR "/src/Mqtt.Service"
RUN dotnet build "Mqtt.Service.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Mqtt.Service.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Mqtt.Service.dll"]
