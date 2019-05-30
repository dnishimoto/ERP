CREATE TABLE [dbo].[TaxRatesByCode] (
    [TaxId]   BIGINT       IDENTITY (1, 1) NOT NULL,
    [TaxCode] VARCHAR (20) NULL,
    [TaxRate] MONEY        NULL,
    [State]   VARCHAR (2)  NULL,
    CONSTRAINT [PK_TaxRatesByCode] PRIMARY KEY CLUSTERED ([TaxId] ASC)
);

