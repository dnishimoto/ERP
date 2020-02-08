CREATE TABLE [dbo].[AccountPayableDetail] (
    [AccountPayableDetailId]     BIGINT          IDENTITY (1, 1) NOT NULL,
    [InvoiceId]                  BIGINT          NOT NULL,
    [InvoiceDetailId]            BIGINT          NOT NULL,
    [UnitPrice]                  DECIMAL (18, 4) NULL,
    [Quantity]                   INT             NULL,
    [QuantityReceived]           INT             NULL,
    [Amount]                     DECIMAL (18, 4) NULL,
    [AmountPaid]                 DECIMAL (18, 4) NULL,
    [PurchaseOrderDetailId]      BIGINT          NULL,
    [SalesOrderDetailId]         BIGINT          NULL,
    [ItemId]                     BIGINT          NULL,
    [ExtendedDescription]        VARCHAR (255)   NULL,
    [AccountPayableDetailNumber] BIGINT          NOT NULL,
    [PurchaseOrderId]            BIGINT          NULL,
    [CustomerId]                 BIGINT          NULL,
    [SupplierId]                 BIGINT          NULL,
    [AccountPayableId]           BIGINT          NULL,
    CONSTRAINT [PK_AccountPayableDetail] PRIMARY KEY CLUSTERED ([AccountPayableDetailId] ASC),
    CONSTRAINT [FK_AccountPayableDetail_AccountPayable] FOREIGN KEY ([AccountPayableId]) REFERENCES [dbo].[AccountPayable] ([AccountPayableId])
);

