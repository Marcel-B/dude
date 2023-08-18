FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Mqtt.Broker/Mqtt.Broker.csproj", "Mqtt.Broker/"]
COPY ["Mqtt.Persistence/Mqtt.Persistence.csproj", "Mqtt.Persistence/"]
COPY ["Mqtt.Domain/Mqtt.Domain.csproj", "Mqtt.Domain/"]
COPY ["Mqtt.Repository/Mqtt.Repository.csproj", "Mqtt.Repository/"]
COPY ["Mqtt.Measurement.Adapter/Mqtt.Measurement.Adapter.csproj", "Mqtt.Measurement.Adapter/"]
RUN dotnet restore "Mqtt.Broker/Mqtt.Broker.csproj"
COPY . .
WORKDIR "/src/Mqtt.Broker"
RUN dotnet build "Mqtt.Broker.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Mqtt.Broker.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Mqtt.Broker.dll"]
