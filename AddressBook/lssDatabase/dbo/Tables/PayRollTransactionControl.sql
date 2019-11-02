CREATE TABLE [dbo].[PayRollTransactionControl] (
    [PayRollTransactionControlId]     BIGINT        IDENTITY (1, 1) NOT NULL,
    [Description]                     VARCHAR (MAX) NULL,
    [CompanyCode]                     VARCHAR (10)  NULL,
    [PayRollType]                     VARCHAR (20)  NULL,
    [RateAmount]                      MONEY         NULL,
    [RateType]                        VARCHAR (50)  NULL,
    [PayRollTransactionCode]          INT           NOT NULL,
    [UpperLimit1]                     MONEY         NULL,
    [UpperLimit2]                     MONEY         NULL,
    [PayRollTransactionControlNumber] BIGINT        NULL,
    CONSTRAINT [PK_PayRollBenefit] PRIMARY KEY CLUSTERED ([PayRollTransactionControlId] ASC)
);



