FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["IdentityCat.Service/IdentityCat.Service.csproj", "IdentityCat.Service/"]
RUN dotnet restore "IdentityCat.Service/IdentityCat.Service.csproj"
COPY . .
WORKDIR "/src/IdentityCat.Service"
RUN dotnet build "IdentityCat.Service.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "IdentityCat.Service.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "IdentityCat.Service.dll"]
