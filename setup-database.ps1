# PowerShell script to set up the database for Inventory Management System

Write-Host "Setting up Inventory Management System Database..." -ForegroundColor Green

# Check if .NET is installed
try {
    $dotnetVersion = dotnet --version
    Write-Host "Found .NET version: $dotnetVersion" -ForegroundColor Green
} catch {
    Write-Host "Error: .NET SDK not found. Please install .NET 8 SDK." -ForegroundColor Red
    exit 1
}

# Check if Entity Framework tools are installed
try {
    $efVersion = dotnet ef --version
    Write-Host "Found Entity Framework tools: $efVersion" -ForegroundColor Green
} catch {
    Write-Host "Installing Entity Framework tools..." -ForegroundColor Yellow
    dotnet tool install --global dotnet-ef
}

# Restore packages
Write-Host "Restoring NuGet packages..." -ForegroundColor Yellow
dotnet restore

# Build the project
Write-Host "Building the project..." -ForegroundColor Yellow
dotnet build

# Check if migrations exist
$migrationsPath = "Migrations"
if (Test-Path $migrationsPath) {
    Write-Host "Migrations folder found. Updating database..." -ForegroundColor Yellow
    dotnet ef database update
} else {
    Write-Host "No migrations found. Creating initial migration..." -ForegroundColor Yellow
    dotnet ef migrations add InitialCreate
    Write-Host "Updating database..." -ForegroundColor Yellow
    dotnet ef database update
}

Write-Host "Database setup completed successfully!" -ForegroundColor Green
Write-Host "You can now run the application with: dotnet run" -ForegroundColor Cyan
