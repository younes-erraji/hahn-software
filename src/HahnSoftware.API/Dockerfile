FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app

RUN apt-get update && apt-get install -y libgssapi-krb5-2 && rm -rf /var/lib/apt/lists/*

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

COPY ["Directory.Packages.props", "."]
COPY ["src/HahnSoftware.API/HahnSoftware.API.csproj", "src/HahnSoftware.API/"]
COPY ["src/HahnSoftware.Infrastructure/HahnSoftware.Infrastructure.csproj", "src/HahnSoftware.Infrastructure/"]
COPY ["src/HahnSoftware.Application/HahnSoftware.Application.csproj", "src/HahnSoftware.Application/"]
COPY ["src/HahnSoftware.Domain/HahnSoftware.Domain.csproj", "src/HahnSoftware.Domain/"]

RUN dotnet restore src/HahnSoftware.API/HahnSoftware.API.csproj
COPY . .
WORKDIR /src/src/HahnSoftware.API
RUN dotnet publish HahnSoftware.API.csproj -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
EXPOSE 80
ENTRYPOINT ["dotnet", "HahnSoftware.API.dll"]
