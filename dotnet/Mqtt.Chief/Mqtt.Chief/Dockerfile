FROM mcr.microsoft.com/dotnet/runtime:7.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Mqtt.Chief/Mqtt.Chief.csproj", "Mqtt.Chief/"]
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
