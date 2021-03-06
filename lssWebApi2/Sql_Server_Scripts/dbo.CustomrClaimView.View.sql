USE [listensoftwareDB]
GO
/****** Object:  View [dbo].[CustomrClaimView]    Script Date: 7/30/2018 6:36:13 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE view [dbo].[CustomrClaimView]
as
SELECT [ClaimId]
      ,[ClassificationXRefId]
      ,customer.[CustomerId]
      ,[Configuration]
      ,[Note]
      ,[EmployeeId]
      ,[GroupIdXrefId]
	  ,udcClassification.Value Classification
	  ,udcGroupId.Value GroupId
	  ,addressBook.Name CustomerName
  FROM [dbo].[CustomerClaim] customerClaim join
  udc udcClassification on udcClassification.xrefid=customerClaim.ClassificationXRefId join
  udc udcGroupId on udcGroupId.xrefid=customerClaim.groupidxrefid join
  customer customer on customer.customerid=customerclaim.customerid join
  addressbook addressbook on addressbook.AddressId=customer.AddressId
GO
