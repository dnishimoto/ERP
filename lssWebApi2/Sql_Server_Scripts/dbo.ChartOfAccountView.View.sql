USE [listensoftwareDB]
GO
/****** Object:  View [dbo].[ChartOfAccountView]    Script Date: 7/30/2018 6:36:15 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

/****** Script for SelectTopNRows command from SSMS  ******/
CREATE View [dbo].[ChartOfAccountView]
as
SELECT [AccountId]
      ,[Location]
      ,[BusUnit]
      ,[Description]
      ,[CompanyNumber]
	  ,[ObjectNumber]
	  ,[Account]
      ,[PostEditCode]
      ,coa.[CompanyId]
	  ,company.CompanyName
      ,[Level]
  FROM [dbo].[ChartOfAccts] coa join
  company company on coa.CompanyId=company.CompanyId
GO
