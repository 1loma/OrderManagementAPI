# OrderManagementAPI
First ASP.NET Core WEB API Project + Entity Framework


# Order Management API (Code First Approach)

This is a sample ASP.NET Core Web API project for managing customers, orders, and order details.

## Features

- CRUD operations for Customers, Orders, and OrderDetails
- Entity Framework Core for database access
- Validations using Data Annotations

## Getting Started

1. Clone the repository
2. Open the project in Visual Studio 2022
3. Restore NuGet packages
4. Update `appsettings.json` with your SQL Server connection string
5. Run `Update-Database` in Package Manager Console to create the database
6. Run the API

## API Endpoints

- `/api/customer`
- `/api/order`
- `/api/orderdetail`




## Seed Data

Add seed data for Customers, Orders, and OrderDetails after running migrations.

**Steps:**
1. Make sure migrations are updated in Visual Studio 2022.
2. Insert data in this order: **Customers → Orders → OrderDetails**.

### Customers
```sql
INSERT INTO Customers (FullName, Email, Phone)
VALUES 
('Luka Lomadze', 'lomadzeluka2006@gmail.com', '599 123 567'),
('Anna Bagrationi', 'anna.b@gmail.com', '577 987 543'),
('Giorgi Tsereteli', 'giorgi.t@gmail.com', NULL),
('Nino Kapanadze', 'nino.k@gmail.com', '555 111 222');

INSERT INTO Orders (OrderDate, CustomerId)
VALUES
('2026-01-15', 1),
('2026-01-16', 2),
('2026-01-16', 3),
('2026-01-17', 4);

INSERT INTO OrderDetails (OrderId, TotalPrice, ShippingAddress)
VALUES
(1, 49.99, 'Tbilisi, Saburtalo'),
(2, 25.50, 'Tbilisi, Vake'),
(3, 120.00, 'Batumi, Old Town'),
(4, 15.75, 'Kutaisi, Center');
