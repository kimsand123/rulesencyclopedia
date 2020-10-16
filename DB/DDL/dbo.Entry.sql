CREATE TABLE [dbo].[Entry] (
    [Id]              INT            IDENTITY (1, 1) NOT NULL,
    [ParagraphNumber] VARCHAR (10)   NOT NULL,
    [Revisions]       INT            NOT NULL,
    [Text]            NVARCHAR (MAX) NOT NULL,
    [Type]            INT            NOT NULL,
    [Editor]          VARCHAR (50)   NULL,
	[TOCId] INT NOT NULL FOREIGN KEY REFERENCES dbo.TOC([Id]), 
    CONSTRAINT [PK_Entry] PRIMARY KEY CLUSTERED ([Id] ASC)
);

