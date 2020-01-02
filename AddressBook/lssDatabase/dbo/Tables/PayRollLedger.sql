CREATE TABLE [dbo].[PayRollLedger] (
    [PayRollLedgerID]        BIGINT       IDENTITY (1, 1) NOT NULL,
    [EmployeeId]             BIGINT       NOT NULL,
    [PayRollTransactionCode] BIGINT       NOT NULL,
    [PayRollType]            VARCHAR (20) NOT NULL,
    [Amount]                 MONEY        NOT NULL,
    [PaidDate]               DATE         NOT NULL,
    [PayPeriodBegin]         DATETIME     NOT NULL,
    [PayPeriodEnd]           DATETIME     NOT NULL,
    [PayRollGroupCode]       INT          NOT NULL,
    [ReversingEntry]         VARCHAR (1)  NULL,
    [UpdateEntry]            VARCHAR (1)  NULL,
    [PayRollLedgerNumber]    BIGINT       NOT NULL,
    [PaySequence]            BIGINT       NOT NULL,
    [TransactionType]        VARCHAR (1)  NOT NULL,
    CONSTRAINT [PK_PayRollLedger] PRIMARY KEY CLUSTERED ([PayRollLedgerID] ASC)
);





