FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Identity.Cat/Identity.Cat.csproj", "Identity.Cat/"]
RUN dotnet restore "Identity.Cat/Identity.Cat.csproj"
COPY . .
WORKDIR "/src/Identity.Cat"
RUN dotnet build "Identity.Cat.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Identity.Cat.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Identity.Cat.dll"]
