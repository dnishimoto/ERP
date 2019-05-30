CREATE TABLE [dbo].[POQuote] (
    [POQuoteId]     BIGINT        IDENTITY (1, 1) NOT NULL,
    [QuoteAmount]   MONEY         NULL,
    [SubmittedDate] DATE          NULL,
    [PoId]          BIGINT        NOT NULL,
    [DocNumber]     BIGINT        NOT NULL,
    [Remarks]       VARCHAR (255) NULL,
    [CustomerId]    BIGINT        NOT NULL,
    [SupplierId]    BIGINT        NOT NULL,
    [SKU]           VARCHAR (50)  NULL,
    [Description]   VARCHAR (255) NULL,
    PRIMARY KEY CLUSTERED ([POQuoteId] ASC),
    CONSTRAINT [FK_POQuote_Customer] FOREIGN KEY ([CustomerId]) REFERENCES [dbo].[Customer] ([CustomerId]),
    CONSTRAINT [FK_POQuote_Supplier] FOREIGN KEY ([SupplierId]) REFERENCES [dbo].[Supplier] ([SupplierId])
);

