USE [listensoftwareDB]
GO
/****** Object:  Table [dbo].[ServiceInformationInvoice]    Script Date: 7/30/2018 6:36:19 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ServiceInformationInvoice](
	[ServiceInformationInvoiceId] [bigint] IDENTITY(1,1) NOT NULL,
	[InvoiceId] [bigint] NOT NULL,
	[ServiceId] [bigint] NOT NULL,
 CONSTRAINT [PK_ServiceInformationInvoice] PRIMARY KEY CLUSTERED 
(
	[ServiceInformationInvoiceId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[ServiceInformationInvoice]  WITH CHECK ADD  CONSTRAINT [FK_ServiceInformationInvoice_Invoice] FOREIGN KEY([InvoiceId])
REFERENCES [dbo].[Invoice] ([InvoiceId])
GO
ALTER TABLE [dbo].[ServiceInformationInvoice] CHECK CONSTRAINT [FK_ServiceInformationInvoice_Invoice]
GO
ALTER TABLE [dbo].[ServiceInformationInvoice]  WITH CHECK ADD  CONSTRAINT [FK_ServiceInformationInvoice_ServiceInformation] FOREIGN KEY([ServiceId])
REFERENCES [dbo].[ServiceInformation] ([ServiceId])
GO
ALTER TABLE [dbo].[ServiceInformationInvoice] CHECK CONSTRAINT [FK_ServiceInformationInvoice_ServiceInformation]
GO
