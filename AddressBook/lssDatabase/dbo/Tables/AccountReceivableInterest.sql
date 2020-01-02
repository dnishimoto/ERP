CREATE TABLE [dbo].[AccountReceivableInterest] (
    [AcctRecInterestId]               BIGINT       IDENTITY (1, 1) NOT NULL,
    [Amount]                          MONEY        NULL,
    [InterestRate]                    MONEY        NULL,
    [InterestFromDate]                DATE         NULL,
    [InterestToDate]                  DATE         NULL,
    [DocNumber]                       BIGINT       NOT NULL,
    [PaymentTerms]                    VARCHAR (50) NULL,
    [PaymentDueDate]                  DATE         NULL,
    [CustomerId]                      BIGINT       NOT NULL,
    [AcctRecDocType]                  VARCHAR (20) NOT NULL,
    [LastInterestDueDate]             DATE         NULL,
    [AcctRecId]                       BIGINT       NOT NULL,
    [AccountReceivableInterestNumber] BIGINT       NOT NULL,
    CONSTRAINT [PK_AcctRecInterest] PRIMARY KEY CLUSTERED ([AcctRecInterestId] ASC),
    CONSTRAINT [FK_AcctRecInterest_AcctRec] FOREIGN KEY ([AcctRecId]) REFERENCES [dbo].[AccountReceivable] ([AcctRecId]),
    CONSTRAINT [FK_AcctRecInterest_Customer] FOREIGN KEY ([CustomerId]) REFERENCES [dbo].[Customer] ([CustomerId])
);

