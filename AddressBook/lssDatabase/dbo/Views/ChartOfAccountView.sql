
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
