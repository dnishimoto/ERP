USE [listensoftwareDB]
GO
/****** Object:  Table [dbo].[CustomerClaim]    Script Date: 7/30/2018 6:36:13 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CustomerClaim](
	[ClaimId] [bigint] IDENTITY(1,1) NOT NULL,
	[ClassificationXRefId] [bigint] NOT NULL,
	[CustomerId] [bigint] NOT NULL,
	[Configuration] [varchar](max) NULL,
	[Note] [varchar](max) NULL,
	[EmployeeId] [bigint] NOT NULL,
	[GroupIdXrefId] [bigint] NOT NULL,
	[ProcessedDate] [date] NULL,
	[CreatedDate] [date] NULL,
	[ContractId] [bigint] NULL,
 CONSTRAINT [PK_CustomerClaim] PRIMARY KEY CLUSTERED 
(
	[ClaimId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
ALTER TABLE [dbo].[CustomerClaim] ADD  CONSTRAINT [DF_CustomerClaim_CreatedDate]  DEFAULT (getdate()) FOR [CreatedDate]
GO
ALTER TABLE [dbo].[CustomerClaim]  WITH CHECK ADD  CONSTRAINT [FK_CustomerClaim_Customer] FOREIGN KEY([CustomerId])
REFERENCES [dbo].[Customer] ([CustomerId])
GO
ALTER TABLE [dbo].[CustomerClaim] CHECK CONSTRAINT [FK_CustomerClaim_Customer]
GO
ALTER TABLE [dbo].[CustomerClaim]  WITH CHECK ADD  CONSTRAINT [FK_CustomerClaim_Employee] FOREIGN KEY([EmployeeId])
REFERENCES [dbo].[Employee] ([EmployeeId])
GO
ALTER TABLE [dbo].[CustomerClaim] CHECK CONSTRAINT [FK_CustomerClaim_Employee]
GO
ALTER TABLE [dbo].[CustomerClaim]  WITH CHECK ADD  CONSTRAINT [FK_CustomerClaim_UDC] FOREIGN KEY([ClassificationXRefId])
REFERENCES [dbo].[UDC] ([XRefId])
GO
ALTER TABLE [dbo].[CustomerClaim] CHECK CONSTRAINT [FK_CustomerClaim_UDC]
GO
ALTER TABLE [dbo].[CustomerClaim]  WITH CHECK ADD  CONSTRAINT [FK_CustomerClaim_UDC1] FOREIGN KEY([GroupIdXrefId])
REFERENCES [dbo].[UDC] ([XRefId])
GO
ALTER TABLE [dbo].[CustomerClaim] CHECK CONSTRAINT [FK_CustomerClaim_UDC1]
GO
