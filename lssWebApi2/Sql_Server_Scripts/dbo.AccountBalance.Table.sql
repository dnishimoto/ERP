USE [listensoftwareDB]
GO
/****** Object:  Table [dbo].[AccountBalance]    Script Date: 7/30/2018 6:36:15 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AccountBalance](
	[AccountBalanceId] [bigint] IDENTITY(1,1) NOT NULL,
	[AccountBalanceType] [varchar](10) NULL,
	[Amount] [money] NULL,
	[Hours] [decimal](18, 2) NULL,
	[FiscalYear] [int] NOT NULL,
	[FiscalPeriod] [int] NOT NULL,
	[AccountId] [bigint] NOT NULL,
 CONSTRAINT [PK_AccountBalance] PRIMARY KEY CLUSTERED 
(
	[AccountBalanceId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[AccountBalance]  WITH CHECK ADD  CONSTRAINT [FK_AccountBalance_ChartOfAccts] FOREIGN KEY([AccountId])
REFERENCES [dbo].[ChartOfAccts] ([AccountId])
GO
ALTER TABLE [dbo].[AccountBalance] CHECK CONSTRAINT [FK_AccountBalance_ChartOfAccts]
GO
