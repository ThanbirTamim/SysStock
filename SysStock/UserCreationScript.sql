USE [SysStockDB]
GO

INSERT INTO [dbo].[Users] (
    [Username],
    [Password],
    [FullName],
    [Email],
    [Role],
    [IsActive],
    [CreatedAt]
)
VALUES
(
    'Thanbir',
    '1234',
    'Sheikh Thanbir Alam',
    'sk.tamim56@gmail.com',
    'admin',
    1,
    GETDATE()
),
(
    'Tamim',
    '1234',
    'Tamim Test',
    'sk.tamim57@gmail.com',
    'staff',
    1,
    GETDATE()
);
