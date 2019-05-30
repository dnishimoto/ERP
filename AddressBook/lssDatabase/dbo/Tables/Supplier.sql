CREATE TABLE [dbo].[Supplier] (
    [SupplierId]     BIGINT       IDENTITY (1, 1) NOT NULL,
    [AddressId]      BIGINT       NOT NULL,
    [Identification] VARCHAR (50) NULL,
    PRIMARY KEY CLUSTERED ([SupplierId] ASC),
    CONSTRAINT [FK_Supplier_AddressBook] FOREIGN KEY ([AddressId]) REFERENCES [dbo].[AddressBook] ([AddressId])
);

