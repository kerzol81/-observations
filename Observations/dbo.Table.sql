﻿CREATE TABLE [dbo].[observations]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Name] VARCHAR(64) NOT NULL,
	[Seen] DATETIME NOT NULL,
	[InSolarSystem] BIT NOT NULL,
)
