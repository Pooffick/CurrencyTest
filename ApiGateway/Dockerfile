FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY ApiGateway/ApiGateway/ApiGateway.csproj ApiGateway/ApiGateway/
RUN dotnet restore ApiGateway/ApiGateway/ApiGateway.csproj

COPY . .
RUN dotnet publish ApiGateway/ApiGateway/ApiGateway.csproj \
    -c Release \
    -o /app/publish \
    /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app

COPY --from=build /app/publish .

ENTRYPOINT ["dotnet", "ApiGateway.dll"]