#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["src/CDSHooks/CDSHooks.csproj", "src/CDSHooks/"]
COPY ["src/CDSHooks.Migrations/CDSHooks.Migrations.csproj", "src/CDSHooks.Migrations/"]
COPY ["src/CDSHooks.Data/CDSHooks.Data.csproj", "src/CDSHooks.Data/"]
COPY ["src/CDSHooks.Domain/CDSHooks.Domain.csproj", "src/CDSHooks.Domain/"]
COPY ["src/CDSHooks.Core/CDSHooks.Core.csproj", "src/CDSHooks.Core/"]
RUN dotnet restore "src/CDSHooks/CDSHooks.csproj"
COPY . .
WORKDIR "/src/src/CDSHooks"
RUN dotnet build "CDSHooks.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CDSHooks.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CDSHooks.dll"]