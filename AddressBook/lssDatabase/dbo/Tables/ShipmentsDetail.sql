CREATE TABLE [dbo].[ShipmentsDetail] (
    [ShipmentDetailId]     BIGINT        IDENTITY (1, 1) NOT NULL,
    [ShipmentId]           BIGINT        NOT NULL,
    [ItemId]               BIGINT        NOT NULL,
    [Quantity]             BIGINT        NULL,
    [Amount]               MONEY         NULL,
    [SalesOrderDetailId]   BIGINT        NOT NULL,
    [InvoiceDetailId]      BIGINT        NULL,
    [ShipmentDetailNumber] BIGINT        NOT NULL,
    [QuantityShipped]      BIGINT        NULL,
    [AmountShipped]        MONEY         NULL,
    [Note]                 VARCHAR (MAX) NULL,
    CONSTRAINT [PK_ShipmentDetail] PRIMARY KEY CLUSTERED ([ShipmentDetailId] ASC),
    CONSTRAINT [FK_ShipmentsDetail_ItemMaster] FOREIGN KEY ([ItemId]) REFERENCES [dbo].[ItemMaster] ([ItemId]),
    CONSTRAINT [FK_ShipmentsDetail_Shipments] FOREIGN KEY ([ShipmentId]) REFERENCES [dbo].[Shipments] ([ShipmentId])
);



