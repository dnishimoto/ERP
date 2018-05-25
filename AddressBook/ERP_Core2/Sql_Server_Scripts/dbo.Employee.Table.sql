USE [listensoftwareDB]
GO
/****** Object:  Table [dbo].[Employee]    Script Date: 5/24/2018 9:44:49 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Employee](
	[EmployeeId] [bigint] IDENTITY(1,1) NOT NULL,
	[AddressId] [bigint] NOT NULL,
	[PrimaryAddressId] [bigint] NULL,
	[PrimaryEmailId] [bigint] NULL,
	[PrimaryPhoneId] [bigint] NULL,
	[MailingAddressId] [bigint] NULL,
	[TaxIdentification] [varchar](50) NULL,
 CONSTRAINT [PK__Employee__091C2AFB7C8C5421] PRIMARY KEY CLUSTERED 
(
	[EmployeeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON)
)

GO
ALTER TABLE [dbo].[Employee]  WITH CHECK ADD  CONSTRAINT [FK_Employee_AddressBook] FOREIGN KEY([AddressId])
REFERENCES [dbo].[AddressBook] ([AddressId])
GO
ALTER TABLE [dbo].[Employee] CHECK CONSTRAINT [FK_Employee_AddressBook]
GO
