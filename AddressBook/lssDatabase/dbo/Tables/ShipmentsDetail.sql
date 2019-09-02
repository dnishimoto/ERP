CREATE TABLE [dbo].[ShipmentsDetail] (
    [ShipmentDetailId]     BIGINT          IDENTITY (1, 1) NOT NULL,
    [ShipmentId]           BIGINT          NOT NULL,
    [ItemId]               BIGINT          NOT NULL,
    [Quantity]             BIGINT          NULL,
    [Amount]               MONEY           NULL,
    [SalesOrderDetailId]   BIGINT          NOT NULL,
    [InvoiceDetailId]      BIGINT          NULL,
    [ShipmentDetailNumber] BIGINT          NOT NULL,
    [QuantityShipped]      BIGINT          NULL,
    [AmountShipped]        MONEY           NULL,
    [Note]                 VARCHAR (MAX)   NULL,
    [UnitPrice]            MONEY           NULL,
    [Weight]               DECIMAL (18, 4) NULL,
    [WeightUnitOfMeasure]  VARCHAR (50)    NULL,
    [Volume]               DECIMAL (18, 4) NULL,
    [VolumeUnitOfMeasure]  VARCHAR (50)    NULL,
    [ShippedDate]          DATETIME        NULL,
    CONSTRAINT [PK_ShipmentDetail] PRIMARY KEY CLUSTERED ([ShipmentDetailId] ASC),
    CONSTRAINT [FK_ShipmentsDetail_ItemMaster] FOREIGN KEY ([ItemId]) REFERENCES [dbo].[ItemMaster] ([ItemId]),
    CONSTRAINT [FK_ShipmentsDetail_Shipments] FOREIGN KEY ([ShipmentId]) REFERENCES [dbo].[Shipments] ([ShipmentId])
);





