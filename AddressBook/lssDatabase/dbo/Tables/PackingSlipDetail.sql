CREATE TABLE [dbo].[PackingSlipDetail] (
    [PackingSlipDetailId] BIGINT          IDENTITY (1, 1) NOT NULL,
    [PackingSlipId]       BIGINT          NOT NULL,
    [ItemId]              BIGINT          NOT NULL,
    [Quantity]            INT             NULL,
    [UnitPrice]           DECIMAL (18, 4) NULL,
    [ExtendedCost]        DECIMAL (18, 4) NULL,
    [UnitOfMeasure]       VARCHAR (20)    NULL,
    [Description]         VARCHAR (200)   NULL,
    CONSTRAINT [PK_PackingSlipDetail] PRIMARY KEY CLUSTERED ([PackingSlipDetailId] ASC),
    CONSTRAINT [FK_PackingSlipDetail_PackingSlip] FOREIGN KEY ([PackingSlipId]) REFERENCES [dbo].[PackingSlip] ([PackingSlipId])
);

