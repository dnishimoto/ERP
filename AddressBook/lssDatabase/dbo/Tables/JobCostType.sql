CREATE TABLE [dbo].[JobCostType] (
    [JobCostTypeId]     BIGINT        IDENTITY (1, 1) NOT NULL,
    [CostCode]          VARCHAR (20)  NULL,
    [Description]       VARCHAR (100) NULL,
    [Account]           VARCHAR (50)  NULL,
    [JobCostTypeNumber] BIGINT        NOT NULL,
    CONSTRAINT [PK_JobCostType] PRIMARY KEY CLUSTERED ([JobCostTypeId] ASC)
);

