FROM mcr.microsoft.com/dotnet/aspnet:9.0-alpine AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:9.0-alpine AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Directory.Packages.props", "."]
COPY ["src/HahnSoftware.API/HahnSoftware.API.csproj", "src/HahnSoftware.API/"]
COPY ["src/HahnSoftware.Infrastructure/HahnSoftware.Infrastructure.csproj", "src/HahnSoftware.Infrastructure/"]
COPY ["src/HahnSoftware.Application/HahnSoftware.Application.csproj", "src/HahnSoftware.Application/"]
COPY ["src/HahnSoftware.Domain/HahnSoftware.Domain.csproj", "src/HahnSoftware.Domain/"]
RUN dotnet restore "./src/HahnSoftware.API/HahnSoftware.API.csproj"
COPY . .
WORKDIR "/src/src/HahnSoftware.API"
RUN dotnet build "./HahnSoftware.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./HahnSoftware.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "HahnSoftware.API.dll"]