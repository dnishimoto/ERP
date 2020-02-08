CREATE TABLE [dbo].[AccountReceivableDetail] (
    [AccountReceivableDetailId]     BIGINT          IDENTITY (1, 1) NOT NULL,
    [InvoiceId]                     BIGINT          NOT NULL,
    [AccountReceivableId]           BIGINT          NOT NULL,
    [UnitPrice]                     DECIMAL (18, 4) NULL,
    [Quantity]                      INT             NULL,
    [UnitOfMeasure]                 VARCHAR (10)    NULL,
    [Amount]                        DECIMAL (18, 4) NULL,
    [AmountReceived]                DECIMAL (18, 4) NULL,
    [PurchaseOrderDetailId]         BIGINT          NULL,
    [SalesOrderDetailId]            BIGINT          NULL,
    [ItemId]                        BIGINT          NULL,
    [AccountReceivableDetailNumber] BIGINT          NOT NULL,
    [PurchaseOrderId]               BIGINT          NULL,
    [CustomerId]                    BIGINT          NULL,
    [SupplierId]                    BIGINT          NULL,
    [QuantityDelivered]             BIGINT          NULL,
    [Comment]                       VARCHAR (255)   NULL,
    [TypeOfPayment]                 VARCHAR (20)    NULL,
    [InvoiceDetailId]               BIGINT          NOT NULL,
    CONSTRAINT [PK_AccountReceivableDetail] PRIMARY KEY CLUSTERED ([AccountReceivableDetailId] ASC),
    CONSTRAINT [FK_AccountReceivableDetail_AccountReceivable] FOREIGN KEY ([AccountReceivableId]) REFERENCES [dbo].[AccountReceivable] ([AccountReceivableId])
);

