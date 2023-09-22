FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Gateway/Aurora.Platform.Gateway/Aurora.Platform.Gateway.csproj", "Gateway/Aurora.Platform.Gateway/"]
COPY ["Common/Aurora.Framework.Logging/Aurora.Framework.Logging.csproj", "Common/Aurora.Framework.Logging/"]
RUN dotnet restore "Gateway/Aurora.Platform.Gateway/Aurora.Platform.Gateway.csproj"
COPY . .
WORKDIR "/src/Gateway/Aurora.Platform.Gateway"
RUN dotnet build "Aurora.Platform.Gateway.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Aurora.Platform.Gateway.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Aurora.Platform.Gateway.dll"]