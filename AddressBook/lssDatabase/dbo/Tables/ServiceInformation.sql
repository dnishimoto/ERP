CREATE TABLE [dbo].[ServiceInformation] (
    [ServiceId]                BIGINT         IDENTITY (1, 1) NOT NULL,
    [ServiceDescription]       VARCHAR (255)  NULL,
    [Price]                    MONEY          NULL,
    [AddOns]                   VARCHAR (1000) NULL,
    [ServiceTypeXRefId]        BIGINT         NULL,
    [CreatedDate]              DATETIME       NULL,
    [LocationId]               BIGINT         NOT NULL,
    [CustomerId]               BIGINT         NOT NULL,
    [ContractId]               BIGINT         NOT NULL,
    [SquareFeetOfStructure]    INT            NULL,
    [LocationDescription]      VARCHAR (255)  NULL,
    [LocationGPS]              VARCHAR (255)  NULL,
    [Comments]                 VARCHAR (1000) NULL,
    [Status]                   BIT            NOT NULL,
    [ServiceInformationNumber] BIGINT         NOT NULL,
    CONSTRAINT [PK__ServiceI__C51BB00AAC530496] PRIMARY KEY CLUSTERED ([ServiceId] ASC),
    CONSTRAINT [FK__ServiceIn__Contr__33BFA6FF] FOREIGN KEY ([ContractId]) REFERENCES [dbo].[Contract] ([ContractId]),
    CONSTRAINT [FK__ServiceIn__Custo__2A363CC5] FOREIGN KEY ([CustomerId]) REFERENCES [dbo].[Customer] ([CustomerId]),
    CONSTRAINT [FK__ServiceIn__Locat__39788055] FOREIGN KEY ([LocationId]) REFERENCES [dbo].[LocationAddress] ([LocationAddressId]),
    CONSTRAINT [FK__ServiceIn__Servi__22951AFD] FOREIGN KEY ([ServiceTypeXRefId]) REFERENCES [dbo].[UDC] ([XRefId])
);





