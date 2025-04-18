﻿FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
RUN apt-get update && apt-get install -y curl --no-install-recommends && rm -rf /var/lib/apt/lists/*
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/BookIt.Api/BookIt.Api.csproj", "src/BookIt.Api/"]
COPY ["src/BookIt.Application/BookIt.Application.csproj", "src/BookIt.Application/"]
COPY ["src/BookIt.Domain/BookIt.Domain.csproj", "src/BookIt.Domain/"]
COPY ["src/BookIt.Infrastructure/BookIt.Infrastructure.csproj", "src/BookIt.Infrastructure/"]
RUN dotnet restore "src/BookIt.Api/BookIt.Api.csproj"
COPY . .
WORKDIR "/src/src/BookIt.Api"
RUN dotnet build "BookIt.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "BookIt.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BookIt.Api.dll"]
