#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER root
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["./services/NSE.Catalogo.API/NSE.Catalogo.API.csproj", "services/NSE.Catalogo.API/"]
COPY ["./building blocks/NSE.Core/NSE.Core.csproj", "building blocks/NSE.Core/NSE.Core/"]
COPY ["./building blocks/NSE.WebAPI.Core/NSE.WebAPI.Core.csproj", "building blocks/NSE.WebAPI.Core/NSE.WebAPI.Core/"]
COPY ["./building blocks/NSE.MessageBus/NSE.MessageBus.csproj", "building blocks/NSE.MessageBus/NSE.MessageBus/"]

RUN dotnet restore "services/NSE.Catalogo.API/NSE.Catalogo.API.csproj"
COPY . .
WORKDIR "services/NSE.Catalogo.API"
RUN dotnet build "NSE.Catalogo.API.csproj" -c Release -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "NSE.Catalogo.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "NSE.Catalogo.API.dll"]