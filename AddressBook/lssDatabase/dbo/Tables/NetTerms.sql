CREATE TABLE [dbo].[NetTerms] (
    [NetTermId]       BIGINT          IDENTITY (1, 1) NOT NULL,
    [NetTerms]        VARCHAR (50)    NULL,
    [DiscountPercent] DECIMAL (18, 2) NULL,
    CONSTRAINT [PK_NetTerms] PRIMARY KEY CLUSTERED ([NetTermId] ASC)
);

