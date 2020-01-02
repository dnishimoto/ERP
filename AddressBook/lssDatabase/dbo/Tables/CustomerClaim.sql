CREATE TABLE [dbo].[CustomerClaim] (
    [ClaimId]              BIGINT        IDENTITY (1, 1) NOT NULL,
    [ClassificationXRefId] BIGINT        NOT NULL,
    [CustomerId]           BIGINT        NOT NULL,
    [Configuration]        VARCHAR (MAX) NULL,
    [Note]                 VARCHAR (MAX) NULL,
    [EmployeeId]           BIGINT        NOT NULL,
    [GroupIdXrefId]        BIGINT        NOT NULL,
    [ProcessedDate]        DATE          NULL,
    [CreatedDate]          DATE          CONSTRAINT [DF_CustomerClaim_CreatedDate] DEFAULT (getdate()) NULL,
    [ContractId]           BIGINT        NULL,
    [CustomerClaimNumber]  BIGINT        NULL,
    CONSTRAINT [PK_CustomerClaim] PRIMARY KEY CLUSTERED ([ClaimId] ASC),
    CONSTRAINT [FK_CustomerClaim_Customer] FOREIGN KEY ([CustomerId]) REFERENCES [dbo].[Customer] ([CustomerId]),
    CONSTRAINT [FK_CustomerClaim_Employee] FOREIGN KEY ([EmployeeId]) REFERENCES [dbo].[Employee] ([EmployeeId]),
    CONSTRAINT [FK_CustomerClaim_UDC] FOREIGN KEY ([ClassificationXRefId]) REFERENCES [dbo].[UDC] ([XRefId]),
    CONSTRAINT [FK_CustomerClaim_UDC1] FOREIGN KEY ([GroupIdXrefId]) REFERENCES [dbo].[UDC] ([XRefId])
);




GO
CREATE NONCLUSTERED INDEX [idx_customerclaim_classificationXrefid]
    ON [dbo].[CustomerClaim]([ClassificationXRefId] ASC);


GO
CREATE NONCLUSTERED INDEX [idx_customerclaim_customerid]
    ON [dbo].[CustomerClaim]([CustomerId] ASC);


GO
CREATE NONCLUSTERED INDEX [idx_customerclaim_employeeid]
    ON [dbo].[CustomerClaim]([EmployeeId] ASC);

