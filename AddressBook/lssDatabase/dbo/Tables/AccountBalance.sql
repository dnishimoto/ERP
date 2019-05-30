CREATE TABLE [dbo].[AccountBalance] (
    [AccountBalanceId]   BIGINT          IDENTITY (1, 1) NOT NULL,
    [AccountBalanceType] VARCHAR (10)    NULL,
    [Amount]             MONEY           NULL,
    [Hours]              DECIMAL (18, 2) NULL,
    [FiscalYear]         INT             NOT NULL,
    [FiscalPeriod]       INT             NOT NULL,
    [AccountId]          BIGINT          NOT NULL,
    CONSTRAINT [PK_AccountBalance] PRIMARY KEY CLUSTERED ([AccountBalanceId] ASC),
    CONSTRAINT [FK_AccountBalance_ChartOfAccts] FOREIGN KEY ([AccountId]) REFERENCES [dbo].[ChartOfAccts] ([AccountId])
);

