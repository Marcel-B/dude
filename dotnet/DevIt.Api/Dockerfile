FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["DevIt.Api/DevIt.Api.csproj", "DevIt.Api/"]
RUN dotnet restore "DevIt.Api/DevIt.Api.csproj"
COPY . .
WORKDIR "/src/DevIt.Api"
RUN dotnet build "DevIt.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "DevIt.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DevIt.Api.dll"]
