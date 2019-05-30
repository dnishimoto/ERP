CREATE TABLE [dbo].[SalesOrder] (
    [SalesOrderId]      BIGINT          IDENTITY (1, 1) NOT NULL,
    [Amount]            MONEY           NULL,
    [OrderNumber]       VARCHAR (20)    NULL,
    [OrderType]         VARCHAR (20)    NULL,
    [CustomerId]        BIGINT          NOT NULL,
    [TakenBy]           VARCHAR (50)    NULL,
    [FreightAmount]     DECIMAL (18, 4) NULL,
    [PaymentInstrument] VARCHAR (20)    NULL,
    [PaymentTerms]      VARCHAR (20)    NULL,
    [Taxes]             MONEY           NULL,
    [Note]              VARCHAR (MAX)   NULL,
    [AmountOpen]        MONEY           NULL,
    CONSTRAINT [PK_SalesOrder] PRIMARY KEY CLUSTERED ([SalesOrderId] ASC),
    CONSTRAINT [FK_SalesOrder_Customer] FOREIGN KEY ([CustomerId]) REFERENCES [dbo].[Customer] ([CustomerId])
);


GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_SalesOrder]
    ON [dbo].[SalesOrder]([OrderNumber] ASC);

