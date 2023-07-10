FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["src/BuildingBlocks/BuildingBlocks/BuildingBlocks.csproj", "src/BuildingBlocks/BuildingBlocks/"]
COPY ["src/Services/Posts/src/Posts.API/Posts.API.csproj", "src/Services/Posts/src/Posts.API/"]
COPY ["src/Services/Posts/src/Posts/Posts.csproj", "src/Services/Posts/src/Posts/"]
RUN dotnet restore "src/Services/Posts/src/Posts.API/Posts.API.csproj"
COPY . .
WORKDIR "/src/src/Services/Posts/src/Posts.API/"
RUN dotnet build "Posts.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Posts.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Posts.API.dll"]

EXPOSE 3004
ENV ASPNETCORE_URLS=http://*:3004