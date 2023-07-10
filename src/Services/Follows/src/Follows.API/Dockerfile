#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["src/Services/Follows/src/Follows.API/Follows.API.csproj", "src/Services/Follows/src/Follows.API/"]
COPY ["src/Services/Follows/src/Follows/Follows.csproj", "src/Services/Follows/src/Follows/"]
COPY ["src/BuildingBlocks/BuildingBlocks/BuildingBlocks.csproj", "src/BuildingBlocks/BuildingBlocks/"]
RUN dotnet restore "src/Services/Follows/src/Follows.API/Follows.API.csproj"
COPY . .
WORKDIR "/src/src/Services/Follows/src/Follows.API"
RUN dotnet build "Follows.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Follows.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Follows.API.dll"]

EXPOSE 3002
ENV ASPNETCORE_URLS=http://*:3002