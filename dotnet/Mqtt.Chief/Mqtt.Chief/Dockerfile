FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Mqtt.Chief/Mqtt.Chief.csproj", "Mqtt.Chief/"]
COPY ["Mqtt.Measurement.Adapter/Mqtt.Measurement.Adapter.csproj", "Mqtt.Measurement.Adapter/"]
COPY ["Mqtt.Domain/Mqtt.Domain.csproj", "Mqtt.Domain/"]
COPY ["Mqtt.Persistence/Mqtt.Persistence.csproj", "Mqtt.Persistence/"]
COPY ["Mqtt.Repository/Mqtt.Repository.csproj", "Mqtt.Repository/"]
RUN dotnet restore "Mqtt.Chief/Mqtt.Chief.csproj"
COPY . .
WORKDIR "/src/Mqtt.Chief"
RUN dotnet build "Mqtt.Chief.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Mqtt.Chief.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Mqtt.Chief.dll"]
