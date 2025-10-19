# ====================================
# Stage 1: Build React Frontend
# ====================================
FROM node:18-alpine AS frontend-build
WORKDIR /app/frontend

# Copy package files and install dependencies
COPY jobtrack360.ui/package*.json ./
RUN npm install

# Copy frontend source and build
COPY jobtrack360.ui/ ./
RUN npm run build

# ====================================
# Stage 2: Build .NET Backend
# ====================================
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS backend-build
WORKDIR /src

# Copy solution file
COPY JobTrack360.sln ./

# Copy all project files
COPY JobTrack360.API/JobTrack360.API.csproj JobTrack360.API/
COPY JobTrack360.Application/JobTrack360.Application.csproj JobTrack360.Application/
COPY JobTrack360.Domain/JobTrack360.Domain.csproj JobTrack360.Domain/
COPY JobTrack360.Infrastructure/JobTrack360.Infrastructure.csproj JobTrack360.Infrastructure/

# Restore dependencies (only for API project, excluding tests)
RUN dotnet restore JobTrack360.API/JobTrack360.API.csproj

# Copy all source code
COPY JobTrack360.API/ JobTrack360.API/
COPY JobTrack360.Application/ JobTrack360.Application/
COPY JobTrack360.Domain/ JobTrack360.Domain/
COPY JobTrack360.Infrastructure/ JobTrack360.Infrastructure/

# Build and publish backend
WORKDIR /src/JobTrack360.API
RUN dotnet publish -c Release -o /app/publish

# ====================================
# Stage 3: Final Runtime Image
# ====================================
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS final
WORKDIR /app

# Install nginx and supervisor
RUN apt-get update && \
    apt-get install -y nginx supervisor && \
    rm -rf /var/lib/apt/lists/*

# Copy backend from build stage
COPY --from=backend-build /app/publish ./backend

# Copy frontend build to nginx directory
COPY --from=frontend-build /app/frontend/build /var/www/html

# Copy nginx configuration
COPY nginx.conf /etc/nginx/sites-available/default

# Copy supervisor configuration
COPY supervisord.conf /etc/supervisor/conf.d/supervisord.conf

# Expose port 80 for frontend (nginx will proxy API calls)
EXPOSE 80

# Start supervisor to run both nginx and dotnet
CMD ["/usr/bin/supervisord", "-c", "/etc/supervisor/conf.d/supervisord.conf"]