FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["DevIt.Service/DevIt.Service.csproj", "DevIt.Service/"]
COPY ["DevIt.Application/DevIt.Application.csproj", "DevIt.Application/"]
COPY ["DevIt.Abrechnung.Adapter/DevIt.Abrechnung.Adapter.csproj", "DevIt.Abrechnung.Adapter/"]
COPY ["DevIt.Repository/DevIt.Repository.csproj", "DevIt.Repository/"]
COPY ["DevIt.Domain/DevIt.Domain.csproj", "DevIt.Domain/"]
COPY ["DevIt.Eintrag.Adapter/DevIt.Eintrag.Adapter.csproj", "DevIt.Eintrag.Adapter/"]
COPY ["DevIt.Moco.Adapter/DevIt.Moco.Adapter.csproj", "DevIt.Moco.Adapter/"]
COPY ["DevIt.Moco.Service/DevIt.Moco.Service.csproj", "DevIt.Moco.Service/"]
COPY ["DevIt.Pbi.Adapter/DevIt.Pbi.Adapter.csproj", "DevIt.Pbi.Adapter/"]
COPY ["DevIt.Persistence/DevIt.Persistence.csproj", "DevIt.Persistence/"]
COPY ["DevIt.Projekt.Adapter/DevIt.Projekt.Adapter.csproj", "DevIt.Projekt.Adapter/"]
COPY ["Dude.Shared/Dude.Shared.csproj", "Dude.Shared/"]
RUN dotnet restore "DevIt.Service/DevIt.Service.csproj"
COPY . .
WORKDIR "/src/DevIt.Service"
RUN dotnet build "DevIt.Service.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "DevIt.Service.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DevIt.Service.dll"]
