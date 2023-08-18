FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Mqtt.Identity/Mqtt.Identity.csproj", "Mqtt.Identity/"]
RUN dotnet restore "Mqtt.Identity/Mqtt.Identity.csproj"
COPY . .
WORKDIR "/src/Mqtt.Identity"
RUN dotnet build "Mqtt.Identity.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Mqtt.Identity.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Mqtt.Identity.dll"]
