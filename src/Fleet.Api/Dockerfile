# Step 1: Build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Restores only the main project
COPY ./*.csproj ./
RUN dotnet restore

# Copy the rest of the files and publish
COPY . .
RUN dotnet publish -c Release -o out

# Step 2: Create the runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/out .

EXPOSE 80
ENTRYPOINT ["dotnet","Fleet.Api.dll"]
