
USE SysStockDB;
GO

-- Seed data for Brands
SET IDENTITY_INSERT dbo.Brands ON;
INSERT INTO dbo.Brands (BrandId, Name, Description, IsActive)
VALUES 
    (1, 'Samsung', 'Leading electronics manufacturer from South Korea', 1),
    (2, 'Apple', 'Premium technology products manufacturer', 1),
    (3, 'Dell', 'American computer technology company', 1),
    (4, 'Nike', 'Global sports and fitness brand', 1),
    (5, 'Adidas', 'German sporting goods manufacturer', 1),
    (6, 'Sony', 'Japanese electronics and entertainment company', 1),
    (7, 'LG', 'Korean electronics manufacturer', 1),
    (8, 'HP', 'American technology company', 1),
    (9, 'Lenovo', 'Chinese technology company', 1),
    (10, 'Puma', 'German sportswear manufacturer', 1);
SET IDENTITY_INSERT dbo.Brands OFF;

-- Seed data for Categories
SET IDENTITY_INSERT dbo.Categories ON;
INSERT INTO dbo.Categories (CategoryId, Name, Description, IsActive)
VALUES 
    (1, 'Smartphones', 'Mobile phones and accessories', 1),
    (2, 'Laptops', 'Portable computers and accessories', 1),
    (3, 'TVs', 'Television sets and home entertainment', 1),
    (4, 'Sports Shoes', 'Athletic and casual footwear', 1),
    (5, 'Tablets', 'Tablet computers and accessories', 1),
    (6, 'Sportswear', 'Athletic clothing and accessories', 1),
    (7, 'Gaming', 'Gaming consoles and accessories', 1),
    (8, 'Audio', 'Headphones, speakers, and sound systems', 1);
SET IDENTITY_INSERT dbo.Categories OFF;

-- Seed data for Products
SET IDENTITY_INSERT dbo.Products ON;
INSERT INTO dbo.Products (ProductId, Name, CategoryId, BrandId, UnitPrice, DiscountPercent, QuantityInStock, Description, IsActive)
VALUES 
    -- Smartphones
    (1, 'Samsung Galaxy S23 Ultra', 1, 1, 1199.99, 0, 50, 'Latest flagship smartphone with advanced camera system', 1),
    (2, 'iPhone 14 Pro Max', 1, 2, 1299.99, 0, 45, 'Premium iPhone with ProMotion display', 1),
    
    -- Laptops
    (3, 'Dell XPS 15', 2, 3, 1799.99, 5, 30, 'Premium laptop with 4K display', 1),
    (4, 'MacBook Pro 16', 2, 2, 2499.99, 0, 25, 'Professional grade laptop with M2 chip', 1),
    (5, 'Lenovo ThinkPad X1', 2, 9, 1599.99, 10, 20, 'Business laptop with premium features', 1),
    
    -- TVs
    (6, 'Samsung Neo QLED 65"', 3, 1, 2999.99, 15, 15, 'Premium 4K Smart TV', 1),
    (7, 'LG OLED C2 55"', 3, 7, 1999.99, 10, 20, 'OLED 4K Smart TV', 1),
    (8, 'Sony Bravia XR 75"', 3, 6, 3499.99, 5, 10, 'Premium LED 4K Smart TV', 1),
    
    -- Sports Shoes
    (9, 'Nike Air Max 2025', 4, 4, 179.99, 0, 100, 'Premium running shoes', 1),
    (10, 'Adidas Ultraboost', 4, 5, 159.99, 0, 85, 'Comfortable running shoes', 1),
    (11, 'Puma RS-X', 4, 10, 129.99, 15, 75, 'Casual sports shoes', 1),
    
    -- Tablets
    (12, 'iPad Pro 12.9"', 5, 2, 1099.99, 0, 40, 'Premium tablet with M2 chip', 1),
    (13, 'Samsung Galaxy Tab S9', 5, 1, 899.99, 5, 35, 'Android flagship tablet', 1),
    
    -- Gaming
    (14, 'PlayStation 5', 7, 6, 499.99, 0, 25, 'Next-gen gaming console', 1),
    
    -- Audio
    (15, 'Sony WH-1000XM5', 8, 6, 399.99, 10, 60, 'Premium noise-cancelling headphones', 1),
    (16, 'Samsung Galaxy Buds Pro', 8, 1, 199.99, 5, 80, 'True wireless earbuds', 1);
SET IDENTITY_INSERT dbo.Products OFF;