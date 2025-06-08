# Step 1: Build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Restores only the main project
COPY *.csproj ./
RUN dotnet restore

# Copies the rest of the files and publishes
COPY . .
RUN dotnet publish -c Release -o out /p:UseAppHost=false

# Step 2: Runtime
FROM mcr.microsoft.com/dotnet/runtime:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/out .

# No ports are exposed as this is a worker service
ENTRYPOINT ["dotnet", "Fleet.Worker.dll"]
