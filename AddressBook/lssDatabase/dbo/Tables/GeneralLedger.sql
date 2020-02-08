CREATE TABLE [dbo].[GeneralLedger] (
    [GeneralLedgerId]     BIGINT          IDENTITY (1, 1) NOT NULL,
    [DocNumber]           BIGINT          NOT NULL,
    [DocType]             VARCHAR (10)    NOT NULL,
    [Amount]              MONEY           NOT NULL,
    [LedgerType]          VARCHAR (10)    NOT NULL,
    [GLDate]              DATETIME        NOT NULL,
    [AccountId]           BIGINT          NOT NULL,
    [CreatedDate]         DATETIME        NOT NULL,
    [AddressId]           BIGINT          NOT NULL,
    [Comment]             VARCHAR (255)   NULL,
    [DebitAmount]         MONEY           NULL,
    [CreditAmount]        MONEY           NULL,
    [FiscalYear]          INT             NULL,
    [FiscalPeriod]        INT             NULL,
    [CheckNumber]         VARCHAR (50)    NULL,
    [PurchaseOrderNumber] VARCHAR (50)    NULL,
    [Units]               DECIMAL (18, 4) NULL,
    [GeneralLedgerNumber] BIGINT          NOT NULL,
    CONSTRAINT [PK__generalL__3214EC07AC773B83] PRIMARY KEY CLUSTERED ([GeneralLedgerId] ASC),
    CONSTRAINT [FK_GeneralLedger_AddressBook] FOREIGN KEY ([AddressId]) REFERENCES [dbo].[AddressBook] ([AddressId]),
    CONSTRAINT [FK_GeneralLedger_ChartOfAccts] FOREIGN KEY ([AccountId]) REFERENCES [dbo].[ChartOfAccount] ([AccountId])
);





