# Pharmacy Application

A comprehensive pharmacy management system developed to streamline inventory, orders, and delivery management for pharmaceutical businesses.

## Overview

This application offers an end-to-end solution for pharmacy operations, including product management, order processing, and delivery tracking. Built with a clean architecture approach, it separates concerns into distinct layers for maximum maintainability and scalability.

## Features

- **User Management**
  - Role-based authentication (Admin/Customer)
  - Secure password storage with hashing
  - JWT token-based authentication

- **Product Management**
  - Hierarchical categorization (Main and Sub-categories)
  - Inventory tracking
  - Detailed product information

- **Order Processing**
  - Complete order lifecycle management
  - Status tracking (pending, delivering, delivered, declined)
  - Delivery assignment

- **Delivery Management**
  - Delivery personnel tracking
  - Order assignment
  - Delivery status updates

## API Endpoints

The application provides a RESTful API with the following endpoints:

### Authentication

| Method | Endpoint | Description | Auth Required |
|--------|----------|-------------|--------------|
| POST | `/api/Auth/RegisterUser` | Register a new user | No |
| POST | `/api/Auth/Login` | User login and token generation | No |

### Products

| Method | Endpoint | Description | Auth Required |
|--------|----------|-------------|--------------|
| POST | `/api/Products/CreateProduct` | Create a new product | Yes (Admin) |
| PUT | `/api/Products/UpdateProduct` | Update an existing product | Yes (Admin) |
| DELETE | `/api/Products/DeleteProduct` | Delete a product | Yes (Admin) |
| GET | `/api/Products/ProductWithName/{productName}` | Get product by name | Yes |
| GET | `/api/Products/ProductsWithCategory/{category}` | Get products by main category | Yes |
| GET | `/api/Products/ProductsWithSubCategory/{subCategory}` | Get products by sub-category | Yes |
| GET | `/api/Products/isAvailable/{productName}` | Check product availability | Yes |

### Orders

| Method | Endpoint | Description | Auth Required |
|--------|----------|-------------|--------------|
| GET | `/api/Orders/ShowAllOrders` | Get all orders | Yes (Admin) |
| GET | `/api/Orders/ShowOrdersWithStatus/{status}` | Get orders by status | Yes (Admin) |
| PUT | `/api/Orders/AcceptUserOrder` | Accept a user's order | Yes (Admin) |
| PUT | `/api/Orders/DeclineUserOrder` | Decline a user's order | Yes (Admin) |
| PUT | `/api/Orders/SetOrderDelivered` | Mark an order as delivered | Yes (Admin) |
| DELETE | `/api/Orders/DeleteOrder/{id}` | Delete an order | Yes (Admin) |

### Delivery

| Method | Endpoint | Description | Auth Required |
|--------|----------|-------------|--------------|
| GET | `/api/Delivery/GetDeliveryManWithPhone/{phoneNumber}` | Get delivery person by phone | Yes (Admin) |
| GET | `/api/Delivery/GetDeliveryManWithName/{name}` | Get delivery person by name | Yes (Admin) |
| POST | `/api/Delivery/CreateNewDeliveryMan` | Add a new delivery person | Yes (Admin) |
| PUT | `/api/Delivery/UpdateDeliveryMan/{oldPhoneNumber}` | Update delivery person details | Yes (Admin) |
| DELETE | `/api/Delivery/DeleteDeliveryMan/{phoneNumber}` | Delete a delivery person | Yes (Admin) |

## Architecture

The application follows a layered architecture pattern:

### Domain Layer
Contains core business entities and logic:
- User, Product, Order, Delivery
- Medical categories
- Order statuses

### Infrastructure Layer
Handles data access and persistence:
- Entity Framework Core with SQL Server
- Dapper for optimized queries
- Repository implementations

### Application Layer
Implements business logic and use cases:
- Authentication and user management
- Order processing
- Product and inventory management
- Delivery coordination

### Presentation Layer
User interface for the application:
- WPF-based desktop application
- User-friendly interfaces for all operations

## Technology Stack

- **.NET Framework**
- **Entity Framework Core**
- **Dapper**
- **SQL Server**
- **WPF** (Windows Presentation Foundation)
- **JWT Authentication**

## Getting Started

### Prerequisites
- Windows OS
- .NET Framework (latest version)
- SQL Server (local or remote)
- Visual Studio 2022 or later

### Installation

1. Clone the repository:
   ```
   git clone https://github.com/yourusername/PharmacyApplication.git
   ```

2. Open the solution file `PharmacyApplication_MakingSomething.sln` in Visual Studio.

3. Configure the database connection in `InfrastructureLayer/db_config.json`.

4. Build the solution.

5. Run the application.

### Configuration

Database configuration is stored in `InfrastructureLayer/db_config.json`. Modify this file to point to your SQL Server instance.

## Usage

The application provides interfaces for:

- Managing pharmaceutical products and categories
- Processing customer orders
- Tracking order status
- Managing delivery personnel
- User authentication and administration

## License

This project is licensed under the terms of the license included in the repository.

## Acknowledgments

- .NET Framework and associated technologies
- Entity Framework Core team
- All contributors to the project

---

Last Updated: May 17, 2025
