#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["src/Services/Comments/src/Comments.API/Comments.API.csproj", "src/Services/Comments/src/Comments.API/"]
COPY ["src/Services/Comments/src/Comments/Comments.csproj", "src/Services/Comments/src/Comments/"]
COPY ["src/BuildingBlocks/BuildingBlocks/BuildingBlocks.csproj", "src/BuildingBlocks/BuildingBlocks/"]
RUN dotnet restore "src/Services/Comments/src/Comments.API/Comments.API.csproj"
COPY . .
WORKDIR "/src/src/Services/Comments/src/Comments.API"
RUN dotnet build "Comments.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Comments.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Comments.API.dll"]

EXPOSE 3006
ENV ASPNETCORE_URLS=http://*:3006