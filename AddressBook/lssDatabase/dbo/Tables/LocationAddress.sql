CREATE TABLE [dbo].[LocationAddress] (
    [LocationAddressId]     BIGINT        IDENTITY (1, 1) NOT NULL,
    [Address Line 1]        VARCHAR (255) NULL,
    [Address Line 2]        VARCHAR (255) NULL,
    [City]                  VARCHAR (50)  NULL,
    [Zipcode]               VARCHAR (20)  NULL,
    [TypeXRefId]            BIGINT        NOT NULL,
    [AddressId]             BIGINT        NOT NULL,
    [State]                 NCHAR (2)     NULL,
    [Country]               VARCHAR (50)  NULL,
    [Type]                  VARCHAR (20)  NULL,
    [LocationAddressNumber] BIGINT        NULL,
    CONSTRAINT [PK_LocationAddress] PRIMARY KEY CLUSTERED ([LocationAddressId] ASC),
    CONSTRAINT [FK_LocationAddress_AddressBook] FOREIGN KEY ([AddressId]) REFERENCES [dbo].[AddressBook] ([AddressId]),
    CONSTRAINT [FK_LocationAddress_UDCType] FOREIGN KEY ([TypeXRefId]) REFERENCES [dbo].[UDC] ([XRefId])
);



