CREATE TABLE [dbo].[AccountReceivableFee] (
    [AccountReceivableFeeId]     BIGINT       IDENTITY (1, 1) NOT NULL,
    [FeeAmount]                  MONEY        NULL,
    [PaymentDueDate]             DATE         NULL,
    [CustomerId]                 BIGINT       NOT NULL,
    [DocNumber]                  BIGINT       NOT NULL,
    [AcctRecDocType]             VARCHAR (20) NOT NULL,
    [AccountReceivableId]        BIGINT       NOT NULL,
    [AccountReceivableFeeNumber] BIGINT       NOT NULL,
    CONSTRAINT [PK_AcctRecFee] PRIMARY KEY CLUSTERED ([AccountReceivableFeeId] ASC),
    CONSTRAINT [FK_AcctRecFee_AcctRec] FOREIGN KEY ([AccountReceivableId]) REFERENCES [dbo].[AccountReceivable] ([AccountReceivableId]),
    CONSTRAINT [FK_AcctRecFee_Customer] FOREIGN KEY ([CustomerId]) REFERENCES [dbo].[Customer] ([CustomerId])
);



