USE [listensoftwareDB]
GO
/****** Object:  Table [dbo].[ContractContent]    Script Date: 7/30/2018 6:36:16 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ContractContent](
	[ContractContentId] [bigint] IDENTITY(1,1) NOT NULL,
	[ContractId] [bigint] NOT NULL,
	[WBS] [varchar](50) NULL,
	[TextMemo] [varchar](max) NULL,
 CONSTRAINT [PK_ContractContent] PRIMARY KEY CLUSTERED 
(
	[ContractContentId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
ALTER TABLE [dbo].[ContractContent]  WITH CHECK ADD  CONSTRAINT [FK_ContractContent_Contract] FOREIGN KEY([ContractId])
REFERENCES [dbo].[Contract] ([ContractId])
GO
ALTER TABLE [dbo].[ContractContent] CHECK CONSTRAINT [FK_ContractContent_Contract]
GO
