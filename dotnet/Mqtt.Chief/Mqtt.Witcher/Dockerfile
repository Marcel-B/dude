FROM mcr.microsoft.com/dotnet/runtime:7.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Mqtt.Witcher/Mqtt.Witcher.csproj", "Mqtt.Witcher/"]
RUN dotnet restore "Mqtt.Witcher/Mqtt.Witcher.csproj"
COPY . .
WORKDIR "/src/Mqtt.Witcher"
RUN dotnet build "Mqtt.Witcher.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Mqtt.Witcher.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Mqtt.Witcher.dll"]
