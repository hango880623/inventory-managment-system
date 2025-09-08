#!/bin/bash

# Bash script to set up the database for Inventory Management System

echo "Setting up Inventory Management System Database..."

# Check if .NET is installed
if ! command -v dotnet &> /dev/null; then
    echo "Error: .NET SDK not found. Please install .NET 8 SDK."
    exit 1
fi

DOTNET_VERSION=$(dotnet --version)
echo "Found .NET version: $DOTNET_VERSION"

# Check if Entity Framework tools are installed
if ! command -v dotnet-ef &> /dev/null; then
    echo "Installing Entity Framework tools..."
    dotnet tool install --global dotnet-ef
fi

# Restore packages
echo "Restoring NuGet packages..."
dotnet restore

# Build the project
echo "Building the project..."
dotnet build

# Check if migrations exist
if [ -d "Migrations" ]; then
    echo "Migrations folder found. Updating database..."
    dotnet ef database update
else
    echo "No migrations found. Creating initial migration..."
    dotnet ef migrations add InitialCreate
    echo "Updating database..."
    dotnet ef database update
fi

echo "Database setup completed successfully!"
echo "You can now run the application with: dotnet run"
