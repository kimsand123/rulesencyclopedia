CREATE TABLE [dbo].[TOC] (
    [Id]        INT           IDENTITY (1, 1) NOT NULL,
    [Text]      VARCHAR (MAX) NOT NULL,
    [Revisions] INT           NOT NULL,
    [Editor]    VARCHAR (50)  NULL,
    [Game]   INT           NULL,
    CONSTRAINT [PK_TOC] PRIMARY KEY CLUSTERED ([Id] ASC),
    FOREIGN KEY ([Game]) REFERENCES [dbo].[Game] ([Id])
);

