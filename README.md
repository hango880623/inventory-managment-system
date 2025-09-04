# Inventory Management System

A .NET C# web application for inventory management practice project.

## 🎯 Project Overview

This is a practice project to learn C# and .NET web development with ASP.NET Core MVC, focusing on building a complete inventory management system.

## 🛠️ Technology Stack

- **.NET 8.0** - Backend framework
- **ASP.NET Core MVC** - Web application framework
- **SQL Server** - Database
- **Entity Framework Core** - ORM
- **Bootstrap 5** - Frontend styling
- **jQuery** - JavaScript functionality

## 📋 Features to Implement

1. **Database Setup** - SQL Server with Entity Framework
2. **User Management** - Add, edit, delete users
3. **Authentication** - Login/logout system
4. **Dashboard** - Main application interface
5. **Inventory Management** - Product CRUD operations
6. **Data Grid** - Paginated product listing
7. **Notifications** - Toast alerts and message boxes

## 🗂️ Project Structure

```
InventoryManagementSystem/
├── Controllers/          # MVC Controllers
├── Models/              # Data Models & ViewModels
├── Views/               # Razor Views
├── Data/                # Entity Framework Context
├── wwwroot/             # Static files (CSS, JS, Images)
└── appsettings.json     # Configuration
```

## 🚀 Getting Started

### Prerequisites
- .NET 8.0 SDK
- SQL Server (LocalDB or Express)
- Visual Studio 2022 or VS Code

### Setup
1. Clone the repository
2. Open in Visual Studio
3. Update connection string in `appsettings.json`
4. Run database migrations: `dotnet ef database update`
5. Run the application: `dotnet run`

## 📊 Database Tables

- **Users** - User accounts
- **Products** - Inventory items
- **Categories** - Product categories
- **Suppliers** - Vendor information

## 🎯 Learning Objectives

- ASP.NET Core MVC architecture
- Entity Framework Core with SQL Server
- Authentication and authorization
- CRUD operations
- Data validation
- Responsive web design
- JavaScript/jQuery integration

## 📝 Notes

This is a practice project for learning C# and .NET web development. Focus on understanding the MVC pattern, database operations, and web application structure.