# 1: Build the application
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src

# Copy the csproj file and restore dependencies
COPY CallCenterAgentManager.Api.csproj .
RUN dotnet restore

# Copy the entire application code and build
COPY . .
RUN dotnet publish -c Release -o /app

# 2: Create the final image
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS final
WORKDIR /app
COPY --from=build /app .

# Set the ASPNETCORE_ENVIRONMENT environment variable to "Development" or "Staging" or "Production"
ENV ASPNETCORE_ENVIRONMENT=Local

# Expose the application port
EXPOSE 80

# Start the application
ENTRYPOINT ["dotnet", "CallCenterAgentManager.Api.dll"]

#Build command: docker build -t bestblogs-api .
#Run command: docker run -p 8080:80 bestblogs-api


