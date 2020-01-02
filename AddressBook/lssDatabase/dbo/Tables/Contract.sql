CREATE TABLE [dbo].[Contract] (
    [ContractId]        BIGINT        IDENTITY (1, 1) NOT NULL,
    [CustomerId]        BIGINT        NULL,
    [ServiceTypeXRefId] BIGINT        NULL,
    [StartDate]         DATETIME      NULL,
    [EndDate]           DATETIME      NULL,
    [Cost]              MONEY         NULL,
    [RemainingBalance]  MONEY         NULL,
    [Title]             VARCHAR (200) NULL,
    [ContractNumber]    BIGINT        NOT NULL,
    CONSTRAINT [PK__Contract__C90D34697E612150] PRIMARY KEY CLUSTERED ([ContractId] ASC),
    CONSTRAINT [FK_Contract_Customer] FOREIGN KEY ([CustomerId]) REFERENCES [dbo].[Customer] ([CustomerId]),
    CONSTRAINT [FK_Contract_UDC] FOREIGN KEY ([ServiceTypeXRefId]) REFERENCES [dbo].[UDC] ([XRefId])
);



