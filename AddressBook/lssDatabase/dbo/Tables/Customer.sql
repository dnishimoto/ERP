CREATE TABLE [dbo].[Customer] (
    [CustomerId]                        BIGINT       IDENTITY (1, 1) NOT NULL,
    [AddressId]                         BIGINT       NOT NULL,
    [PrimaryShippedToLocationAddressId] BIGINT       NULL,
    [PrimaryEmailId]                    BIGINT       NULL,
    [PrimaryPhoneId]                    BIGINT       NULL,
    [MailingLocationAddressId]          BIGINT       NULL,
    [PrimaryBillingLocationAddressId]   BIGINT       NULL,
    [TaxIdentification]                 VARCHAR (50) NULL,
    [CustomerNumber]                    BIGINT       NULL,
    CONSTRAINT [PK__Customer__091C2AFB7C8C5421] PRIMARY KEY CLUSTERED ([CustomerId] ASC),
    CONSTRAINT [FK_Customer_AddressBook] FOREIGN KEY ([AddressId]) REFERENCES [dbo].[AddressBook] ([AddressId])
);





