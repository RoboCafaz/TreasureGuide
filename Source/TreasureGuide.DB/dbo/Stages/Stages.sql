﻿CREATE TABLE [dbo].[Stages]
(
    [Id] INT NOT NULL,
    [Name] NVARCHAR(128) NULL,
    [Stamina] TINYINT NULL,
    [Type] TINYINT NOT NULL CONSTRAINT [DF_dbo.Stages_Type] DEFAULT 0,
    [Global] BIT NOT NULL CONSTRAINT [DF_dbo.Stages_Global] DEFAULT 1,
    CONSTRAINT [PK_dbo.Stages] PRIMARY KEY CLUSTERED ([Id] ASC)
)
