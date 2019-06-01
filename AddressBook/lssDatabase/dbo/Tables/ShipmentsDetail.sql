CREATE TABLE [dbo].[ShipmentsDetail] (
    [ShipmentDetailId]   BIGINT          NOT NULL,
    [ShipmentId]         BIGINT          NOT NULL,
    [ItemId]             BIGINT          NOT NULL,
    [Quantity]           INT             NULL,
    [Amount]             DECIMAL (18, 4) NULL,
    [SalesOrderDetailId] BIGINT          NOT NULL,
    [InvoiceDetailId] BIGINT NULL, 
    CONSTRAINT [PK_ShipmentDetail] PRIMARY KEY CLUSTERED ([ShipmentDetailId] ASC),
    CONSTRAINT [FK_ShipmentsDetail_ItemMaster] FOREIGN KEY ([ItemId]) REFERENCES [dbo].[ItemMaster] ([ItemId]),
    CONSTRAINT [FK_ShipmentsDetail_Shipments] FOREIGN KEY ([ShipmentId]) REFERENCES [dbo].[Shipments] ([ShipmentId])
);

