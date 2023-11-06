FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Services/Settings/Aurora.Platform.Settings.API/Aurora.Platform.Settings.API.csproj", "Services/Settings/Aurora.Platform.Settings.API/"]
COPY ["Common/Aurora.Framework.Api/Aurora.Framework.Api.csproj", "Common/Aurora.Framework.Api/"]
COPY ["Common/Aurora.Framework.Common/Aurora.Framework.Common.csproj", "Common/Aurora.Framework.Common/"]
COPY ["Common/Aurora.Framework.Validations/Aurora.Framework.Validations.csproj", "Common/Aurora.Framework.Validations/"]
COPY ["Common/Aurora.Framework.Logging/Aurora.Framework.Logging.csproj", "Common/Aurora.Framework.Logging/"]
COPY ["Common/Aurora.Framework.Repositories/Aurora.Framework.Repositories.csproj", "Common/Aurora.Framework.Repositories/"]
COPY ["Services/Settings/Aurora.Platform.Settings.Application/Aurora.Platform.Settings.Application.csproj", "Services/Settings/Aurora.Platform.Settings.Application/"]
COPY ["Services/Settings/Aurora.Platform.Settings.Domain/Aurora.Platform.Settings.Domain.csproj", "Services/Settings/Aurora.Platform.Settings.Domain/"]
COPY ["Services/Settings/Aurora.Platform.Settings.Infrastructure/Aurora.Platform.Settings.Infrastructure.csproj", "Services/Settings/Aurora.Platform.Settings.Infrastructure/"]
RUN dotnet restore "Services/Settings/Aurora.Platform.Settings.API/Aurora.Platform.Settings.API.csproj"
COPY . .
WORKDIR "/src/Services/Settings/Aurora.Platform.Settings.API"
RUN dotnet build "Aurora.Platform.Settings.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Aurora.Platform.Settings.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Aurora.Platform.Settings.API.dll"]