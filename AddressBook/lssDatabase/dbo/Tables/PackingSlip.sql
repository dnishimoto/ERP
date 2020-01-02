CREATE TABLE [dbo].[PackingSlip] (
    [PackingSlipId]     BIGINT        IDENTITY (1, 1) NOT NULL,
    [SupplierId]        BIGINT        NOT NULL,
    [ReceivedDate]      DATETIME      NOT NULL,
    [SlipDocument]      VARCHAR (50)  NULL,
    [PONumber]          VARCHAR (50)  NULL,
    [Remark]            VARCHAR (MAX) NULL,
    [SlipType]          VARCHAR (20)  NULL,
    [Amount]            MONEY         NULL,
    [PackingSlipNumber] BIGINT        NOT NULL,
    CONSTRAINT [PK_Receipt] PRIMARY KEY CLUSTERED ([PackingSlipId] ASC),
    CONSTRAINT [FK_Receipt_Supplier] FOREIGN KEY ([SupplierId]) REFERENCES [dbo].[Supplier] ([SupplierId])
);



