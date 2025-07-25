--Create database for the first time (During )
CREATE DATABASE SysStockDB;
GO

--after createing databse you may skip the first 2 line.

-- Use the database
USE SysStockDB;
GO

-- Create table: Brands
CREATE TABLE dbo.Brands (
    BrandId INT IDENTITY(1,1) PRIMARY KEY,
    Name VARCHAR(50) NOT NULL,
    Description TEXT NULL,
    IsActive BIT DEFAULT 1
);

-- Create table: Categories
CREATE TABLE dbo.Categories (
    CategoryId INT IDENTITY(1,1) PRIMARY KEY,
    Name VARCHAR(50) NOT NULL,
    Description TEXT NULL,
    IsActive BIT DEFAULT 1
);

-- Create table: Users
CREATE TABLE dbo.Users (
    UserId INT IDENTITY(1,1) PRIMARY KEY,
    Username VARCHAR(50) NOT NULL UNIQUE,
    [Password] VARCHAR(255) NOT NULL,
    FullName VARCHAR(100) NULL,
    Email VARCHAR(150) NULL,
    Role VARCHAR(50) NULL,
    IsActive BIT DEFAULT 1,
    CreatedAt DATETIME DEFAULT GETDATE()
);

-- Create table: Products
CREATE TABLE dbo.Products (
    ProductId INT IDENTITY(1,1) PRIMARY KEY,
    Name VARCHAR(100) NOT NULL,
    CategoryId INT NULL,
    BrandId INT NULL,
    UnitPrice DECIMAL(10,2) NOT NULL,
    DiscountPercent DECIMAL(5,2) DEFAULT 0,
    QuantityInStock INT DEFAULT 0,
    Description TEXT NULL,
    IsActive BIT DEFAULT 1,
    CreatedAt DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (BrandId) REFERENCES dbo.Brands(BrandId),
    FOREIGN KEY (CategoryId) REFERENCES dbo.Categories(CategoryId)
);

-- Create table: Orders
CREATE TABLE dbo.Orders (
    OrderId INT IDENTITY(1,1) PRIMARY KEY,
    UserId INT NULL,
    OrderDate DATETIME DEFAULT GETDATE(),
    SubTotal DECIMAL(12,2) NULL,
    GrossDiscount DECIMAL(10,2) DEFAULT 0,
    VATPercentage DECIMAL(5,2) DEFAULT 0,
    VATAmount DECIMAL(10,2) NULL,
    TotalAmount DECIMAL(12,2) NULL,
    AmountPaid DECIMAL(12,2) NULL,
    ChangeGiven DECIMAL(12,2) NULL,
    PaymentMethod VARCHAR(50) NULL,
    Remarks TEXT NULL,
    FOREIGN KEY (UserId) REFERENCES dbo.Users(UserId)
);

-- Create table: OrderItems
CREATE TABLE dbo.OrderItems (
    OrderItemId INT IDENTITY(1,1) PRIMARY KEY,
    OrderId INT NULL,
    ProductId INT NULL,
    Quantity INT NOT NULL,
    UnitPrice DECIMAL(10,2) NULL,
    DiscountPercent DECIMAL(5,2) DEFAULT 0,
    DiscountAmount DECIMAL(10,2) NULL,
    LineTotal DECIMAL(12,2) NULL,
    FOREIGN KEY (OrderId) REFERENCES dbo.Orders(OrderId),
    FOREIGN KEY (ProductId) REFERENCES dbo.Products(ProductId)
);