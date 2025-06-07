# Build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copia o csproj e restaura dependências
COPY *.csproj ./
RUN dotnet restore

# Copia todo o código e publica
COPY . .
RUN dotnet publish -c Release -o out /p:UseAppHost=false

# Runtime stage
FROM mcr.microsoft.com/dotnet/runtime:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/out .

# Não expomos portas, pois é um worker
ENTRYPOINT ["dotnet", "Fleet.Worker.dll"]
