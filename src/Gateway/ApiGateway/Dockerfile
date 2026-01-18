# Stage 1: Build
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# Копируем нужный csproj файл и выполняем restore зависимостей
COPY ["src/Gateway/ApiGateway/ApiGateway.csproj", "Gateway/ApiGateway/"]
RUN dotnet restore "Gateway/ApiGateway/ApiGateway.csproj"

# Копируем всё содержимое приложения
COPY . .
WORKDIR "/src/src/Gateway/ApiGateway"

# Сборка и публикация приложения для Release
RUN dotnet build "ApiGateway.csproj" -c Release -o /app/build
RUN dotnet publish "ApiGateway.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Stage 2: Final
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS final
WORKDIR /app

# Копируем публикацию из build стейджа (fix здесь!)
COPY --from=build /app/publish .

# Для Production: возможно добавить окружение ocelot.Development.json в образ
# COPY src/Gateway/ApiGateway/ocelot.Development.json .

ENTRYPOINT ["dotnet", "ApiGateway.dll"]
