FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["DevIt.Api/DevIt.Api.csproj", "DevIt.Api/"]
COPY ["DevIt.Repository/DevIt.Repository.csproj", "DevIt.Repository/"]
COPY ["DevIt.Domain/DevIt.Domain.csproj", "DevIt.Domain/"]
COPY ["DevIt.Persistence/DevIt.Persistence.csproj", "DevIt.Persistence/"]
COPY ["DevIt.Application/DevIt.Application.csproj", "DevIt.Application/"]
COPY ["DevIt.Abrechnung.Adapter/DevIt.Abrechnung.Adapter.csproj", "DevIt.Abrechnung.Adapter/"]
COPY ["DevIt.Eintrag.Adapter/DevIt.Eintrag.Adapter.csproj", "DevIt.Eintrag.Adapter/"]
COPY ["DevIt.Pbi.Adapter/DevIt.Pbi.Adapter.csproj", "DevIt.Pbi.Adapter/"]
COPY ["DevIt.Projekt.Adapter/DevIt.Projekt.Adapter.csproj", "DevIt.Projekt.Adapter/"]
COPY ["DevIt.Moco.Service/DevIt.Moco.Service.csproj", "DevIt.Moco.Service/"]
COPY ["DevIt.Moco.Adapter/DevIt.Moco.Adapter.csproj", "DevIt.Moco.Adapter/"]
RUN dotnet restore "DevIt.Api/DevIt.Api.csproj"
COPY . .
WORKDIR "/src/DevIt.Api"
RUN dotnet build "DevIt.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "DevIt.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DevIt.Api.dll"]
