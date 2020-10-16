CREATE TABLE [dbo].[Game] (
    [Id]       INT           IDENTITY (1, 1) NOT NULL,
    [Name]     VARCHAR (100) NOT NULL,
    [Company]  VARCHAR (100) NOT NULL,
    [Revision] INT           NOT NULL,
    [Editor]   VARCHAR (50)  NULL,
    CONSTRAINT [PK_Game] PRIMARY KEY CLUSTERED ([Id] ASC)
);

