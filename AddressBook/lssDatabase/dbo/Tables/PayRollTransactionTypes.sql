CREATE TABLE [dbo].[PayRollTransactionTypes] (
    [PayRollTransactionTypesId]     BIGINT        IDENTITY (1, 1) NOT NULL,
    [PayRollTranactionCode]         INT           NOT NULL,
    [Description]                   VARCHAR (255) NOT NULL,
    [PayRollTransactionTypesNumber] BIGINT        NOT NULL,
    CONSTRAINT [PK_PayRollTransactionControl] PRIMARY KEY CLUSTERED ([PayRollTransactionTypesId] ASC)
);



