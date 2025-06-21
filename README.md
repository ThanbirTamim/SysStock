# SysStock - Super Shop Inventory Management System

SysStock is a system for tracking items in a super shop, featuring two user panels: **Admin** and **Staff**.

- **Admin Panel**: Allows admins to add and update products.
- **Staff Panel**: Allows staff to sell products.

## Features

- Track inventory and product stock.
- Role-based access: Admins manage inventory; staff sell products.
- SQL Server 2021 database backend.

---

## Getting Started

### Prerequisites

- **SQL Server 2021** installed and running on your machine.
- Access to modify and run SQL scripts.
- The SysStock project files, including:
  - `SysStock\SysStock.sql`
  - `SysStock\UserCreationScript.sql`
  - `SysStock\DataSeedForBrandsCategoryProducts.sql` (or directory for individual seeding files)

### Installation Steps

#### 1. **Initialize the Database**

Open SQL Server Management Studio (SSMS) and execute the following script to create the database:

```sql
CREATE DATABASE SysStockDB;
GO
```

#### 2. **Set Up Database Schema**

- Open `SysStock\SysStock.sql`.
- **Skip** the database creation part (the above script), and execute the rest of the file to create tables, stored procedures, etc.

#### 3. **Create Users**

- Run the script `SysStock\UserCreationScript.sql` to create required SQL users and assign appropriate permissions.

#### 4. **Seed Data**

- Run the script(s) in `SysStock\DataSeedForBrandsCategoryProducts.sql` (or the scripts within the `DataSeedForBrandsCategoryProducts` folder) to seed initial data for Brands, Categories, and Products.

#### 5. **Configure Application**

- If your SQL Server instance name is different from the default, **modify the connection string** in the application's configuration file (e.g., `appsettings.json` or `web.config`) to match your server name and credentials.

---

## Usage

- **Admins**: Log in using admin credentials to add or update products.
- **Staff**: Log in using staff credentials to process sales.

## Notes

- Make sure all SQL scripts execute successfully before running the application.
- You may modify the codebase as needed to suit your environment.

## Troubleshooting

- If you encounter connection issues, verify your server name and credentials in the connection string.
- Ensure that all required tables and seed data are present in `SysStockDB`.

---

## License

This project is for educational and demonstration purposes.

---

## Contact

For questions or support, please open an issue or contact the project maintainer sk.tamim56@gmail.com.