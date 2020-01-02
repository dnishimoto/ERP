CREATE TABLE [dbo].[ServiceInformationInvoice] (
    [ServiceInformationInvoiceId]     BIGINT IDENTITY (1, 1) NOT NULL,
    [InvoiceId]                       BIGINT NOT NULL,
    [ServiceId]                       BIGINT NOT NULL,
    [ServiceInformationInvoiceNumber] BIGINT NOT NULL,
    CONSTRAINT [PK_ServiceInformationInvoice] PRIMARY KEY CLUSTERED ([ServiceInformationInvoiceId] ASC),
    CONSTRAINT [FK_ServiceInformationInvoice_Invoice] FOREIGN KEY ([InvoiceId]) REFERENCES [dbo].[Invoice] ([InvoiceId]),
    CONSTRAINT [FK_ServiceInformationInvoice_ServiceInformation] FOREIGN KEY ([ServiceId]) REFERENCES [dbo].[ServiceInformation] ([ServiceId])
);



