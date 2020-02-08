CREATE TABLE [dbo].[InvoiceDetail] (
    [InvoiceDetailId]       BIGINT          IDENTITY (1, 1) NOT NULL,
    [InvoiceId]             BIGINT          NOT NULL,
    [UnitPrice]             DECIMAL (18, 4) NULL,
    [Quantity]              INT             NULL,
    [UnitOfMeasure]         VARCHAR (10)    NULL,
    [Amount]                DECIMAL (18, 4) NULL,
    [PurchaseOrderDetailId] BIGINT          NULL,
    [SalesOrderDetailId]    BIGINT          NULL,
    [ItemId]                BIGINT          NULL,
    [DiscountPercent]       DECIMAL (18, 4) NULL,
    [DiscountAmount]        DECIMAL (18, 4) NULL,
    [ShipmentDetailId]      BIGINT          NULL,
    [ExtendedDescription]   VARCHAR (255)   NULL,
    [DiscountDueDate]       DATE            NULL,
    [InvoiceDetailNumber]   BIGINT          NOT NULL,
    [PurchaseOrderId]       BIGINT          NULL,
    [CustomerId]            BIGINT          NULL,
    [SupplierId]            BIGINT          NULL,
    CONSTRAINT [PK_InvoiceLineDetail] PRIMARY KEY CLUSTERED ([InvoiceDetailId] ASC),
    CONSTRAINT [FK_InvoicesDetail_Invoices] FOREIGN KEY ([InvoiceId]) REFERENCES [dbo].[Invoice] ([InvoiceId])
);





