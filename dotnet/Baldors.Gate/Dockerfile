FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Baldors.Gate.csproj", "Baldors.Gate/"]
RUN dotnet restore "Baldors.Gate/Baldors.Gate.csproj"
COPY . .
WORKDIR "/src/Baldors.Gate"
RUN dotnet build "Baldors.Gate.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Baldors.Gate.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Baldors.Gate.dll"]
