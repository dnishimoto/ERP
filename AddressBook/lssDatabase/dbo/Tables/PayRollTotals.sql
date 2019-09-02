CREATE TABLE [dbo].[PayRollTotals] (
    [PayRollTotalsId]          BIGINT       IDENTITY (1, 1) NOT NULL,
    [Employee]                 BIGINT       NOT NULL,
    [EarningCode]              INT          NULL,
    [EarningType]              VARCHAR (10) NULL,
    [DeductionLiabilitiesCode] INT          NULL,
    [DeductionLiabilitiesType] VARCHAR (10) NULL,
    [Amount]                   MONEY        NULL,
    [PayRollGroupCode]         INT          NOT NULL,
    [PaySeqence]               BIGINT       NOT NULL,
    CONSTRAINT [PK_PayRollTotals] PRIMARY KEY CLUSTERED ([PayRollTotalsId] ASC)
);

