FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["DevIt.ReverseProxy/DevIt.ReverseProxy.csproj", "DevIt.ReverseProxy/"]
RUN dotnet restore "DevIt.ReverseProxy/DevIt.ReverseProxy.csproj"
COPY . .
WORKDIR "/src/DevIt.ReverseProxy"
RUN dotnet build "DevIt.ReverseProxy.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "DevIt.ReverseProxy.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DevIt.ReverseProxy.dll"]
