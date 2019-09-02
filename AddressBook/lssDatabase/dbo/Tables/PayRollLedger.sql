CREATE TABLE [dbo].[PayRollLedger] (
    [PayrollLedgerID]  BIGINT       IDENTITY (1, 1) NOT NULL,
    [EmployeeId]       BIGINT       NOT NULL,
    [TransactionCode]  BIGINT       NOT NULL,
    [PayRollType]      VARCHAR (20) NOT NULL,
    [Amount]           MONEY        NOT NULL,
    [PaidDate]         DATE         NOT NULL,
    [PayPeriodBegin]   DATETIME     NOT NULL,
    [PayPeriodEnd]     DATETIME     NOT NULL,
    [TransactionType]  VARCHAR (50) NOT NULL,
    [PayRollGroupCode] INT          NOT NULL,
    CONSTRAINT [PK_PayRollLedger] PRIMARY KEY CLUSTERED ([PayrollLedgerID] ASC)
);

