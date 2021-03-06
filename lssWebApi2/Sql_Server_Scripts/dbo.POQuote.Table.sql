USE [listensoftwareDB]
GO
/****** Object:  Table [dbo].[POQuote]    Script Date: 7/30/2018 6:36:18 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[POQuote](
	[POQuoteId] [bigint] IDENTITY(1,1) NOT NULL,
	[QuoteAmount] [money] NULL,
	[SubmittedDate] [date] NULL,
	[PoId] [bigint] NOT NULL,
	[DocNumber] [bigint] NOT NULL,
	[Remarks] [varchar](255) NULL,
	[CustomerId] [bigint] NOT NULL,
	[SupplierId] [bigint] NOT NULL,
	[SKU] [varchar](50) NULL,
	[Description] [varchar](255) NULL,
PRIMARY KEY CLUSTERED 
(
	[POQuoteId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[POQuote]  WITH CHECK ADD  CONSTRAINT [FK_POQuote_Customer] FOREIGN KEY([CustomerId])
REFERENCES [dbo].[Customer] ([CustomerId])
GO
ALTER TABLE [dbo].[POQuote] CHECK CONSTRAINT [FK_POQuote_Customer]
GO
ALTER TABLE [dbo].[POQuote]  WITH CHECK ADD  CONSTRAINT [FK_POQuote_Supplier] FOREIGN KEY([SupplierId])
REFERENCES [dbo].[Supplier] ([SupplierId])
GO
ALTER TABLE [dbo].[POQuote] CHECK CONSTRAINT [FK_POQuote_Supplier]
GO
