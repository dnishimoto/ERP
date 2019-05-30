CREATE TABLE [dbo].[ShippedToAddresses] (
    [ShippedToAddressId] BIGINT        IDENTITY (1, 1) NOT NULL,
    [AddressId]          BIGINT        NOT NULL,
    [ShipToAddressLine1] VARCHAR (100) NULL,
    [ShipToAddressLine2] VARCHAR (100) NULL,
    [ShipToState]        VARCHAR (50)  NULL,
    [ShipToCity]         VARCHAR (50)  NULL,
    [ShipToZipcode]      VARCHAR (50)  NULL,
    CONSTRAINT [PK_ShipToAddresses] PRIMARY KEY CLUSTERED ([ShippedToAddressId] ASC)
);

