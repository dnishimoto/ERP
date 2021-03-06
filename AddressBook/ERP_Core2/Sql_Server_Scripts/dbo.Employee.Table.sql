USE [listensoftwareDB]
GO
/****** Object:  Table [dbo].[Employee]    Script Date: 7/30/2018 6:36:13 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Employee](
	[EmployeeId] [bigint] IDENTITY(1,1) NOT NULL,
	[AddressId] [bigint] NOT NULL,
	[JobTitleXrefId] [bigint] NOT NULL,
	[EmploymentStatusXRefId] [bigint] NOT NULL,
	[HiredDate] [date] NULL,
	[TerminationDate] [date] NULL,
	[TaxIdentification] [varchar](50) NULL,
 CONSTRAINT [PK__Employee__091C2AFB7C8C5421] PRIMARY KEY CLUSTERED 
(
	[EmployeeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Employee]  WITH CHECK ADD FOREIGN KEY([AddressId])
REFERENCES [dbo].[AddressBook] ([AddressId])
GO
ALTER TABLE [dbo].[Employee]  WITH CHECK ADD FOREIGN KEY([EmploymentStatusXRefId])
REFERENCES [dbo].[UDC] ([XRefId])
GO
ALTER TABLE [dbo].[Employee]  WITH CHECK ADD FOREIGN KEY([JobTitleXrefId])
REFERENCES [dbo].[UDC] ([XRefId])
GO
