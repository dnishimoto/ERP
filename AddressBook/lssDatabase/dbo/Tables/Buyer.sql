CREATE TABLE [dbo].[Buyer] (
    [BuyerId]     BIGINT        IDENTITY (1, 1) NOT NULL,
    [AddressId]   BIGINT        NOT NULL,
    [Title]       VARCHAR (100) NULL,
    [BuyerNumber] BIGINT        NOT NULL,
    CONSTRAINT [PK_Buyer] PRIMARY KEY CLUSTERED ([BuyerId] ASC),
    CONSTRAINT [FK_Buyer_AddressBook] FOREIGN KEY ([AddressId]) REFERENCES [dbo].[AddressBook] ([AddressId])
);



