#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["src/Services/Auth/src/Auth.API/Auth.API.csproj", "src/Services/Auth/src/Auth.API/"]
COPY ["src/Services/Auth/src/Auth/Auth.csproj", "src/Services/Auth/src/Auth/"]
COPY ["src/BuildingBlocks/BuildingBlocks/BuildingBlocks.csproj", "src/BuildingBlocks/BuildingBlocks/"]
RUN dotnet restore "src/Services/Auth/src/Auth.API/Auth.API.csproj"
COPY . .
WORKDIR "/src/src/Services/Auth/src/Auth.API"
RUN dotnet build "Auth.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Auth.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Auth.API.dll"]