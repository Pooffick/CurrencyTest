FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY CurrencyTest.Database/CurrencyTest.Database.csproj CurrencyTest.Database/
COPY DatabaseMigrator/DatabaseMigrator/DatabaseMigrator.csproj DatabaseMigrator/DatabaseMigrator/

RUN dotnet restore DatabaseMigrator/DatabaseMigrator/DatabaseMigrator.csproj

COPY . .

RUN dotnet publish DatabaseMigrator/DatabaseMigrator/DatabaseMigrator.csproj \
    -c Release \
    -o /app/publish \
    /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/runtime:8.0 AS runtime
WORKDIR /app

COPY --from=build /app/publish .

ENTRYPOINT ["dotnet", "DatabaseMigrator.dll"]