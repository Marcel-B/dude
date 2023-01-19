FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Identity.Servus/Identity.Servus.csproj", "Identity.Servus/"]
COPY ["Identity.Servus.Messaging.Persistence/Identity.Servus.Messaging.Persistence.csproj", "Identity.Servus.Messaging.Persistence/"]
COPY ["Identity.Servus.Messaging/Identity.Servus.Messaging.csproj", "Identity.Servus.Messaging/"]
COPY ["Identity.Servus.Application/Identity.Servus.Application.csproj", "Identity.Servus.Application/"]
COPY ["Identity.Servus.Domain/Identity.Servus.Domain.csproj", "Identity.Servus.Domain/"]
COPY ["Identity.Servus.Repository/Identity.Servus.Repository.csproj", "Identity.Servus.Repository/"]
COPY ["Identity.Servus.Authentication/Identity.Servus.Authentication.csproj", "Identity.Servus.Authentication/"]
COPY ["Identity.Servus.Persistence/Identity.Servus.Persistence.csproj", "Identity.Servus.Persistence/"]
COPY ["Identity.Servus.Authorization/Identity.Servus.Authorization.csproj", "Identity.Servus.Authorization/"]
COPY ["Identity.Servus.AppUser.Adapter/Identity.Servus.AppUser.Adapter.csproj", "Identity.Servus.AppUser.Adapter/"]
RUN dotnet restore "Identity.Servus/Identity.Servus.csproj"
COPY . .
WORKDIR "/src/Identity.Servus"
RUN dotnet build "Identity.Servus.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Identity.Servus.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Identity.Servus.dll"]
