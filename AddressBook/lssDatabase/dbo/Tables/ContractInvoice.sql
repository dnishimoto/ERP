CREATE TABLE [dbo].[ContractInvoice] (
    [ContractInvoiceId] BIGINT IDENTITY (1, 1) NOT NULL,
    [ContractId]        BIGINT NOT NULL,
    [InvoiceId]         BIGINT NOT NULL,
    CONSTRAINT [PK_ContractInvoice] PRIMARY KEY CLUSTERED ([ContractInvoiceId] ASC)
);

