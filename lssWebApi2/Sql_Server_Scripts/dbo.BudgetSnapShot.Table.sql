USE [listensoftwareDB]
GO
/****** Object:  Table [dbo].[BudgetSnapShot]    Script Date: 7/30/2018 6:36:16 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BudgetSnapShot](
	[BudgetId] [bigint] IDENTITY(1,1) NOT NULL,
	[BudgetHours] [decimal](18, 1) NULL,
	[BudgetAmount] [decimal](18, 4) NULL,
	[ActualHours] [decimal](18, 2) NULL,
	[ActualAmount] [decimal](18, 4) NULL,
	[AccountId] [bigint] NULL,
	[RangeId] [bigint] NULL,
	[ProjectedHours] [decimal](18, 1) NULL,
	[ProjectedAmount] [decimal](18, 4) NULL,
	[OpenPurchaseOrderAmount] [decimal](18, 4) NULL,
	[Comments] [varchar](max) NULL,
 CONSTRAINT [PK_BudgetSnapShot] PRIMARY KEY CLUSTERED 
(
	[BudgetId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
