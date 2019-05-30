CREATE TABLE [dbo].[SupplierInvoice] (
    [SupplierInvoiceId]     BIGINT          IDENTITY (1, 1) NOT NULL,
    [SupplierInvoiceNumber] VARCHAR (50)    NULL,
    [SupplierInvoiceDate]   DATE            NULL,
    [PONumber]              VARCHAR (50)    NULL,
    [Amount]                DECIMAL (18, 4) NULL,
    [Description]           VARCHAR (2000)  NULL,
    [TaxAmount]             DECIMAL (18, 4) NULL,
    [PaymentDueDate]        DATE            NULL,
    [PaymentTerms]          VARCHAR (50)    NULL,
    [DiscountDueDate]       DATE            NULL,
    [SupplierId]            BIGINT          NOT NULL,
    [FreightCost]           DECIMAL (18, 4) NULL,
    [DiscountAmount]        DECIMAL (18, 4) NULL,
    CONSTRAINT [PK_SupplierInvoices] PRIMARY KEY CLUSTERED ([SupplierInvoiceId] ASC),
    CONSTRAINT [FK_SupplierInvoice_Supplier] FOREIGN KEY ([SupplierId]) REFERENCES [dbo].[Supplier] ([SupplierId])
);

