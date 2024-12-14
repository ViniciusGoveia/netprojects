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
CREATE TABLE [Category] (
    [ID] int NOT NULL IDENTITY,
    [Name] NVARCHAR(80) NOT NULL,
    [Slug] NVARCHAR(80) NOT NULL,
    CONSTRAINT [PK_Category] PRIMARY KEY ([ID])
);

CREATE TABLE [Role] (
    [ID] int NOT NULL IDENTITY,
    [Name] nvarchar(max) NOT NULL,
    [Slug] nvarchar(max) NOT NULL,
    CONSTRAINT [PK_Role] PRIMARY KEY ([ID])
);

CREATE TABLE [Tag] (
    [ID] int NOT NULL IDENTITY,
    [Name] nvarchar(max) NOT NULL,
    [Slug] nvarchar(max) NOT NULL,
    CONSTRAINT [PK_Tag] PRIMARY KEY ([ID])
);

CREATE TABLE [User] (
    [ID] int NOT NULL IDENTITY,
    [Name] NVARCHAR(80) NOT NULL,
    [Email] nvarchar(max) NOT NULL,
    [PasswordHash] nvarchar(max) NOT NULL,
    [Bio] nvarchar(max) NOT NULL,
    [Image] nvarchar(max) NOT NULL,
    [Slug] NVARCHAR(80) NOT NULL,
    [GitHub] nvarchar(max) NOT NULL,
    CONSTRAINT [PK_User] PRIMARY KEY ([ID])
);

CREATE TABLE [Post] (
    [ID] int NOT NULL IDENTITY,
    [CategoryID] int NOT NULL,
    [AuthorID] int NOT NULL,
    [Title] nvarchar(max) NOT NULL,
    [Summary] nvarchar(max) NOT NULL,
    [Body] nvarchar(max) NOT NULL,
    [Slug] nvarchar(450) NOT NULL,
    [CreateDate] datetime2 NOT NULL,
    [LastUpdateDate] DATETIME NOT NULL DEFAULT '2024-12-03T00:10:44.6598111Z',
    CONSTRAINT [PK_Post] PRIMARY KEY ([ID]),
    CONSTRAINT [FK_Post_Author] FOREIGN KEY ([AuthorID]) REFERENCES [User] ([ID]) ON DELETE CASCADE,
    CONSTRAINT [FK_Post_Cathegory] FOREIGN KEY ([CategoryID]) REFERENCES [Category] ([ID]) ON DELETE CASCADE
);

CREATE TABLE [UserRole] (
    [RoleId] int NOT NULL,
    [UserId] int NOT NULL,
    CONSTRAINT [PK_UserRole] PRIMARY KEY ([RoleId], [UserId]),
    CONSTRAINT [FK_UserRole_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [Role] ([ID]) ON DELETE CASCADE,
    CONSTRAINT [FK_UserRole_UserId] FOREIGN KEY ([UserId]) REFERENCES [User] ([ID]) ON DELETE CASCADE
);

CREATE TABLE [PostTag] (
    [PostId] int NOT NULL,
    [TagId] int NOT NULL,
    CONSTRAINT [PK_PostTag] PRIMARY KEY ([PostId], [TagId]),
    CONSTRAINT [FK_PostTag_PostId] FOREIGN KEY ([PostId]) REFERENCES [Tag] ([ID]) ON DELETE CASCADE,
    CONSTRAINT [FK_PostTag_TagId] FOREIGN KEY ([TagId]) REFERENCES [Post] ([ID]) ON DELETE CASCADE
);

CREATE UNIQUE INDEX [IX_Category_Slug] ON [Category] ([Slug]);

CREATE INDEX [IX_Post_AuthorID] ON [Post] ([AuthorID]);

CREATE INDEX [IX_Post_CategoryID] ON [Post] ([CategoryID]);

CREATE UNIQUE INDEX [IX_Post_Slug] ON [Post] ([Slug]);

CREATE INDEX [IX_PostTag_TagId] ON [PostTag] ([TagId]);

CREATE UNIQUE INDEX [IX_User_Slug] ON [User] ([Slug]);

CREATE INDEX [IX_UserRole_UserId] ON [UserRole] ([UserId]);

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20241203001045_InitialMigration', N'9.0.0');

COMMIT;
GO

