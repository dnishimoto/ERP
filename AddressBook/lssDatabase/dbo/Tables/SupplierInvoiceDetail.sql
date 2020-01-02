CREATE TABLE [dbo].[SupplierInvoiceDetail] (
    [SupplierInvoiceDetailId]     BIGINT          IDENTITY (1, 1) NOT NULL,
    [SupplierInvoiceId]           BIGINT          NOT NULL,
    [UnitPrice]                   DECIMAL (18, 4) NULL,
    [Quantity]                    INT             NULL,
    [UnitOfMeasure]               VARCHAR (10)    NULL,
    [ExtendedCost]                DECIMAL (18, 4) NULL,
    [ItemId]                      BIGINT          NOT NULL,
    [Description]                 VARCHAR (255)   NULL,
    [DiscountDueDate]             DATE            NULL,
    [DiscountAmount]              DECIMAL (18, 4) NULL,
    [DiscountPercent]             DECIMAL (18, 4) NULL,
    [SupplierInvoiceDetailNumber] BIGINT          NOT NULL,
    [InvoiceId]                   BIGINT          NULL,
    [InvoiceDetailId]             BIGINT          NULL,
    CONSTRAINT [PK_SupplierInvoiceLineDetail] PRIMARY KEY CLUSTERED ([SupplierInvoiceDetailId] ASC),
    CONSTRAINT [FK_SupplierInvoicesDetail_ItemMaster] FOREIGN KEY ([ItemId]) REFERENCES [dbo].[ItemMaster] ([ItemId]),
    CONSTRAINT [FK_SupplierInvoicesDetail_SupplierInvoices] FOREIGN KEY ([SupplierInvoiceId]) REFERENCES [dbo].[SupplierInvoice] ([SupplierInvoiceId])
);



