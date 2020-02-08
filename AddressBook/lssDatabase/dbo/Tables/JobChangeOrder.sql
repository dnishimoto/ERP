CREATE TABLE [dbo].[JobChangeOrder] (
    [JobChangeOrderId]     BIGINT         IDENTITY (1, 1) NOT NULL,
    [JobMasterId]          BIGINT         NOT NULL,
    [ContractId]           BIGINT         NOT NULL,
    [ContractItemId]       BIGINT         NOT NULL,
    [Description]          VARCHAR (1000) NULL,
    [InvoiceId]            BIGINT         NULL,
    [AcctRecId]            BIGINT         NULL,
    [CustomerId]           BIGINT         NULL,
    [ChangeAmount]         MONEY          NOT NULL,
    [EstimatedAmount]      MONEY          NULL,
    [JobChangeOrderNumber] BIGINT         NOT NULL,
    CONSTRAINT [PK_JobChangeOrder] PRIMARY KEY CLUSTERED ([JobChangeOrderId] ASC),
    CONSTRAINT [FK_JobChangeOrder_Contract] FOREIGN KEY ([ContractId]) REFERENCES [dbo].[Contract] ([ContractId]),
    CONSTRAINT [FK_JobChangeOrder_ContractItem] FOREIGN KEY ([ContractItemId]) REFERENCES [dbo].[ContractItem] ([ContractItemId]),
    CONSTRAINT [FK_JobChangeOrder_JobMaster] FOREIGN KEY ([JobMasterId]) REFERENCES [dbo].[JobMaster] ([JobMasterId])
);

