CREATE TABLE [dbo].[PurchaseOrder] (
    [PurchaseOrderId]       BIGINT          IDENTITY (1, 1) NOT NULL,
    [DocType]               VARCHAR (20)    NULL,
    [PaymentTerms]          VARCHAR (10)    NULL,
    [Remark]                VARCHAR (MAX)   NULL,
    [GLDate]                DATETIME        NULL,
    [AccountId]             BIGINT          NOT NULL,
    [SupplierId]            BIGINT          NULL,
    [ContractId]            BIGINT          NULL,
    [POQuoteId]             BIGINT          NULL,
    [Description]           VARCHAR (1000)  NULL,
    [PONumber]              VARCHAR (50)    NULL,
    [TakenBy]               NCHAR (100)     NULL,
    [BuyerId]               BIGINT          NULL,
    [RequestedDate]         DATE            NULL,
    [PromisedDeliveredDate] DATE            NULL,
    [Tax]                   MONEY           NULL,
    [TransactionDate]       DATETIME        NULL,
    [AmountPaid]            MONEY           NULL,
    [ShippedToName]         VARCHAR (255)   NULL,
    [ShippedToAddress1]     VARCHAR (100)   NULL,
    [ShippedToAddress2]     VARCHAR (100)   NULL,
    [ShippedToCity]         VARCHAR (50)    NULL,
    [ShippedToZipcode]      VARCHAR (20)    NULL,
    [ShippedToState]        VARCHAR (20)    NULL,
    [TaxCode1]              VARCHAR (20)    NULL,
    [TaxCode2]              VARCHAR (20)    NULL,
    [PurchaseOrderNumber]   BIGINT          NOT NULL,
    [Amount]                MONEY           NULL,
    [CarrierId]             BIGINT          NULL,
    [DiscountPercent]       DECIMAL (18, 2) NULL,
    [DiscountAmount]        MONEY           NULL,
    [FreightAmount]         MONEY           NULL,
    [CustomerId]            BIGINT          NULL,
    [DiscountDueDate]       DATE            NULL,
    CONSTRAINT [PK_PurchaseOrder] PRIMARY KEY CLUSTERED ([PurchaseOrderId] ASC),
    CONSTRAINT [FK_PurchaseOrder_ChartOfAccts] FOREIGN KEY ([AccountId]) REFERENCES [dbo].[ChartOfAccount] ([AccountId])
);





