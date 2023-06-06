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

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230419184922_InitialMigration')
BEGIN
    CREATE TABLE [Gender] (
        [Id] uniqueidentifier NOT NULL,
        [Description] nvarchar(max) NULL,
        CONSTRAINT [PK_Gender] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230419184922_InitialMigration')
BEGIN
    CREATE TABLE [Student] (
        [Id] uniqueidentifier NOT NULL,
        [FirstName] nvarchar(max) NULL,
        [LastName] nvarchar(max) NULL,
        [DateOfBirth] datetime2 NOT NULL,
        [Email] nvarchar(max) NULL,
        [Mobile] bigint NOT NULL,
        [ProfileImageUrl] nvarchar(max) NULL,
        [GenderId] uniqueidentifier NOT NULL,
        CONSTRAINT [PK_Student] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Student_Gender_GenderId] FOREIGN KEY ([GenderId]) REFERENCES [Gender] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230419184922_InitialMigration')
BEGIN
    CREATE TABLE [Address] (
        [Id] uniqueidentifier NOT NULL,
        [PhysicalAddress] nvarchar(max) NULL,
        [PostalAddress] nvarchar(max) NULL,
        [StudentId] uniqueidentifier NOT NULL,
        CONSTRAINT [PK_Address] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Address_Student_StudentId] FOREIGN KEY ([StudentId]) REFERENCES [Student] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230419184922_InitialMigration')
BEGIN
    CREATE UNIQUE INDEX [IX_Address_StudentId] ON [Address] ([StudentId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230419184922_InitialMigration')
BEGIN
    CREATE INDEX [IX_Student_GenderId] ON [Student] ([GenderId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20230419184922_InitialMigration')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20230419184922_InitialMigration', N'5.0.9');
END;
GO

COMMIT;
GO

