
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, and Azure
-- --------------------------------------------------
-- Date Created: 02/19/2014 09:53:47
-- Generated from EDMX file: I:\Mac Files\Desktop\Work\Projects\Home\BillManager\BillManager.Test\BillManagerModel.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [BillManagerDB];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------


-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------


-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Accounts'
CREATE TABLE [dbo].[Accounts] (
    [AccountId] int IDENTITY(1,1) NOT NULL,
    [UserName] nvarchar(max)  NOT NULL,
    [Password] nvarchar(max)  NOT NULL,
    [FirstName] nvarchar(max)  NOT NULL,
    [LastName] nvarchar(max)  NOT NULL,
    [Email] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'Bills'
CREATE TABLE [dbo].[Bills] (
    [BillId] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [Description] nvarchar(max)  NOT NULL,
    [AccountId] int  NOT NULL,
    [IsActive] bit  NOT NULL,
    [Category_CategoryId] int  NOT NULL
);
GO

-- Creating table 'FavoriteLinks'
CREATE TABLE [dbo].[FavoriteLinks] (
    [FavoriteLinkId] int IDENTITY(1,1) NOT NULL,
    [Label] nvarchar(max)  NOT NULL,
    [AccountId] int  NOT NULL,
    [Website_WebsiteId] int  NOT NULL
);
GO

-- Creating table 'Websites'
CREATE TABLE [dbo].[Websites] (
    [WebsiteId] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [Description] nvarchar(max)  NOT NULL,
    [Url] nvarchar(max)  NOT NULL,
    [UserName] nvarchar(max)  NOT NULL,
    [Password] nvarchar(max)  NOT NULL,
    [AccountId] int  NOT NULL
);
GO

-- Creating table 'PayOptions'
CREATE TABLE [dbo].[PayOptions] (
    [PayOptionId] int IDENTITY(1,1) NOT NULL,
    [Label] nvarchar(max)  NOT NULL,
    [IsPrimary] bit  NOT NULL,
    [BillId] int  NOT NULL,
    [Website_WebsiteId] int  NOT NULL
);
GO

-- Creating table 'Categories'
CREATE TABLE [dbo].[Categories] (
    [CategoryId] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [AccountId] int  NOT NULL
);
GO

-- Creating table 'Payments'
CREATE TABLE [dbo].[Payments] (
    [PaymentId] int IDENTITY(1,1) NOT NULL,
    [DatePaid] datetime  NOT NULL,
    [PassedDue] bit  NOT NULL,
    [Comment] nvarchar(max)  NOT NULL,
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

-- Creating primary key on [BillId] in table 'Bills'
ALTER TABLE [dbo].[Bills]
ADD CONSTRAINT [PK_Bills]
    PRIMARY KEY CLUSTERED ([BillId] ASC);
GO

-- Creating primary key on [FavoriteLinkId] in table 'FavoriteLinks'
ALTER TABLE [dbo].[FavoriteLinks]
ADD CONSTRAINT [PK_FavoriteLinks]
    PRIMARY KEY CLUSTERED ([FavoriteLinkId] ASC);
GO

-- Creating primary key on [WebsiteId] in table 'Websites'
ALTER TABLE [dbo].[Websites]
ADD CONSTRAINT [PK_Websites]
    PRIMARY KEY CLUSTERED ([WebsiteId] ASC);
GO

-- Creating primary key on [PayOptionId] in table 'PayOptions'
ALTER TABLE [dbo].[PayOptions]
ADD CONSTRAINT [PK_PayOptions]
    PRIMARY KEY CLUSTERED ([PayOptionId] ASC);
GO

-- Creating primary key on [CategoryId] in table 'Categories'
ALTER TABLE [dbo].[Categories]
ADD CONSTRAINT [PK_Categories]
    PRIMARY KEY CLUSTERED ([CategoryId] ASC);
GO

-- Creating primary key on [PaymentId] in table 'Payments'
ALTER TABLE [dbo].[Payments]
ADD CONSTRAINT [PK_Payments]
    PRIMARY KEY CLUSTERED ([PaymentId] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

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

-- Creating foreign key on [AccountId] in table 'Websites'
ALTER TABLE [dbo].[Websites]
ADD CONSTRAINT [FK_AccountWebsite]
    FOREIGN KEY ([AccountId])
    REFERENCES [dbo].[Accounts]
        ([AccountId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_AccountWebsite'
CREATE INDEX [IX_FK_AccountWebsite]
ON [dbo].[Websites]
    ([AccountId]);
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

-- Creating foreign key on [Website_WebsiteId] in table 'PayOptions'
ALTER TABLE [dbo].[PayOptions]
ADD CONSTRAINT [FK_PayOptionWebsite]
    FOREIGN KEY ([Website_WebsiteId])
    REFERENCES [dbo].[Websites]
        ([WebsiteId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;

-- Creating non-clustered index for FOREIGN KEY 'FK_PayOptionWebsite'
CREATE INDEX [IX_FK_PayOptionWebsite]
ON [dbo].[PayOptions]
    ([Website_WebsiteId]);
GO

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

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------