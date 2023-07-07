#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Services/Applications/Aurora.Platform.Applications.API/Aurora.Platform.Applications.API.csproj", "Services/Applications/Aurora.Platform.Applications.API/"]
COPY ["Common/Aurora.Framework.Common/Aurora.Framework.Common.csproj", "Common/Aurora.Framework.Common/"]
COPY ["Common/Aurora.Framework.Entities/Aurora.Framework.Entities.csproj", "Common/Aurora.Framework.Entities/"]
COPY ["Common/Aurora.Framework.Logging/Aurora.Framework.Logging.csproj", "Common/Aurora.Framework.Logging/"]
COPY ["Common/Aurora.Framework.Repositories/Aurora.Framework.Repositories.csproj", "Common/Aurora.Framework.Repositories/"]
RUN dotnet restore "Services/Applications/Aurora.Platform.Applications.API/Aurora.Platform.Applications.API.csproj"
COPY . .
WORKDIR "/src/Services/Applications/Aurora.Platform.Applications.API"
RUN dotnet build "Aurora.Platform.Applications.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Aurora.Platform.Applications.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Aurora.Platform.Applications.API.dll"]