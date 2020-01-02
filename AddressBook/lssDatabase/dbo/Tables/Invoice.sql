CREATE TABLE [dbo].[Invoice] (
    [InvoiceId]       BIGINT          IDENTITY (1, 1) NOT NULL,
    [InvoiceDocument] VARCHAR (20)    NULL,
    [InvoiceDate]     DATE            NULL,
    [Amount]          DECIMAL (18, 4) NULL,
    [CustomerId]      BIGINT          NULL,
    [Description]     VARCHAR (2000)  NULL,
    [TaxAmount]       DECIMAL (18, 4) NULL,
    [PaymentDueDate]  DATE            NULL,
    [PaymentTerms]    VARCHAR (50)    NULL,
    [CompanyId]       BIGINT          NOT NULL,
    [DiscountDueDate] DATE            NULL,
    [FreightCost]     DECIMAL (18, 4) NULL,
    [DiscountAmount]  DECIMAL (18, 4) NULL,
    [InvoiceNumber]   BIGINT          NOT NULL,
    [PurchaseOrderId] BIGINT          NULL,
    [SupplierId]      BIGINT          NULL,
    CONSTRAINT [PK_Invoices] PRIMARY KEY CLUSTERED ([InvoiceId] ASC),
    CONSTRAINT [FK_Invoice_Company] FOREIGN KEY ([CompanyId]) REFERENCES [dbo].[Company] ([CompanyId]),
    CONSTRAINT [FK_Invoices_Customer] FOREIGN KEY ([CustomerId]) REFERENCES [dbo].[Customer] ([CustomerId])
);



