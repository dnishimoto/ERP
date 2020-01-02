CREATE TABLE [dbo].[ContractItem] (
    [ContractItemId]     BIGINT         IDENTITY (1, 1) NOT NULL,
    [ContractId]         BIGINT         NOT NULL,
    [WBS]                VARCHAR (50)   NULL,
    [ItemDescription]    VARCHAR (2000) NULL,
    [UnitOfMeasure]      VARCHAR (50)   NULL,
    [Quantity]           INT            NULL,
    [UnitPrice]          MONEY          NULL,
    [ExtendedCost]       MONEY          NULL,
    [Fees]               MONEY          NULL,
    [ContractType]       VARCHAR (10)   NULL,
    [PaymentMethod]      VARCHAR (50)   NULL,
    [DurationHours]      INT            NULL,
    [EstimatedStartDate] DATE           NULL,
    [EstimatedEndDate]   DATE           NULL,
    [ContractItemNumber] BIGINT         NOT NULL,
    CONSTRAINT [PK_ContractContent] PRIMARY KEY CLUSTERED ([ContractItemId] ASC),
    CONSTRAINT [FK_ContractContent_Contract] FOREIGN KEY ([ContractId]) REFERENCES [dbo].[Contract] ([ContractId])
);

