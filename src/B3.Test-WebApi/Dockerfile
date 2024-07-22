#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/B3.Test-WebApi/B3.Test-WebApi.csproj", "src/B3.Test-WebApi/"]
COPY ["src/B3.Test.Application/B3.Test.Application.csproj", "src/B3.Test.Application/"]
COPY ["src/B3.Test.Domain.Core/B3.Test.Domain.Core.csproj", "src/B3.Test.Domain.Core/"]
COPY ["src/B3.Test.Crosscuting.Ioc/B3.Test.Crosscuting.Ioc.csproj", "src/B3.Test.Crosscuting.Ioc/"]
COPY ["src/B3.Test.Domain/B3.Test.Domain.csproj", "src/B3.Test.Domain/"]
COPY ["src/B3.Test.Infra/B3.Test.Infra.csproj", "src/B3.Test.Infra/"]
RUN dotnet restore "./src/B3.Test-WebApi/./B3.Test-WebApi.csproj"
COPY . .
WORKDIR "/src/src/B3.Test-WebApi"
RUN dotnet build "./B3.Test-WebApi.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./B3.Test-WebApi.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "B3.Test-WebApi.dll"]