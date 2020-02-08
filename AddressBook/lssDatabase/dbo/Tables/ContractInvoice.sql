CREATE TABLE [dbo].[ContractInvoice] (
    [ContractInvoiceId]     BIGINT IDENTITY (1, 1) NOT NULL,
    [ContractId]            BIGINT NOT NULL,
    [InvoiceId]             BIGINT NOT NULL,
    [ContractInvoiceNumber] BIGINT NOT NULL,
    CONSTRAINT [PK_ContractInvoice] PRIMARY KEY CLUSTERED ([ContractInvoiceId] ASC),
    CONSTRAINT [FK_ContractInvoice_Contract] FOREIGN KEY ([ContractId]) REFERENCES [dbo].[Contract] ([ContractId]),
    CONSTRAINT [FK_ContractInvoice_Invoice] FOREIGN KEY ([InvoiceId]) REFERENCES [dbo].[Invoice] ([InvoiceId])
);



