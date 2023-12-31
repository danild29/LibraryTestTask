﻿CREATE TABLE [dbo].[Books]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY,
	[Name] NVARCHAR(100) NOT NULL,
	[Author] NVARCHAR(100) NULL,
	[Publisher] NVARCHAR(100) NOT NULL,
	[Year] INT NULL,
	[Genre] NVARCHAR(100) NOT NULL,
	[ISBN] NVARCHAR(32) NOT NULL,
	[Available] bit NOT NULL DEFAULT 1,
	[Status] NVARCHAR(10) NOT NULL DEFAULT 'Free',
)
