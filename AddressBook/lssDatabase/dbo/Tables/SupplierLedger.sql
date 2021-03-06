﻿CREATE TABLE [dbo].[SupplierLedger] (
    [SupplierLedgerId]     BIGINT        IDENTITY (1, 1) NOT NULL,
    [SupplierId]           BIGINT        NOT NULL,
    [InvoiceId]            BIGINT        NOT NULL,
    [AcctPayId]            BIGINT        NOT NULL,
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
    [SupplierLedgerNumber] BIGINT        NOT NULL,
    CONSTRAINT [PK_SupplierLedger] PRIMARY KEY CLUSTERED ([SupplierLedgerId] ASC),
    CONSTRAINT [FK_SupplierLedger_AcctPay] FOREIGN KEY ([AcctPayId]) REFERENCES [dbo].[AccountPayable] ([AccountPayableId]),
    CONSTRAINT [FK_SupplierLedger_GeneralLedger] FOREIGN KEY ([GeneralLedgerId]) REFERENCES [dbo].[GeneralLedger] ([GeneralLedgerId]),
    CONSTRAINT [FK_SupplierLedger_Supplier] FOREIGN KEY ([SupplierId]) REFERENCES [dbo].[Supplier] ([SupplierId]),
    CONSTRAINT [FK_SupplierLedger_SupplierInvoice] FOREIGN KEY ([InvoiceId]) REFERENCES [dbo].[SupplierInvoice] ([SupplierInvoiceId])
);





