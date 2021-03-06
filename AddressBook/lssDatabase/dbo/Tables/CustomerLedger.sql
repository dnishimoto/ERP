﻿CREATE TABLE [dbo].[CustomerLedger] (
    [CustomerLedgerId]     BIGINT        IDENTITY (1, 1) NOT NULL,
    [CustomerId]           BIGINT        NOT NULL,
    [InvoiceId]            BIGINT        NOT NULL,
    [AccountReceivableId]  BIGINT        NOT NULL,
    [Amount]               MONEY         NULL,
    [GLDate]               DATE          NULL,
    [AccountId]            BIGINT        NOT NULL,
    [GeneralLedgerId]      BIGINT        NOT NULL,
    [DocNumber]            BIGINT        NOT NULL,
    [Comment]              VARCHAR (255) NULL,
    [AddressId]            BIGINT        NOT NULL,
    [CreatedDate]          DATETIME      NULL,
    [DocType]              VARCHAR (50)  NOT NULL,
    [DebitAmount]          MONEY         NULL,
    [CreditAmount]         MONEY         NULL,
    [FiscalYear]           INT           NOT NULL,
    [FiscalPeriod]         INT           NOT NULL,
    [CheckNumber]          VARCHAR (20)  NULL,
    [CustomerLedgerNumber] BIGINT        NOT NULL,
    CONSTRAINT [PK_CustomerLedger] PRIMARY KEY CLUSTERED ([CustomerLedgerId] ASC),
    CONSTRAINT [FK_CustomerLedger_AcctRec] FOREIGN KEY ([AccountReceivableId]) REFERENCES [dbo].[AccountReceivable] ([AccountReceivableId]),
    CONSTRAINT [FK_CustomerLedger_Customer] FOREIGN KEY ([CustomerId]) REFERENCES [dbo].[Customer] ([CustomerId]),
    CONSTRAINT [FK_CustomerLedger_GeneralLedger] FOREIGN KEY ([GeneralLedgerId]) REFERENCES [dbo].[GeneralLedger] ([GeneralLedgerId]),
    CONSTRAINT [FK_CustomerLedger_Invoice] FOREIGN KEY ([InvoiceId]) REFERENCES [dbo].[Invoice] ([InvoiceId])
);





