FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY UserService/UserService.Domain/UserService.Domain.csproj UserService/UserService.Domain/
COPY UserService/UserService.Application/UserService.Application.csproj UserService/UserService.Application/
COPY UserService/UserService.Infrastructure/UserService.Infrastructure.csproj UserService/UserService.Infrastructure/
COPY UserService/UserService.Api/UserService.Api.csproj UserService/UserService.Api/

RUN dotnet restore UserService/UserService.Api/UserService.Api.csproj

COPY . .

RUN dotnet publish UserService/UserService.Api/UserService.Api.csproj \
    -c Release \
    -o /app/publish \
    /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app

COPY --from=build /app/publish .

ENTRYPOINT ["dotnet", "UserService.Api.dll"]