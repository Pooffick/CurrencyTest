FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY CurrencyUpdater/CurrencyUpdater/CurrencyUpdater.csproj CurrencyUpdater/CurrencyUpdater/

RUN dotnet restore CurrencyUpdater/CurrencyUpdater/CurrencyUpdater.csproj

COPY . .

RUN dotnet publish CurrencyUpdater/CurrencyUpdater/CurrencyUpdater.csproj \
    -c Release \
    -o /app/publish \
    /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/runtime:8.0 AS runtime
WORKDIR /app

COPY --from=build /app/publish .

ENTRYPOINT ["dotnet", "CurrencyUpdater.dll"]