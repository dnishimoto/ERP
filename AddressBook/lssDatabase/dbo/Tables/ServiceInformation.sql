CREATE TABLE [dbo].[ServiceInformation] (
    [ServiceId]             BIGINT         IDENTITY (1, 1) NOT NULL,
    [ServiceDescription]    VARCHAR (255)  NULL,
    [Price]                 MONEY          NULL,
    [AddOns]                VARCHAR (1000) NULL,
    [ServiceTypeXRefId]     BIGINT         NULL,
    [CreatedDate]           DATETIME       NULL,
    [LocationId]            BIGINT         NOT NULL,
    [CustomerId]            BIGINT         NOT NULL,
    [ContractId]            BIGINT         NOT NULL,
    [SquareFeetOfStructure] INT            NULL,
    [LocationDescription]   VARCHAR (255)  NULL,
    [LocationGPS]           VARCHAR (255)  NULL,
    [Comments]              VARCHAR (1000) NULL,
    [Status]                BIT            NOT NULL,
    PRIMARY KEY CLUSTERED ([ServiceId] ASC),
    FOREIGN KEY ([ContractId]) REFERENCES [dbo].[Contract] ([ContractId]),
    FOREIGN KEY ([CustomerId]) REFERENCES [dbo].[Customer] ([CustomerId]),
    FOREIGN KEY ([LocationId]) REFERENCES [dbo].[LocationAddress] ([LocationId]),
    FOREIGN KEY ([ServiceTypeXRefId]) REFERENCES [dbo].[UDC] ([XRefId])
);

