IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230325153037_InitialDeploy')
BEGIN
    CREATE TABLE [Clients] (
        [Id] int NOT NULL IDENTITY,
        [Name] nvarchar(max) NULL,
        [Age] int NOT NULL,
        CONSTRAINT [PK_Clients] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230325153037_InitialDeploy')
BEGIN
    CREATE TABLE [DrinkTypes] (
        [Id] int NOT NULL IDENTITY,
        [Name] nvarchar(max) NULL,
        [NrOfBrands] int NOT NULL,
        [Stock] int NOT NULL,
        [ProfitMargin] real NOT NULL,
        CONSTRAINT [PK_DrinkTypes] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230325153037_InitialDeploy')
BEGIN
    CREATE TABLE [Drinks] (
        [Id] int NOT NULL IDENTITY,
        [Name] nvarchar(max) NULL,
        [Price] int NULL,
        [Quantity] int NULL,
        [Abv] real NULL,
        [DrinkTypeId] int NOT NULL,
        CONSTRAINT [PK_Drinks] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Drinks_DrinkTypes_DrinkTypeId] FOREIGN KEY ([DrinkTypeId]) REFERENCES [DrinkTypes] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230325153037_InitialDeploy')
BEGIN
    CREATE TABLE [Orders] (
        [ClientId] int NOT NULL,
        [DrinkId] int NOT NULL,
        [DateTime] datetime2 NOT NULL,
        [NrOfDrinks] int NOT NULL,
        CONSTRAINT [PK_Orders] PRIMARY KEY ([DrinkId], [ClientId]),
        CONSTRAINT [FK_Orders_Clients_ClientId] FOREIGN KEY ([ClientId]) REFERENCES [Clients] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_Orders_Drinks_DrinkId] FOREIGN KEY ([DrinkId]) REFERENCES [Drinks] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230325153037_InitialDeploy')
BEGIN
    CREATE INDEX [IX_Drinks_DrinkTypeId] ON [Drinks] ([DrinkTypeId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230325153037_InitialDeploy')
BEGIN
    CREATE INDEX [IX_Orders_ClientId] ON [Orders] ([ClientId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230325153037_InitialDeploy')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20230325153037_InitialDeploy', N'7.0.4');
END;
GO

COMMIT;
GO

