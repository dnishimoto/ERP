CREATE TABLE [dbo].[AccountPayable] (
    [AccountPayableId]     BIGINT          IDENTITY (1, 1) NOT NULL,
    [DocNumber]            BIGINT          NULL,
    [GrossAmount]          MONEY           NULL,
    [DiscountAmount]       MONEY           NULL,
    [Remark]               VARCHAR (MAX)   NULL,
    [GLDate]               DATETIME        NULL,
    [SupplierId]           BIGINT          NOT NULL,
    [ContractId]           BIGINT          NULL,
    [POQuoteId]            BIGINT          NULL,
    [Description]          VARCHAR (1000)  NULL,
    [PurchaseOrderId]      BIGINT          NULL,
    [Tax]                  MONEY           NULL,
    [AccountId]            BIGINT          NOT NULL,
    [DocType]              VARCHAR (20)    NOT NULL,
    [PaymentTerms]         VARCHAR (20)    NULL,
    [DiscountPercent]      DECIMAL (18, 3) NULL,
    [AmountOpen]           MONEY           NULL,
    [OrderNumber]          VARCHAR (50)    NULL,
    [DiscountDueDate]      DATE            NULL,
    [AmountPaid]           MONEY           NULL,
    [AccountPayableNumber] BIGINT          NOT NULL,
    CONSTRAINT [PK_AcctPay] PRIMARY KEY CLUSTERED ([AccountPayableId] ASC),
    CONSTRAINT [FK__AcctPay__Purchas__031C6FA4] FOREIGN KEY ([PurchaseOrderId]) REFERENCES [dbo].[PurchaseOrder] ([PurchaseOrderId]),
    CONSTRAINT [FK_AcctPay_ChartOfAccts] FOREIGN KEY ([AccountId]) REFERENCES [dbo].[ChartOfAccount] ([AccountId]),
    CONSTRAINT [FK_AcctPay_Contract] FOREIGN KEY ([ContractId]) REFERENCES [dbo].[Contract] ([ContractId]),
    CONSTRAINT [FK_AcctPay_POQuote] FOREIGN KEY ([POQuoteId]) REFERENCES [dbo].[POQuote] ([POQuoteId]),
    CONSTRAINT [FK_AcctPay_Supplier] FOREIGN KEY ([SupplierId]) REFERENCES [dbo].[Supplier] ([SupplierId])
);



