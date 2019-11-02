CREATE TABLE [dbo].[PayRollGroup] (
    [PayRollGroupId]     BIGINT        IDENTITY (1, 1) NOT NULL,
    [PayRollGroupCode]   INT           NOT NULL,
    [Description]        VARCHAR (200) NULL,
    [PayRollGroupNumber] BIGINT        NOT NULL,
    CONSTRAINT [PK_PayRollGroup] PRIMARY KEY CLUSTERED ([PayRollGroupId] ASC)
);



