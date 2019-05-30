CREATE TABLE [dbo].[ContractContent] (
    [ContractContentId] BIGINT        IDENTITY (1, 1) NOT NULL,
    [ContractId]        BIGINT        NOT NULL,
    [WBS]               VARCHAR (50)  NULL,
    [TextMemo]          VARCHAR (MAX) NULL,
    CONSTRAINT [PK_ContractContent] PRIMARY KEY CLUSTERED ([ContractContentId] ASC),
    CONSTRAINT [FK_ContractContent_Contract] FOREIGN KEY ([ContractId]) REFERENCES [dbo].[Contract] ([ContractId])
);

