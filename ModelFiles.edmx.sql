
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 09/24/2015 22:47:59
-- Generated from EDMX file: C:\Users\Manu\Downloads\FastDirectoryEnumerator_src\ModelFiles.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [dbFile];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------


-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FilesMetadatas]', 'U') IS NOT NULL
    DROP TABLE [dbo].[FilesMetadatas];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'FilesMetadatas'
CREATE TABLE [dbo].[FilesMetadatas] (
    [Path] varchar(500)  NOT NULL,
    [Size] bigint  NULL,
    [DateCreated] datetime  NULL,
    [DateLastAccessed] datetime  NULL,
    [DateLastModified] datetime  NULL,
    [Md5Hash] nvarchar(max)  NULL,
    [FileName] nvarchar(max)  NOT NULL,
    [FileExtension] nvarchar(max)  NOT NULL,
    [Depth] int  NOT NULL,
    [Attributes] nvarchar(max)  NULL,
    [PathLength] int  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Path] in table 'FilesMetadatas'
ALTER TABLE [dbo].[FilesMetadatas]
ADD CONSTRAINT [PK_FilesMetadatas]
    PRIMARY KEY CLUSTERED ([Path] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------