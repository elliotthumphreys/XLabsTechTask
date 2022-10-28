#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/runtime:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Tests/IntegrationTests/IntegrationTests.csproj", "Tests/IntegrationTests/"]
COPY ["DataAccess/DataAccess.csproj", "DataAccess/"]
COPY ["Domain/Domain.csproj", "Domain/"]
COPY ["Dtos/Dtos.csproj", "Dtos/"]
RUN dotnet restore "Tests/IntegrationTests/IntegrationTests.csproj"
COPY . .
WORKDIR "/src/Tests/IntegrationTests"
RUN dotnet build "IntegrationTests.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "IntegrationTests.csproj" -c Release -o /app/publish

FROM build AS final
WORKDIR /app
COPY ["wait-for-it.sh", "wait-for-it.sh"]
RUN chmod +x wait-for-it.sh
COPY --from=publish /app/publish .