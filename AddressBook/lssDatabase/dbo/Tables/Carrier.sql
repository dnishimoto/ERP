CREATE TABLE [dbo].[Carrier] (
    [CarrierId]         BIGINT IDENTITY (1, 1) NOT NULL,
    [AddressId]         BIGINT NOT NULL,
    [CarrierTypeXrefId] BIGINT NOT NULL,
    [CarrierNumber]     BIGINT NOT NULL,
    CONSTRAINT [PK_Carier] PRIMARY KEY CLUSTERED ([CarrierId] ASC),
    CONSTRAINT [FK__Carrier__Carrier__668030F6] FOREIGN KEY ([CarrierTypeXrefId]) REFERENCES [dbo].[UDC] ([XRefId]),
    CONSTRAINT [FK_Carier_AddressBook] FOREIGN KEY ([AddressId]) REFERENCES [dbo].[AddressBook] ([AddressId])
);



