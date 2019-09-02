CREATE TABLE [dbo].[TaxRatesByCode] (
    [TaxRatesByCodeId]     BIGINT       IDENTITY (1, 1) NOT NULL,
    [TaxCode]              VARCHAR (20) NULL,
    [TaxRate]              MONEY        NULL,
    [State]                VARCHAR (2)  NULL,
    [TaxRatesByCodeNumber] BIGINT       NOT NULL,
    CONSTRAINT [PK_TaxRatesByCode] PRIMARY KEY CLUSTERED ([TaxRatesByCodeId] ASC)
);



