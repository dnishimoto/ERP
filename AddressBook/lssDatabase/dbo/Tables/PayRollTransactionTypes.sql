CREATE TABLE [dbo].[PayRollTransactionTypes] (
    [PayRollTransactionId]  BIGINT        IDENTITY (1, 1) NOT NULL,
    [PayRollTranactionCode] INT           NOT NULL,
    [Description]           VARCHAR (MAX) NOT NULL,
    CONSTRAINT [PK_PayRollTransactionControl] PRIMARY KEY CLUSTERED ([PayRollTransactionId] ASC)
);

