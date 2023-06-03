FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["src/Services/MealIngredients/src/MealIngredients.API/MealIngredients.API.csproj", "src/Services/MealIngredients/src/MealIngredients.API/"]
COPY ["src/BuildingBlocks/BuildingBlocks.csproj", "src/BuildingBlocks/"]
RUN dotnet restore "src/Services/MealIngredients/src/MealIngredients.API/MealIngredients.API.csproj"
COPY . .
WORKDIR "/src/src/Services/MealIngredients/src/MealIngredients.API"
RUN dotnet build "MealIngredients.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MealIngredients.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MealIngredients.API.dll"]
