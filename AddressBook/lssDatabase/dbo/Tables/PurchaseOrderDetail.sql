CREATE TABLE [dbo].[PurchaseOrderDetail] (
    [PurchaseOrderDetailId]     BIGINT          IDENTITY (1, 1) NOT NULL,
    [PurchaseOrderId]           BIGINT          NOT NULL,
    [Amount]                    DECIMAL (18, 4) NULL,
    [OrderedQuantity]           DECIMAL (18, 4) NULL,
    [UnitPrice]                 DECIMAL (18, 4) NULL,
    [UnitOfMeasure]             VARCHAR (10)    NULL,
    [ReceivedDate]              DATE            NULL,
    [ExpectedDeliveryDate]      DATE            NULL,
    [OrderDate]                 DATE            NULL,
    [ReceivedQuantity]          INT             NULL,
    [RemainingQuantity]         INT             NULL,
    [LineDescription]           VARCHAR (255)   NULL,
    [PurchaseOrderDetailNumber] BIGINT          NOT NULL,
    [DiscountAmount]            MONEY           NULL,
    [InvoiceId]                 BIGINT          NULL,
    [LineNumber]                BIGINT          NOT NULL,
    [SupplierId]                BIGINT          NULL,
    [CustomerId]                BIGINT          NULL,
    [ItemId]                    BIGINT          NULL,
    CONSTRAINT [PK_PurchaseOrderDetail] PRIMARY KEY CLUSTERED ([PurchaseOrderDetailId] ASC),
    CONSTRAINT [FK_PurchaseOrderDetail_PurchaseOrder] FOREIGN KEY ([PurchaseOrderId]) REFERENCES [dbo].[PurchaseOrder] ([PurchaseOrderId])
);





