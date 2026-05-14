FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY FinanceService/FinanceService.Domain/FinanceService.Domain.csproj FinanceService/FinanceService.Domain/
COPY FinanceService/FinanceService.Application/FinanceService.Application.csproj FinanceService/FinanceService.Application/
COPY FinanceService/FinanceService.Infrastructure/FinanceService.Infrastructure.csproj FinanceService/FinanceService.Infrastructure/
COPY FinanceService/FinanceService.Api/FinanceService.Api.csproj FinanceService/FinanceService.Api/

RUN dotnet restore FinanceService/FinanceService.Api/FinanceService.Api.csproj

COPY . .

RUN dotnet publish FinanceService/FinanceService.Api/FinanceService.Api.csproj \
    -c Release \
    -o /app/publish \
    /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app

COPY --from=build /app/publish .

ENTRYPOINT ["dotnet", "FinanceService.Api.dll"]