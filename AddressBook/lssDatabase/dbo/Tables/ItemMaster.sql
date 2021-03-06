﻿CREATE TABLE [dbo].[ItemMaster] (
    [ItemId]              BIGINT          IDENTITY (1, 1) NOT NULL,
    [Description]         VARCHAR (255)   NULL,
    [UnitOfMeasure]       VARCHAR (20)    NULL,
    [CommodityCode]       VARCHAR (10)    NULL,
    [Description2]        VARCHAR (255)   NULL,
    [ItemCode]            VARCHAR (20)    NOT NULL,
    [UnitPrice]           MONEY           NULL,
    [Branch]              VARCHAR (50)    NULL,
    [Weight]              DECIMAL (18, 4) NULL,
    [WeightUnitOfMeasure] VARCHAR (50)    NULL,
    [Volume]              DECIMAL (18, 4) NULL,
    [VolumeUnitOfMeasure] VARCHAR (50)    NULL,
    [ItemMasterNumber]    BIGINT          NOT NULL,
    [AccountId]           BIGINT          NULL,
    CONSTRAINT [PK_ItemMaster] PRIMARY KEY CLUSTERED ([ItemId] ASC)
);







