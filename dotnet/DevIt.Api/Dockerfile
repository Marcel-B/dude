FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["DevIt.Api/DevIt.Api.csproj", "DevIt.Api/"]
COPY ["DevIt.Application/DevIt.Application.csproj", "DevIt.Application/"]
COPY ["DevIt.Projekt.Adapter/DevIt.Projekt.Adapter.csproj", "DevIt.Projekt.Adapter/"]
COPY ["DevIt.Eintrag.Adapter/DevIt.Eintrag.Adapter.csproj", "DevIt.Eintrag.Adapter/"]
COPY ["DevIt.Repository/DevIt.Repository.csproj", "DevIt.Repository/"]
COPY ["DevIt.Domain/DevIt.Domain.csproj", "DevIt.Domain/"]
COPY ["DevIt.Persistence/DevIt.Persistence.csproj", "DevIt.Persistence/"]
COPY ["DevIt.Pbi.Adapter/DevIt.Pbi.Adapter.csproj", "DevIt.Pbi.Adapter/"]
RUN dotnet restore "DevIt.Api/DevIt.Api.csproj" -v diag
COPY . .
WORKDIR "/src/DevIt.Api"
RUN dotnet build "DevIt.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "DevIt.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DevIt.Api.dll"]
