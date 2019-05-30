CREATE TABLE [dbo].[Emails] (
    [EmailId]    BIGINT       IDENTITY (1, 1) NOT NULL,
    [Password]   VARCHAR (20) NULL,
    [LoginEmail] BIT          NULL,
    [Email]      VARCHAR (30) NOT NULL,
    [AddressId]  BIGINT       NOT NULL,
    CONSTRAINT [PK_Emails] PRIMARY KEY CLUSTERED ([EmailId] ASC),
    CONSTRAINT [FK_Emails_AddressBook] FOREIGN KEY ([AddressId]) REFERENCES [dbo].[AddressBook] ([AddressId])
);

