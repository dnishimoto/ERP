CREATE TABLE [dbo].[PhoneEntity] (
    [PhoneId]           BIGINT       IDENTITY (1, 1) NOT NULL,
    [PhoneNumber]       VARCHAR (50) NULL,
    [PhoneType]         VARCHAR (10) NULL,
    [Extension]         VARCHAR (10) NULL,
    [AddressId]         BIGINT       NOT NULL,
    [PhoneEntityNumber] BIGINT       NULL,
    CONSTRAINT [PK_Phones] PRIMARY KEY CLUSTERED ([PhoneId] ASC),
    CONSTRAINT [FK_Phones_AddressBook] FOREIGN KEY ([AddressId]) REFERENCES [dbo].[AddressBook] ([AddressId])
);

