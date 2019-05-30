CREATE TABLE [dbo].[Inventory] (
    [InventoryId]           BIGINT         IDENTITY (1, 1) NOT NULL,
    [Description]           VARCHAR (100)  NULL,
    [Remarks]               VARCHAR (2000) NULL,
    [UnitOfMeasure]         VARCHAR (100)  NULL,
    [Quantity]              INT            NULL,
    [ExtendedPrice]         MONEY          NULL,
    [DistributionAccountId] BIGINT         NULL,
    [PackingSlipDetailId]   BIGINT         NULL,
    [ItemId]                BIGINT         NOT NULL,
    [Branch]                VARCHAR (50)   NULL,
    [InventoryNumber]       BIGINT         NOT NULL,
    CONSTRAINT [PK_Inventory] PRIMARY KEY CLUSTERED ([InventoryId] ASC),
    CONSTRAINT [FK_Inventory_ItemMaster] FOREIGN KEY ([ItemId]) REFERENCES [dbo].[ItemMaster] ([ItemId])
);

