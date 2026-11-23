FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src
COPY OficinaMecanica.slnx ./
COPY src/OficinaMecanica.Domain/*.csproj src/OficinaMecanica.Domain/
COPY src/OficinaMecanica.Application/*.csproj src/OficinaMecanica.Application/
COPY src/OficinaMecanica.Infrastructure/*.csproj src/OficinaMecanica.Infrastructure/
COPY src/OficinaMecanica.Api/*.csproj src/OficinaMecanica.Api/
RUN dotnet restore src/OficinaMecanica.Api/OficinaMecanica.Api.csproj
COPY src/ ./src/
RUN dotnet publish src/OficinaMecanica.Api/OficinaMecanica.Api.csproj -c Release -o /app/publish --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:10.0
WORKDIR /app
COPY --from=build /app/publish .
USER $APP_UID
EXPOSE 8080
ENTRYPOINT ["dotnet", "OficinaMecanica.Api.dll"]
