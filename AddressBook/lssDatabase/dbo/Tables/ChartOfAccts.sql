﻿CREATE TABLE [dbo].[ChartOfAccts] (
    [AccountId]     BIGINT        IDENTITY (1, 1) NOT NULL,
    [Location]      VARCHAR (10)  NULL,
    [BusUnit]       VARCHAR (10)  NULL,
    [Subsidiary]    VARCHAR (10)  NULL,
    [SubSub]        VARCHAR (10)  NULL,
    [Account]       VARCHAR (30)  NULL,
    [Description]   VARCHAR (255) NULL,
    [CompanyNumber] VARCHAR (10)  NULL,
    [GenCode]       VARCHAR (3)   NULL,
    [SubCode]       VARCHAR (3)   NULL,
    [ObjectNumber]  VARCHAR (20)  NULL,
    [SupCode]       VARCHAR (10)  NULL,
    [ThirdAccount]  VARCHAR (20)  NULL,
    [CategoryCode1] VARCHAR (10)  NULL,
    [CategoryCode2] VARCHAR (10)  NULL,
    [CategoryCode3] VARCHAR (10)  NULL,
    [PostEditCode]  VARCHAR (10)  NULL,
    [CompanyId]     BIGINT        NOT NULL,
    [Level]         INT           NOT NULL,
    CONSTRAINT [PK__chartOfA__349DA5A6F015CCF2] PRIMARY KEY CLUSTERED ([AccountId] ASC),
    CONSTRAINT [FK_ChartOfAccts_Company] FOREIGN KEY ([CompanyId]) REFERENCES [dbo].[Company] ([CompanyId])
);

