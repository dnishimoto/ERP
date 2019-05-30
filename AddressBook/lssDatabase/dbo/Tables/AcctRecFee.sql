CREATE TABLE [dbo].[AcctRecFee] (
    [AcctRecFeeId]   BIGINT       IDENTITY (1, 1) NOT NULL,
    [FeeAmount]      MONEY        NULL,
    [PaymentDueDate] DATE         NULL,
    [CustomerId]     BIGINT       NOT NULL,
    [DocNumber]      BIGINT       NOT NULL,
    [AcctRecDocType] VARCHAR (20) NOT NULL,
    [AcctRecId]      BIGINT       NOT NULL,
    CONSTRAINT [PK_AcctRecFee] PRIMARY KEY CLUSTERED ([AcctRecFeeId] ASC),
    CONSTRAINT [FK_AcctRecFee_AcctRec] FOREIGN KEY ([AcctRecId]) REFERENCES [dbo].[AcctRec] ([AcctRecId]),
    CONSTRAINT [FK_AcctRecFee_Customer] FOREIGN KEY ([CustomerId]) REFERENCES [dbo].[Customer] ([CustomerId])
);

