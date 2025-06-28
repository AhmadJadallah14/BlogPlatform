# Base stage
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

# Create non-root user
RUN groupadd -r appgroup && useradd -r -g appgroup appuser

# Switch to non-root user later (after setting permissions)
# We'll do this in the final stage

# Build stage
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

COPY ["src/BlogPlatform.API/BlogPlatform.API.csproj", "src/BlogPlatform.API/"]
COPY ["src/BlogPlatform.Application/BlogPlatform.Application.csproj", "src/BlogPlatform.Application/"]
COPY ["src/BlogPlatform.Domain/BlogPlatform.Domain.csproj", "src/BlogPlatform.Domain/"]
COPY ["src/BlogPlatform.Infrastructure/BlogPlatform.Infrastructure.csproj", "src/BlogPlatform.Infrastructure/"]

RUN dotnet restore "src/BlogPlatform.API/BlogPlatform.API.csproj"
COPY . .
WORKDIR "/src/src/BlogPlatform.API"
RUN dotnet build "BlogPlatform.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Publish stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "BlogPlatform.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Final stage
FROM base AS final
WORKDIR /app

# Copy published output
COPY --from=publish /app/publish .

# Create uploads folder AFTER copying files
RUN mkdir -p /app/wwwroot/uploads && \
    chown appuser:appgroup /app/wwwroot/uploads && \
    chmod 755 /app/wwwroot/uploads

# Switch to non-root user
USER appuser

ENTRYPOINT ["dotnet", "BlogPlatform.API.dll"]
