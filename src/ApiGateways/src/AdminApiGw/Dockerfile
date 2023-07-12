#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["src/ApiGateways/src/AdminApiGw/AdminApiGw.csproj", "src/ApiGateways/src/AdminApiGw/"]
COPY ["src/BuildingBlocks/BuildingBlocks/BuildingBlocks.csproj", "src/BuildingBlocks/BuildingBlocks/"]
RUN dotnet restore "src/ApiGateways/src/AdminApiGw/AdminApiGw.csproj"
COPY . .
WORKDIR "/src/src/ApiGateways/src/AdminApiGw"
RUN dotnet build "AdminApiGw.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "AdminApiGw.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AdminApiGw.dll"]

EXPOSE 4001
ENV ASPNETCORE_URLS=http://*:4001