FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Dude.Bff/Dude.Bff.csproj", "Dude.Bff/"]
COPY ["Dude.Shared/Dude.Shared.csproj", "Dude.Shared/"]
RUN dotnet restore "Dude.Bff/Dude.Bff.csproj"
COPY . .
WORKDIR "/src/Dude.Bff"
RUN dotnet build "Dude.Bff.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Dude.Bff.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Dude.Bff.dll"]
