
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, and Azure
-- --------------------------------------------------
-- Date Created: 06/12/2014 16:15:50
-- Generated from EDMX file: J:\Mac Files\Desktop\Work\Projects\Home\BillManager\EFModelFirst1\BillManagerModel.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [BillManager];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_BillCategory]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Bills] DROP CONSTRAINT [FK_BillCategory];
GO
IF OBJECT_ID(N'[dbo].[FK_FavoriteLinkWebsite]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[FavoriteLinks] DROP CONSTRAINT [FK_FavoriteLinkWebsite];
GO
IF OBJECT_ID(N'[dbo].[FK_BillPayment]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Payments] DROP CONSTRAINT [FK_BillPayment];
GO
IF OBJECT_ID(N'[dbo].[FK_AccountWebsite]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Websites] DROP CONSTRAINT [FK_AccountWebsite];
GO
IF OBJECT_ID(N'[dbo].[FK_AccountBill]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Bills] DROP CONSTRAINT [FK_AccountBill];
GO
IF OBJECT_ID(N'[dbo].[FK_AccountCategory]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Categories] DROP CONSTRAINT [FK_AccountCategory];
GO
IF OBJECT_ID(N'[dbo].[FK_AccountFavoriteLink]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[FavoriteLinks] DROP CONSTRAINT [FK_AccountFavoriteLink];
GO
IF OBJECT_ID(N'[dbo].[FK_PayOptionWebsite]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[PayOptions] DROP CONSTRAINT [FK_PayOptionWebsite];
GO
IF OBJECT_ID(N'[dbo].[FK_BillPayOption]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[PayOptions] DROP CONSTRAINT [FK_BillPayOption];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Accounts]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Accounts];
GO
IF OBJECT_ID(N'[dbo].[FavoriteLinks]', 'U') IS NOT NULL
    DROP TABLE [dbo].[FavoriteLinks];
GO
IF OBJECT_ID(N'[dbo].[Bills]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Bills];
GO
IF OBJECT_ID(N'[dbo].[Categories]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Categories];
GO
IF OBJECT_ID(N'[dbo].[PayOptions]', 'U') IS NOT NULL
    DROP TABLE [dbo].[PayOptions];
GO
IF OBJECT_ID(N'[dbo].[Websites]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Websites];
GO
IF OBJECT_ID(N'[dbo].[Payments]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Payments];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Accounts'
CREATE TABLE [dbo].[Accounts] (
    [AccountId] int IDENTITY(1,1) NOT NULL,
    [UserName] nvarchar(20)  NOT NULL,
    [Password] nvarchar(20)  NOT NULL,
    [FirstName] nvarchar(20)  NOT NULL,
    [LastName] nvarchar(20)  NOT NULL,
    [Email] nvarchar(max)  NULL
);
GO

-- Creating table 'FavoriteLinks'
CREATE TABLE [dbo].[FavoriteLinks] (
    [FavoriteLinkId] int IDENTITY(1,1) NOT NULL,
    [Label] nvarchar(20)  NOT NULL,
    [AccountId] int  NOT NULL,
    [Website_WebsiteId] int  NOT NULL
);
GO

-- Creating table 'Bills'
CREATE TABLE [dbo].[Bills] (
    [BillId] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(20)  NOT NULL,
    [Description] nvarchar(max)  NULL,
    [CommenceDate] datetime  NULL,
    [BillKind] int  NOT NULL,
    [IsActive] bit  NOT NULL,
    [BillFrequency] int  NOT NULL,
    [DayDueInMonth] int  NOT NULL,
    [AutopayIsEnrolled] bit  NULL,
    [PhoneNum] nvarchar(20)  NULL,
    [InitialBalance] decimal(10,2)  NULL,
    [AccountNum] nvarchar(max)  NULL,
    [AccountId] int  NOT NULL,
    [Category_CategoryId] int  NULL
);
GO

-- Creating table 'Categories'
CREATE TABLE [dbo].[Categories] (
    [CategoryId] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(20)  NOT NULL,
    [AccountId] int  NOT NULL
);
GO

-- Creating table 'PayOptions'
CREATE TABLE [dbo].[PayOptions] (
    [PayOptionId] int IDENTITY(1,1) NOT NULL,
    [Label] nvarchar(20)  NOT NULL,
    [IsPrimary] bit  NOT NULL,
    [WebsiteWebsiteId] int  NOT NULL,
    [BillId] int  NOT NULL
);
GO

-- Creating table 'Websites'
CREATE TABLE [dbo].[Websites] (
    [WebsiteId] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(20)  NOT NULL,
    [Description] nvarchar(max)  NULL,
    [UrlString] nvarchar(max)  NOT NULL,
    [UserName] nvarchar(max)  NULL,
    [Password] nvarchar(max)  NULL,
    [AccountId] int  NOT NULL,
    [AccountWebsite_Website_AccountId] int  NOT NULL
);
GO

-- Creating table 'Payments'
CREATE TABLE [dbo].[Payments] (
    [PaymentId] int IDENTITY(1,1) NOT NULL,
    [Amount] decimal(6,2)  NOT NULL,
    [DatePaid] datetime  NOT NULL,
    [PaymentMonthApplied] datetime  NOT NULL,
    [IsLate] bit  NOT NULL,
    [Comment] nvarchar(max)  NULL,
    [BillId] int  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [AccountId] in table 'Accounts'
ALTER TABLE [dbo].[Accounts]
ADD CONSTRAINT [PK_Accounts]
    PRIMARY KEY CLUSTERED ([AccountId] ASC);
GO

-- Creating primary key on [FavoriteLinkId] in table 'FavoriteLinks'
ALTER TABLE [dbo].[FavoriteLinks]
ADD CONSTRAINT [PK_FavoriteLinks]
    PRIMARY KEY CLUSTERED ([FavoriteLinkId] ASC);
GO

-- Creating primary key on [BillId] in table 'Bills'
ALTER TABLE [dbo].[Bills]
ADD CONSTRAINT [PK_Bills]
    PRIMARY KEY CLUSTERED ([BillId] ASC);
GO

-- Creating primary key on [CategoryId] in table 'Categories'
ALTER TABLE [dbo].[Categories]
ADD CONSTRAINT [PK_Categories]
    PRIMARY KEY CLUSTERED ([CategoryId] ASC);
GO

-- Creating primary key on [PayOptionId] in table 'PayOptions'
ALTER TABLE [dbo].[PayOptions]
ADD CONSTRAINT [PK_PayOptions]
    PRIMARY KEY CLUSTERED ([PayOptionId] ASC);
GO

-- Creating primary key on [WebsiteId] in table 'Websites'
ALTER TABLE [dbo].[Websites]
ADD CONSTRAINT [PK_Websites]
    PRIMARY KEY CLUSTERED ([WebsiteId] ASC);
GO

-- Creating primary key on [PaymentId] in table 'Payments'
ALTER TABLE [dbo].[Payments]
ADD CONSTRAINT [PK_Payments]
    PRIMARY KEY CLUSTERED ([PaymentId] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [Category_CategoryId] in table 'Bills'
ALTER TABLE [dbo].[Bills]
ADD CONSTRAINT [FK_BillCategory]
    FOREIGN KEY ([Category_CategoryId])
    REFERENCES [dbo].[Categories]
        ([CategoryId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_BillCategory'
CREATE INDEX [IX_FK_BillCategory]
ON [dbo].[Bills]
    ([Category_CategoryId]);
GO

-- Creating foreign key on [Website_WebsiteId] in table 'FavoriteLinks'
ALTER TABLE [dbo].[FavoriteLinks]
ADD CONSTRAINT [FK_FavoriteLinkWebsite]
    FOREIGN KEY ([Website_WebsiteId])
    REFERENCES [dbo].[Websites]
        ([WebsiteId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_FavoriteLinkWebsite'
CREATE INDEX [IX_FK_FavoriteLinkWebsite]
ON [dbo].[FavoriteLinks]
    ([Website_WebsiteId]);
GO

-- Creating foreign key on [BillId] in table 'Payments'
ALTER TABLE [dbo].[Payments]
ADD CONSTRAINT [FK_BillPayment]
    FOREIGN KEY ([BillId])
    REFERENCES [dbo].[Bills]
        ([BillId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_BillPayment'
CREATE INDEX [IX_FK_BillPayment]
ON [dbo].[Payments]
    ([BillId]);
GO

-- Creating foreign key on [AccountWebsite_Website_AccountId] in table 'Websites'
ALTER TABLE [dbo].[Websites]
ADD CONSTRAINT [FK_AccountWebsite]
    FOREIGN KEY ([AccountWebsite_Website_AccountId])
    REFERENCES [dbo].[Accounts]
        ([AccountId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_AccountWebsite'
CREATE INDEX [IX_FK_AccountWebsite]
ON [dbo].[Websites]
    ([AccountWebsite_Website_AccountId]);
GO

-- Creating foreign key on [AccountId] in table 'Bills'
ALTER TABLE [dbo].[Bills]
ADD CONSTRAINT [FK_AccountBill]
    FOREIGN KEY ([AccountId])
    REFERENCES [dbo].[Accounts]
        ([AccountId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_AccountBill'
CREATE INDEX [IX_FK_AccountBill]
ON [dbo].[Bills]
    ([AccountId]);
GO

-- Creating foreign key on [AccountId] in table 'Categories'
ALTER TABLE [dbo].[Categories]
ADD CONSTRAINT [FK_AccountCategory]
    FOREIGN KEY ([AccountId])
    REFERENCES [dbo].[Accounts]
        ([AccountId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_AccountCategory'
CREATE INDEX [IX_FK_AccountCategory]
ON [dbo].[Categories]
    ([AccountId]);
GO

-- Creating foreign key on [AccountId] in table 'FavoriteLinks'
ALTER TABLE [dbo].[FavoriteLinks]
ADD CONSTRAINT [FK_AccountFavoriteLink]
    FOREIGN KEY ([AccountId])
    REFERENCES [dbo].[Accounts]
        ([AccountId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_AccountFavoriteLink'
CREATE INDEX [IX_FK_AccountFavoriteLink]
ON [dbo].[FavoriteLinks]
    ([AccountId]);
GO

-- Creating foreign key on [WebsiteWebsiteId] in table 'PayOptions'
ALTER TABLE [dbo].[PayOptions]
ADD CONSTRAINT [FK_PayOptionWebsite]
    FOREIGN KEY ([WebsiteWebsiteId])
    REFERENCES [dbo].[Websites]
        ([WebsiteId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_PayOptionWebsite'
CREATE INDEX [IX_FK_PayOptionWebsite]
ON [dbo].[PayOptions]
    ([WebsiteWebsiteId]);
GO

-- Creating foreign key on [BillId] in table 'PayOptions'
ALTER TABLE [dbo].[PayOptions]
ADD CONSTRAINT [FK_BillPayOption]
    FOREIGN KEY ([BillId])
    REFERENCES [dbo].[Bills]
        ([BillId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_BillPayOption'
CREATE INDEX [IX_FK_BillPayOption]
ON [dbo].[PayOptions]
    ([BillId]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------