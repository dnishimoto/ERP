CREATE TABLE [dbo].[Equations] (
    [Id]        BIGINT        IDENTITY (1, 1) NOT NULL,
    [equation]  VARCHAR (255) NULL,
    [queueid]   VARCHAR (20)  NULL,
    [evaluated] VARCHAR (255) NULL,
    [cellname]  VARCHAR (10)  NULL,
    CONSTRAINT [PK_equations] PRIMARY KEY CLUSTERED ([Id] ASC)
);

