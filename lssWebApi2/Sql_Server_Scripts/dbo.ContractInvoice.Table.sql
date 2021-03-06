USE [listensoftwareDB]
GO
/****** Object:  Table [dbo].[ContractInvoice]    Script Date: 7/30/2018 6:36:16 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ContractInvoice](
	[ContractInvoiceId] [bigint] IDENTITY(1,1) NOT NULL,
	[ContractId] [bigint] NOT NULL,
	[InvoiceId] [bigint] NOT NULL,
 CONSTRAINT [PK_ContractInvoice] PRIMARY KEY CLUSTERED 
(
	[ContractInvoiceId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
