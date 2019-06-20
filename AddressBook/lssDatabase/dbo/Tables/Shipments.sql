﻿CREATE TABLE [dbo].[Shipments] (
    [ShipmentId]            BIGINT          IDENTITY (1, 1) NOT NULL,
    [ShipmentDate]          DATETIME        NULL,
    [CustomerId]            BIGINT          NOT NULL,
    [CarrierId]             BIGINT          NOT NULL,
    [TrackingNumber]        VARCHAR (50)    NULL,
    [ActualWeight]          DECIMAL (18, 4) NULL,
    [BillableWeight]        DECIMAL (18, 4) NULL,
    [Duty]                  DECIMAL (18, 4) NULL,
    [Tax]                   DECIMAL (18, 4) NULL,
    [ShippingCost]          DECIMAL (18, 4) NULL,
    [Amount]                DECIMAL (18, 4) NULL,
    [CodAmount]             DECIMAL (18, 4) NULL,
    [ShippedFromLocationId] BIGINT          NOT NULL,
    [ShippedToLocationId]   BIGINT          NULL,
    [PurchaseOrderId]       BIGINT          NULL,
    [VendorInvoiceId]       BIGINT          NULL,
    [VendorShippingCost]    DECIMAL (18, 4) NULL,
    [VendorHandlingCost]    DECIMAL (18, 4) NULL,
    [OrderNumber]           VARCHAR (20)    NOT NULL,
    [OrderType]             VARCHAR (20)    NOT NULL,
    [WeightUOM]             VARCHAR (20)    NOT NULL,
    CONSTRAINT [PK_Shipments] PRIMARY KEY CLUSTERED ([ShipmentId] ASC),
    CONSTRAINT [FK_Shipments_Customer] FOREIGN KEY ([CustomerId]) REFERENCES [dbo].[Customer] ([CustomerId])
);



