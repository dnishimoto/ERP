CREATE TABLE [dbo].[Equipment] (
    [EquipmentId]           BIGINT        IDENTITY (1, 1) NOT NULL,
    [Model]                 VARCHAR (100) NULL,
    [Make]                  VARCHAR (50)  NULL,
    [VIN]                   VARCHAR (200) NULL,
    [PurchasePrice]         MONEY         NULL,
    [CurrentAppraisalPrice] MONEY         NULL,
    [SalesPrice]            MONEY         NULL,
    [Description]           VARCHAR (MAX) NULL,
    [SaleOption]            VARCHAR (20)  NULL,
    [YearPurchased]         INT           NULL,
    [LocationCity]          VARCHAR (100) NULL,
    [LocationState]         VARCHAR (2)   NULL,
    [Category1]             VARCHAR (20)  NULL,
    [Category2]             VARCHAR (20)  NULL,
    [Category3]             VARCHAR (20)  NULL,
    CONSTRAINT [PK_Equipment] PRIMARY KEY CLUSTERED ([EquipmentId] ASC)
);

