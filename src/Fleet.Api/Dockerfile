# Step 1: Build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY src/Fleet.Api/Fleet.Api.csproj src/Fleet.Api/
COPY Fleet.Application/Fleet.Application.csproj Fleet.Application/
COPY Fleet.Domain/Fleet.Domain.csproj Fleet.Domain/
COPY Fleet.Infra.Database/Fleet.Infra.Database.csproj Fleet.Infra.Database/
COPY Fleet.Infra.FileStorage/Fleet.Infra.FileStorage.csproj Fleet.Infra.FileStorage/
COPY Fleet.Infra.Messaging/Fleet.Infra.Messaging.csproj Fleet.Infra.Messaging/

RUN dotnet restore src/Fleet.Api/Fleet.Api.csproj

COPY src/Fleet.Api/ src/Fleet.Api/
COPY Fleet.Application/ Fleet.Application/
COPY Fleet.Domain/ Fleet.Domain/
COPY Fleet.Infra.Database/ Fleet.Infra.Database/
COPY Fleet.Infra.FileStorage/ Fleet.Infra.FileStorage/
COPY Fleet.Infra.Messaging/ Fleet.Infra.Messaging/

RUN dotnet publish src/Fleet.Api/Fleet.Api.csproj \
    -c Release \
    -o /app/out

# Step 2: Create the runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/out .

EXPOSE 80
ENTRYPOINT ["dotnet","Fleet.Api.dll"]
