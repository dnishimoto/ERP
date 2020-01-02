CREATE TABLE [dbo].[Company] (
    [CompanyId]      BIGINT        IDENTITY (1, 1) NOT NULL,
    [CompanyName]    VARCHAR (50)  NULL,
    [CompanyCode]    VARCHAR (10)  NULL,
    [CompanyStreet]  VARCHAR (100) NULL,
    [CompanyCity]    VARCHAR (50)  NULL,
    [CompanyState]   VARCHAR (20)  NULL,
    [CompanyZipcode] VARCHAR (20)  NULL,
    [TaxCode1]       VARCHAR (20)  NULL,
    [TaxCode2]       VARCHAR (20)  NULL,
    [CompanyNumber]  BIGINT        NULL,
    CONSTRAINT [PK_Company] PRIMARY KEY CLUSTERED ([CompanyId] ASC)
);



