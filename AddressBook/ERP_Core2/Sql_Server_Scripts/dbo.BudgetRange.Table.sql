USE [listensoftwareDB]
GO
/****** Object:  Table [dbo].[BudgetRange]    Script Date: 7/30/2018 6:36:10 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BudgetRange](
	[RangeId] [bigint] IDENTITY(1,1) NOT NULL,
	[StartDate] [date] NULL,
	[EndDate] [date] NULL,
	[Location] [varchar](2) NULL,
	[GenCode] [varchar](3) NULL,
	[SubCode] [varchar](3) NULL,
	[SubsidiaryAcct] [varchar](2) NULL,
	[Company] [varchar](10) NULL,
	[BusinessUnit] [varchar](10) NULL,
	[Subsidiary] [varchar](10) NULL,
	[AccountId] [bigint] NULL,
	[SupervisorCode] [varchar](50) NULL,
	[LastUpdated] [datetime] NULL,
 CONSTRAINT [PK_BudgetRange] PRIMARY KEY CLUSTERED 
(
	[RangeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[BudgetRange]  WITH CHECK ADD  CONSTRAINT [FK_BudgetRange_ChartOfAccts] FOREIGN KEY([AccountId])
REFERENCES [dbo].[ChartOfAccts] ([AccountId])
GO
ALTER TABLE [dbo].[BudgetRange] CHECK CONSTRAINT [FK_BudgetRange_ChartOfAccts]
GO
