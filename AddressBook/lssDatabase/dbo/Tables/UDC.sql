CREATE TABLE [dbo].[UDC] (
    [XRefId]      BIGINT        IDENTITY (1, 1) NOT NULL,
    [ProductCode] VARCHAR (20)  NULL,
    [KeyCode]     VARCHAR (50)  NULL,
    [Value]       VARCHAR (255) NULL,
    [UdcNumber]   BIGINT        NOT NULL,
    CONSTRAINT [PK__UDC__B9604F3957BD8349] PRIMARY KEY CLUSTERED ([XRefId] ASC)
);



