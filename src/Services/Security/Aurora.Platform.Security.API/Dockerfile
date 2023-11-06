FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Services/Security/Aurora.Platform.Security.API/Aurora.Platform.Security.API.csproj", "Services/Security/Aurora.Platform.Security.API/"]
COPY ["Common/Aurora.Framework.Api/Aurora.Framework.Api.csproj", "Common/Aurora.Framework.Api/"]
COPY ["Common/Aurora.Framework.Common/Aurora.Framework.Common.csproj", "Common/Aurora.Framework.Common/"]
COPY ["Common/Aurora.Framework.Validations/Aurora.Framework.Validations.csproj", "Common/Aurora.Framework.Validations/"]
COPY ["Common/Aurora.Framework.Logging/Aurora.Framework.Logging.csproj", "Common/Aurora.Framework.Logging/"]
COPY ["Common/Aurora.Framework.Cryptography/Aurora.Framework.Cryptography.csproj", "Common/Aurora.Framework.Cryptography/"]
COPY ["Common/Aurora.Framework.Repositories/Aurora.Framework.Repositories.csproj", "Common/Aurora.Framework.Repositories/"]
COPY ["Services/Security/Aurora.Platform.Security.Application/Aurora.Platform.Security.Application.csproj", "Services/Security/Aurora.Platform.Security.Application/"]
COPY ["Services/Security/Aurora.Platform.Security.Domain/Aurora.Platform.Security.Domain.csproj", "Services/Security/Aurora.Platform.Security.Domain/"]
COPY ["Services/Security/Aurora.Platform.Security.Infrastructure/Aurora.Platform.Security.Infrastructure.csproj", "Services/Security/Aurora.Platform.Security.Infrastructure/"]
RUN dotnet restore "Services/Security/Aurora.Platform.Security.API/Aurora.Platform.Security.API.csproj"
COPY . .
WORKDIR "/src/Services/Security/Aurora.Platform.Security.API"
RUN dotnet build "Aurora.Platform.Security.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Aurora.Platform.Security.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Aurora.Platform.Security.API.dll"]