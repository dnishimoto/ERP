USE [listensoftwareDB]
GO
/****** Object:  UserDefinedTableType [dbo].[AccountRegistrationTableType]    Script Date: 12/2/2020 5:04:02 AM ******/
CREATE TYPE [dbo].[AccountRegistrationTableType] AS TABLE(
	[CustomerName] [varchar](50) NULL,
	[FirstName] [varchar](50) NULL,
	[LastName] [varchar](50) NULL,
	[CompanyName] [varchar](100) NULL,
	[Address_Line1] [varchar](100) NULL,
	[Address_Line2] [varchar](100) NULL,
	[City] [varchar](100) NULL,
	[State] [varchar](100) NULL,
	[Zipcode] [varchar](100) NULL,
	[EmailText] [varchar](50) NULL,
	[LoginEmail] [bit] NULL,
	[Password] [varchar](50) NULL
)
GO
/****** Object:  Table [dbo].[GeneralLedger]    Script Date: 12/2/2020 5:04:02 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[GeneralLedger](
	[GeneralLedgerId] [bigint] IDENTITY(1,1) NOT NULL,
	[DocNumber] [bigint] NOT NULL,
	[DocType] [varchar](10) NOT NULL,
	[Amount] [money] NOT NULL,
	[LedgerType] [varchar](10) NOT NULL,
	[GLDate] [datetime] NOT NULL,
	[AccountId] [bigint] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[AddressId] [bigint] NOT NULL,
	[Comment] [varchar](255) NULL,
	[DebitAmount] [money] NULL,
	[CreditAmount] [money] NULL,
	[FiscalYear] [int] NULL,
	[FiscalPeriod] [int] NULL,
	[CheckNumber] [varchar](50) NULL,
	[PurchaseOrderNumber] [varchar](50) NULL,
	[Units] [decimal](18, 4) NULL,
	[GeneralLedgerNumber] [bigint] NOT NULL,
 CONSTRAINT [PK__generalL__3214EC07AC773B83] PRIMARY KEY CLUSTERED 
(
	[GeneralLedgerId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AddressBook]    Script Date: 12/2/2020 5:04:09 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AddressBook](
	[AddressId] [bigint] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](255) NULL,
	[FirstName] [varchar](50) NULL,
	[LastName] [varchar](50) NULL,
	[PeopleXrefId] [bigint] NULL,
	[CategoryCodeChar1] [varchar](50) NULL,
	[CategoryCodeChar2] [varchar](50) NULL,
	[CategoryCodeChar3] [varchar](50) NULL,
	[CategoryCodeInt1] [int] NULL,
	[CategoryCodeInt2] [int] NULL,
	[CategoryCodeInt3] [int] NULL,
	[CategoryCodeDate1] [date] NULL,
	[CategoryCodeDate2] [date] NULL,
	[CategoryCodeDate3] [date] NULL,
	[CompanyName] [varchar](100) NULL,
PRIMARY KEY CLUSTERED 
(
	[AddressId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[IncomeView]    Script Date: 12/2/2020 5:04:11 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create view [dbo].[IncomeView]
as
select coa.Description,
coa.Account,
coa.AccountId,
gl.GeneralLedgerId,
gl.DocType,
gl.LedgerType,
gl.AddressId,
ab.Name,
gl.Amount,
gl.GLDate,
gl.FiscalPeriod,
gl.FiscalYear
 from chartOfAccts coa
join GeneralLedger gl
	on coa.AccountId=gl.AccountId
join AddressBook ab
	on gl.AddressId=ab.AddressId
where busUnit='1200' and objectNumber='300'
GO
/****** Object:  Table [dbo].[SalesOrder]    Script Date: 12/2/2020 5:04:11 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SalesOrder](
	[SalesOrderId] [bigint] IDENTITY(1,1) NOT NULL,
	[Amount] [money] NULL,
	[OrderNumber] [varchar](20) NULL,
	[OrderType] [varchar](20) NULL,
	[CustomerId] [bigint] NOT NULL,
	[TakenBy] [varchar](50) NULL,
	[FreightAmount] [decimal](18, 4) NULL,
	[PaymentInstrument] [varchar](20) NULL,
	[PaymentTerms] [varchar](20) NULL,
	[Taxes] [money] NULL,
	[Note] [varchar](max) NULL,
	[AmountOpen] [money] NULL,
 CONSTRAINT [PK_SalesOrder] PRIMARY KEY CLUSTERED 
(
	[SalesOrderId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Carrier]    Script Date: 12/2/2020 5:04:12 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Carrier](
	[CarrierId] [bigint] IDENTITY(1,1) NOT NULL,
	[AddressId] [bigint] NOT NULL,
	[CarrierTypeXrefId] [bigint] NOT NULL,
	[CarrierNumber] [bigint] NOT NULL,
 CONSTRAINT [PK_Carier] PRIMARY KEY CLUSTERED 
(
	[CarrierId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Buyer]    Script Date: 12/2/2020 5:04:12 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Buyer](
	[BuyerId] [bigint] IDENTITY(1,1) NOT NULL,
	[AddressId] [bigint] NOT NULL,
	[Title] [varchar](100) NULL,
	[BuyerNumber] [bigint] NOT NULL,
 CONSTRAINT [PK_Buyer] PRIMARY KEY CLUSTERED 
(
	[BuyerId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Customer]    Script Date: 12/2/2020 5:04:12 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Customer](
	[CustomerId] [bigint] IDENTITY(1,1) NOT NULL,
	[AddressId] [bigint] NOT NULL,
	[PrimaryShippedToLocationAddressId] [bigint] NULL,
	[PrimaryEmailId] [bigint] NULL,
	[PrimaryPhoneId] [bigint] NULL,
	[MailingLocationAddressId] [bigint] NULL,
	[PrimaryBillingLocationAddressId] [bigint] NULL,
	[TaxIdentification] [varchar](50) NULL,
	[CustomerNumber] [bigint] NULL,
 CONSTRAINT [PK__Customer__091C2AFB7C8C5421] PRIMARY KEY CLUSTERED 
(
	[CustomerId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[SalesOrderAndInvoiceView]    Script Date: 12/2/2020 5:04:13 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE View [dbo].[SalesOrderAndInvoiceView]
as
select 
salesorder.amount Amount
,salesorder.AmountOpen 
,salesorder.ordernumber
,salesorder.ordertype
,salesorder.CustomerId
,salesorder.TakenBy
,salesorder.FreightAmount
,salesorder.PaymentInstrument
,customer.customername
,buyer.buyername
,carrier.carriername

from salesorder salesorder

outer apply
(select name customername from addressbook addressbook join
customer customer on addressbook.addressid=customer.addressid) customer

outer apply
(select name buyername from addressbook addressbook join
buyer buyer on addressbook.addressid=buyer.addressid) buyer

outer apply
(select name carriername from addressbook addressbook join
carrier carrier on addressbook.addressid=carrier.addressid) carrier
GO
/****** Object:  Table [dbo].[BudgetRange]    Script Date: 12/2/2020 5:04:13 AM ******/
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
	[CompanyCode] [varchar](10) NULL,
	[BusUnit] [varchar](10) NULL,
	[Subsidiary] [varchar](10) NULL,
	[AccountId] [bigint] NULL,
	[SupervisorCode] [varchar](50) NULL,
	[LastUpdated] [datetime] NULL,
	[ObjectNumber] [varchar](10) NULL,
	[IsActive] [bit] NULL,
	[PayCycles] [int] NULL,
	[BudgetRangeNumber] [bigint] NOT NULL,
 CONSTRAINT [PK_BudgetRange] PRIMARY KEY CLUSTERED 
(
	[RangeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Budget]    Script Date: 12/2/2020 5:04:13 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Budget](
	[BudgetId] [bigint] IDENTITY(1,1) NOT NULL,
	[BudgetHours] [decimal](18, 1) NULL,
	[BudgetAmount] [decimal](18, 4) NULL,
	[ActualHours] [decimal](18, 2) NULL,
	[ActualAmount] [decimal](18, 4) NULL,
	[AccountId] [bigint] NULL,
	[RangeId] [bigint] NULL,
	[ProjectedHours] [decimal](18, 1) NULL,
	[ProjectedAmount] [decimal](18, 4) NULL,
	[ActualsAsOfDate] [date] NULL,
	[BudgetNumber] [bigint] NOT NULL,
 CONSTRAINT [PK_Budget] PRIMARY KEY CLUSTERED 
(
	[BudgetId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[BudgetView]    Script Date: 12/2/2020 5:04:13 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE View [dbo].[BudgetView]
as
select 
budgetRange.RangeId,
budgetRange.StartDate,
budgetRange.EndDate,
budgetRange.AccountId,
budgetRange.SupervisorCode,
budgetRange.LastUpdated,
chartOfAccounts.Location,
chartOfAccounts.CompanyNumber,
chartOfAccounts.BusUnit,
chartOfAccounts.ObjectNumber,
chartOfAccounts.Description,
Budget.budgetHours,
Budget.budgetAmount,
Budget.actualHours,
Budget.ActualAmount,
Budget.ProjectedHours,
Budget.ProjectedAmount
 from budgetrange budgetRange
inner join chartofaccts chartOfAccounts on budgetRange.accountid=chartOfAccounts.accountid
inner join budget budget on budgetRange.rangeid=budget.rangeid
GO
/****** Object:  Table [dbo].[SalesOrderDetail]    Script Date: 12/2/2020 5:04:13 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SalesOrderDetail](
	[SalesOrderDetailId] [bigint] IDENTITY(1,1) NOT NULL,
	[SalesOrderId] [bigint] NOT NULL,
	[ItemId] [bigint] NOT NULL,
	[Description] [varchar](255) NULL,
	[Quantity] [bigint] NULL,
	[Amount] [decimal](18, 4) NOT NULL,
	[UnitOfMeasure] [varchar](25) NULL,
	[UnitPrice] [decimal](18, 4) NULL,
	[ScheduledShipDate] [datetime] NULL,
	[PromisedDate] [datetime] NULL,
	[GrossWeight] [decimal](18, 4) NULL,
	[GrossWeightUnitOfMeasure] [varchar](25) NULL,
	[ShippedDate] [datetime] NULL,
	[AccountId] [bigint] NULL,
	[LotSerial] [varchar](50) NULL,
	[BusUnit] [varchar](10) NULL,
	[LineNumber] [bigint] NULL,
	[CompanyCode] [varchar](10) NULL,
	[CarrierId] [bigint] NULL,
	[InvoiceDate] [datetime] NULL,
	[GLDate] [datetime] NULL,
	[Location] [varchar](50) NULL,
	[QuantityShipped] [bigint] NULL,
	[QuantityOpen] [bigint] NULL,
	[AmountOpen] [money] NULL,
	[PaymentTerms] [varchar](20) NULL,
	[PaymentInstrument] [varchar](50) NULL,
	[UnitVolume] [decimal](18, 4) NULL,
	[UnitVolumeUnitOfMeasurement] [varchar](50) NULL,
	[PurchaseOrderDetailId] [bigint] NULL,
	[PurchaseOrderId] [bigint] NULL,
	[PickListId] [bigint] NULL,
	[PickListDetailId] [bigint] NULL,
	[SalesOrderDetailNumber] [bigint] NULL,
 CONSTRAINT [PK_SalesOrderDetail] PRIMARY KEY CLUSTERED 
(
	[SalesOrderDetailId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[SalesOrderDetailView]    Script Date: 12/2/2020 5:04:13 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE View [dbo].[SalesOrderDetailView]
as
/****** Script for SelectTopNRows command from SSMS  ******/
SELECT SalesOrderDetail.[SalesOrderDetailID]
      ,SalesOrderDetail.[SalesOrderID]
      ,SalesOrderDetail.[ItemID]
      ,SalesOrderDetail.[Description]
      ,SalesOrderDetail.[Quantity]
      ,SalesOrderDetail.[Amount]
      ,SalesOrderDetail.[UnitOfMeasure]
      ,SalesOrderDetail.[UnitPrice]
	  ,SalesOrder.OrderNumber
	  ,SalesOrder.OrderType
  FROM SalesOrder SalesOrder
  left join [dbo].[SalesOrderDetail] SalesOrderDetail on SalesOrder.SalesOrderID=SalesOrderDetail.SalesOrderID
 
  
GO
/****** Object:  Table [dbo].[Supplier]    Script Date: 12/2/2020 5:04:13 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Supplier](
	[SupplierId] [bigint] IDENTITY(1,1) NOT NULL,
	[AddressId] [bigint] NOT NULL,
	[Identification] [varchar](50) NULL,
	[SupplierNumber] [bigint] NULL,
PRIMARY KEY CLUSTERED 
(
	[SupplierId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[SupplierView]    Script Date: 12/2/2020 5:04:13 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE View [dbo].[SupplierView]
as
select 

AB.companyname SupplierName 
,AB.Name ContactName
,AB.AddressId
,supplier.SupplierId
from addressbook AB 
	join Supplier Supplier
		on AB.AddressId=Supplier.AddressId
GO
/****** Object:  Table [dbo].[Invoice]    Script Date: 12/2/2020 5:04:13 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Invoice](
	[InvoiceId] [bigint] IDENTITY(1,1) NOT NULL,
	[InvoiceDocument] [varchar](20) NULL,
	[InvoiceDate] [date] NULL,
	[Amount] [decimal](18, 4) NULL,
	[CustomerId] [bigint] NULL,
	[Description] [varchar](2000) NULL,
	[TaxAmount] [decimal](18, 4) NULL,
	[PaymentDueDate] [date] NULL,
	[PaymentTerms] [varchar](50) NULL,
	[CompanyId] [bigint] NOT NULL,
	[DiscountDueDate] [date] NULL,
	[FreightCost] [decimal](18, 4) NULL,
	[DiscountAmount] [decimal](18, 4) NULL,
	[InvoiceNumber] [bigint] NOT NULL,
	[PurchaseOrderId] [bigint] NULL,
	[SupplierId] [bigint] NULL,
	[TaxRatesByCodeId] [bigint] NOT NULL,
 CONSTRAINT [PK_Invoices] PRIMARY KEY CLUSTERED 
(
	[InvoiceId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[AR_GL_INV_View]    Script Date: 12/2/2020 5:04:13 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create View [dbo].[AR_GL_INV_View]
as
select AR.OpenAmount,
AR.DiscountDueDate,
AR.InvoiceId,
AR.DiscountPercent,
AR.DiscountAmount,
INV.InvoiceDate,
INV.InvoiceNumber,
GL.DocNumber,
AR.PaymentTerms,
AR.Amount,
GL.DocType GLDocType,
GL.LedgerType,
GL.GLDate,
COA.Account,
AB.Name CustomerName,
AB.CompanyName

 from acctrec AR
join generalledger GL
	on ar.docNumber=GL.docNumber and GL.DocType='OV'
join ChartOfAccts COA 
	on COA.AccountId= GL.accountid
join AddressBook AB
	on GL.AddressId=AB.AddressId
join Invoice INV
	on AR.InvoiceId=INV.InvoiceId
GO
/****** Object:  Table [dbo].[Company]    Script Date: 12/2/2020 5:04:13 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Company](
	[CompanyId] [bigint] IDENTITY(1,1) NOT NULL,
	[CompanyName] [varchar](50) NULL,
	[CompanyCode] [varchar](10) NULL,
	[CompanyStreet] [varchar](100) NULL,
	[CompanyCity] [varchar](50) NULL,
	[CompanyState] [varchar](20) NULL,
	[CompanyZipcode] [varchar](20) NULL,
	[TaxCode1] [varchar](20) NULL,
	[TaxCode2] [varchar](20) NULL,
	[CompanyNumber] [bigint] NULL,
 CONSTRAINT [PK_Company] PRIMARY KEY CLUSTERED 
(
	[CompanyId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[CompanyForInvoiceView]    Script Date: 12/2/2020 5:04:13 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[CompanyForInvoiceView]
AS
SELECT        dbo.Company.CompanyName, dbo.Company.CompanyCode, dbo.Company.CompanyStreet, dbo.Company.CompanyCity, dbo.Company.CompanyState, dbo.Company.CompanyZipcode, dbo.Invoice.InvoiceId, 
                         dbo.Invoice.InvoiceNumber
FROM            dbo.Company INNER JOIN
                         dbo.Invoice ON dbo.Company.CompanyId = dbo.Invoice.CompanyId
GO
/****** Object:  Table [dbo].[UDC]    Script Date: 12/2/2020 5:04:13 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UDC](
	[XRefId] [bigint] IDENTITY(1,1) NOT NULL,
	[ProductCode] [varchar](20) NULL,
	[KeyCode] [varchar](50) NULL,
	[Value] [varchar](255) NULL,
	[UdcNumber] [bigint] NOT NULL,
 CONSTRAINT [PK__UDC__B9604F3957BD8349] PRIMARY KEY CLUSTERED 
(
	[XRefId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CustomerClaim]    Script Date: 12/2/2020 5:04:14 AM ******/
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
	[CustomerClaimNumber] [bigint] NULL,
 CONSTRAINT [PK_CustomerClaim] PRIMARY KEY CLUSTERED 
(
	[ClaimId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  View [dbo].[CustomerClaimView]    Script Date: 12/2/2020 5:04:14 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE view [dbo].[CustomerClaimView]
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
/****** Object:  Table [dbo].[Employee]    Script Date: 12/2/2020 5:04:14 AM ******/
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
	[PayRollGroupCode] [int] NULL,
	[Salary] [money] NULL,
	[HourlyRate] [money] NULL,
	[SalaryPerPayPeriod] [money] NULL,
	[EmployeeNumber] [bigint] NOT NULL,
	[PositionCodeId] [bigint] NULL,
 CONSTRAINT [PK__Employee__091C2AFB7C8C5421] PRIMARY KEY CLUSTERED 
(
	[EmployeeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[EmployeeView]    Script Date: 12/2/2020 5:04:14 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE view [dbo].[EmployeeView]
as
select
employeeid,
name employeeName,
JobTitleXrefId,
jobcodeUDC.KeyCode JobCode,
jobcodeUDC.Value JobCodeDescription
from employee employee join
addressbook addressbook on employee.addressid=addressbook.addressid 
     join udc jobcodeUDC on 
	 jobcodeUDC.XRefId=employee.JobTitleXrefId  
GO
/****** Object:  Table [dbo].[Supervisor]    Script Date: 12/2/2020 5:04:14 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Supervisor](
	[SupervisorId] [bigint] IDENTITY(1,1) NOT NULL,
	[AddressId] [bigint] NOT NULL,
	[SupervisorCode] [varchar](20) NULL,
	[JobTitleXrefId] [bigint] NULL,
	[ParentSupervisorId] [bigint] NULL,
	[IsActive] [bit] NULL,
	[Area] [varchar](20) NULL,
	[DepartmentCode] [varchar](20) NULL,
	[SupervisorNumber] [bigint] NOT NULL,
 CONSTRAINT [PK__Supervisor__091C2AFB7C8C5421] PRIMARY KEY CLUSTERED 
(
	[SupervisorId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[supervisorView]    Script Date: 12/2/2020 5:04:14 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create view [dbo].[supervisorView]
as
select
supervisorid,
supervisorcode,
name supervisorname
from supervisor supervisor join
addressbook addressbook on supervisor.addressid=addressbook.addressid
GO
/****** Object:  Table [dbo].[TimeAndAttendanceShift]    Script Date: 12/2/2020 5:04:14 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TimeAndAttendanceShift](
	[ShiftId] [bigint] IDENTITY(1,1) NOT NULL,
	[ShiftName] [char](20) NULL,
	[ShiftStartTime] [varchar](4) NULL,
	[ShiftEndTime] [varchar](4) NULL,
	[ShiftType] [varchar](50) NULL,
	[DurationHours] [int] NULL,
	[DurationMinutes] [int] NULL,
	[Monday] [bit] NULL,
	[Tuesday] [bit] NULL,
	[Wednesday] [bit] NULL,
	[Thursday] [bit] NULL,
	[Friday] [bit] NULL,
	[Saturday] [bit] NULL,
	[Sunday] [bit] NULL,
	[TimeAndAttendanceShiftNumber] [bigint] NULL,
 CONSTRAINT [PK_TimeAndAttendanceShift] PRIMARY KEY CLUSTERED 
(
	[ShiftId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TimeAndAttendancePunchIn]    Script Date: 12/2/2020 5:04:14 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TimeAndAttendancePunchIn](
	[TimePunchinId] [bigint] IDENTITY(1,1) NOT NULL,
	[PunchinDate] [date] NULL,
	[PunchinDateTime] [char](14) NULL,
	[PunchoutDateTime] [char](14) NULL,
	[JobCodeXrefId] [bigint] NOT NULL,
	[Approved] [bit] NULL,
	[EmployeeId] [bigint] NOT NULL,
	[SupervisorId] [bigint] NOT NULL,
	[ProcessedDate] [date] NULL,
	[PunchoutDate] [date] NULL,
	[Note] [varchar](255) NULL,
	[ShiftId] [bigint] NULL,
	[mealPunchin] [char](14) NULL,
	[mealPunchout] [char](14) NULL,
	[ScheduledToWork] [bit] NULL,
	[TypeOfTimeUdcXrefId] [bigint] NOT NULL,
	[ApprovingAddressId] [bigint] NOT NULL,
	[PayCodeXrefId] [bigint] NOT NULL,
	[ScheduleId] [bigint] NULL,
	[DurationInMinutes] [int] NULL,
	[MealDurationInMinutes] [int] NULL,
	[TypeOfTime] [varchar](20) NULL,
	[PayCode] [varchar](20) NULL,
	[JobCode] [varchar](20) NULL,
	[TransferJobCode] [varchar](20) NULL,
	[TransferSupervisorId] [bigint] NULL,
	[TaskStatusXRefId] [bigint] NULL,
	[TaskStatus] [varchar](20) NULL,
	[Account] [varchar](100) NULL,
	[AreaCode] [varchar](20) NULL,
	[DepartmentCode] [varchar](20) NULL,
 CONSTRAINT [PK_TimeAndAttendancePunchIn] PRIMARY KEY CLUSTERED 
(
	[TimePunchinId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TimeAndAttendanceSchedule]    Script Date: 12/2/2020 5:04:14 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TimeAndAttendanceSchedule](
	[ScheduleId] [bigint] IDENTITY(1,1) NOT NULL,
	[ScheduleName] [varchar](255) NULL,
	[StartDate] [date] NULL,
	[EndDate] [date] NULL,
	[ShiftId] [bigint] NULL,
	[ScheduleGroup] [varchar](50) NULL,
	[Monday] [bit] NULL,
	[Tuesday] [bit] NULL,
	[Wednesday] [bit] NULL,
	[Thursday] [bit] NULL,
	[Friday] [bit] NULL,
	[Saturday] [bit] NULL,
	[Sunday] [bit] NULL,
	[TimeAndAttendanceScheduleNumber] [bigint] NOT NULL,
 CONSTRAINT [PK_TimeAndAttendanceSchedule] PRIMARY KEY CLUSTERED 
(
	[ScheduleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[TimeAndAttendencePunchinView]    Script Date: 12/2/2020 5:04:14 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE view [dbo].[TimeAndAttendencePunchinView]
as
SELECT [TimePunchinId]
      ,[PunchinDate]
      ,[PunchinDateTime]
      ,[PunchoutDateTime]
      ,[JobCodeXrefId]
	  ,udcJobCode.Value JobCode
      ,[Approved]
      ,employee.[EmployeeId]
	  ,employeeAddressBook.Name EmployeeName
      ,supervisor.[SupervisorId]
	  ,supervisorAddressBook.Name SupervisorName
      ,[ProcessedDate]
      ,[PunchoutDate]
      ,[Note]
      ,taschedule.[ShiftId]
      ,[mealPunchin]
      ,[mealPunchout]
      ,[ScheduledToWork]
      ,[TypeOfTimeUdcXrefId]
	  ,udcTypeOfTime.value TypeOfTime
      ,[ApprovingAddressId]
      ,[PayCodeXrefId]
	  ,udcPayCode.value PayCode
      ,taschedule.[ScheduleId]
      ,[DurationInMinutes]
	  ,taShift.ShiftName
	  ,taShift.ShiftStartTime
	  ,taShift.ShiftEndTime
  FROM [dbo].[TimeAndAttendancePunchIn] taPunchin 
	join   udc udcTypeOfTime 
		on udctypeofTime.xrefid=taPunchin.TypeOfTimeUdcXrefId 
	join  udc udcJobCode 
		on udcjobcode.xrefid=taPunchin.JobCodeXrefId 
	left join  udc udcPayCode 
		on udcPayCode.xrefid=taPunchin.PayCodeXrefId 
	left join TimeAndAttendanceSchedule taSchedule 
		on taschedule.ScheduleId=taPunchin.ScheduleId 
	left join TimeAndAttendanceShift taShift
		on taPunchin.ShiftId=taShift.ShiftId
	join supervisor supervisor 
		on supervisor.SupervisorId=taPunchin.SupervisorId 
	join addressbook supervisorAddressbook 
		on supervisor.AddressId=supervisoraddressbook.addressid 
	join employee employee 
		on employee.employeeid = tapunchin.EmployeeId 
	join addressbook employeeaddressbook 
		on employee.AddressId=employeeaddressbook.addressid 
	join addressbook addressbookApproving 
		on addressbookApproving.AddressId=tapunchin.ApprovingAddressId
GO
/****** Object:  View [dbo].[CarrierView]    Script Date: 12/2/2020 5:04:14 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create View [dbo].[CarrierView]
as
SELECT  [CarrierId]
      ,addressBook.[AddressId]
      ,[CarrierTypeXrefId]
	  ,addressBook.CompanyName
  FROM [dbo].[Carrier] carrier
  join [dbo].[AddressBook] addressBook
	on carrier.AddressId=addressBook.AddressId
GO
/****** Object:  Table [dbo].[ShippedToAddresses]    Script Date: 12/2/2020 5:04:14 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ShippedToAddresses](
	[ShippedToAddressId] [bigint] IDENTITY(1,1) NOT NULL,
	[AddressId] [bigint] NOT NULL,
	[ShipToAddressLine1] [varchar](100) NULL,
	[ShipToAddressLine2] [varchar](100) NULL,
	[ShipToState] [varchar](50) NULL,
	[ShipToCity] [varchar](50) NULL,
	[ShipToZipcode] [varchar](50) NULL,
 CONSTRAINT [PK_ShipToAddresses] PRIMARY KEY CLUSTERED 
(
	[ShippedToAddressId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[CustomerPrimaryShippedToView]    Script Date: 12/2/2020 5:04:15 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE View [dbo].[CustomerPrimaryShippedToView]
as
select 
addressBook.name,
shippedToAddresses.[AddressId]
      ,shippedToAddresses.[ShipToAddressLine1]
      ,shippedToAddresses.[ShipToAddressLine2]
      ,shippedToAddresses.[ShipToState]
      ,shippedToAddresses.[ShipToCity]
      ,shippedToAddresses.[ShipToZipcode]
	   from addressBook addressbook
left join shippedToAddresses shippedToAddresses
on addressbook.AddressId=shippedToAddresses.shippedToAddressId
GO
/****** Object:  View [dbo].[AccountReceivableView]    Script Date: 12/2/2020 5:04:15 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

/****** Script for SelectTopNRows command from SSMS  ******/

CREATE View [dbo].[AccountReceivableView]
as

SELECT [AcctRecId]
      ,[OpenAmount]
      ,[GLDate]
      ,invoice.[InvoiceId]
	  ,invoice.InvoiceNumber
      ,[CreateDate]
      ,[DocNumber]
      ,[Remarks]
      ,customer.[CustomerId]
	  ,customerAddressbook.name CustomerName
      ,[PurchaseOrderId]
      ,acctrec.[Description]
      ,[AcctRecDocTypeXRefId]
	  ,udcDocType.Value docType
  FROM [dbo].[AcctRec] AcctRec join
  udc udcDocType on acctrec.AcctRecDocTypeXRefId=udcDocType.xrefid join
  customer customer on acctrec.customerid=customer.customerid join
  addressBook customerAddressbook on customer.AddressId=customeraddressbook.addressid join
  invoice invoice on acctrec.InvoiceId=invoice.InvoiceId

GO
/****** Object:  Table [dbo].[PhoneEntity]    Script Date: 12/2/2020 5:04:15 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PhoneEntity](
	[PhoneId] [bigint] IDENTITY(1,1) NOT NULL,
	[PhoneNumber] [varchar](50) NULL,
	[PhoneType] [varchar](10) NULL,
	[Extension] [varchar](10) NULL,
	[AddressId] [bigint] NOT NULL,
	[PhoneEntityNumber] [bigint] NULL,
 CONSTRAINT [PK_Phones] PRIMARY KEY CLUSTERED 
(
	[PhoneId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[EmailEntity]    Script Date: 12/2/2020 5:04:15 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EmailEntity](
	[EmailId] [bigint] IDENTITY(1,1) NOT NULL,
	[Password] [varchar](20) NULL,
	[LoginEmail] [bit] NULL,
	[Email] [varchar](30) NOT NULL,
	[EmailEntityNumber] [bigint] NOT NULL,
	[AddressId] [bigint] NOT NULL,
 CONSTRAINT [PK_Emails] PRIMARY KEY CLUSTERED 
(
	[EmailId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[LocationAddress]    Script Date: 12/2/2020 5:04:15 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LocationAddress](
	[LocationAddressId] [bigint] IDENTITY(1,1) NOT NULL,
	[Address Line 1] [varchar](255) NULL,
	[Address Line 2] [varchar](255) NULL,
	[City] [varchar](50) NULL,
	[Zipcode] [varchar](20) NULL,
	[TypeXRefId] [bigint] NOT NULL,
	[AddressId] [bigint] NOT NULL,
	[State] [nchar](2) NULL,
	[Country] [varchar](50) NULL,
	[Type] [varchar](20) NULL,
	[LocationAddressNumber] [bigint] NULL,
 CONSTRAINT [PK_LocationAddress] PRIMARY KEY CLUSTERED 
(
	[LocationAddressId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[CustomerView]    Script Date: 12/2/2020 5:04:15 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO






CREATE VIEW [dbo].[CustomerView] AS
SELECT Customer.CustomerId, Customer.AddressId, AddressBook.Name, 
LocationAddress.LocationAddressId, LocationAddress.[Address Line 1], 
LocationAddress.[Address Line 2], LocationAddress.City, LocationAddress.State, 
                  LocationAddress.Zipcode, LocationAddress.Country, LocationAddress.TypeXRefId, 
PhoneEntity.phonenumber,
PhoneEntity.phonetype,
EmailEntity.email,
locationType_udc.value locationType
FROM     Customer LEFT JOIN
                  LocationAddress ON Customer.AddressId = LocationAddress.AddressId LEFT OUTER JOIN
                  AddressBook ON Customer.AddressId = AddressBook.AddressId LEFT OUTER JOIN
                  phoneEntity ON phoneEntity.addressid = Customer.AddressId LEFT OUTER JOIN
                  emailEntity ON emailEntity.addressid = Customer.AddressId   LEFT OUTER join
		  udc locationType_udc on locationType_udc.xrefid=LocationAddress.TypeXRefId


GO
/****** Object:  Table [dbo].[ItemMaster]    Script Date: 12/2/2020 5:04:15 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ItemMaster](
	[ItemId] [bigint] IDENTITY(1,1) NOT NULL,
	[Description] [varchar](255) NULL,
	[UnitOfMeasure] [varchar](20) NULL,
	[CommodityCode] [varchar](10) NULL,
	[Description2] [varchar](255) NULL,
	[ItemCode] [varchar](20) NOT NULL,
	[UnitPrice] [money] NULL,
	[Branch] [varchar](50) NULL,
	[Weight] [decimal](18, 4) NULL,
	[WeightUnitOfMeasure] [varchar](50) NULL,
	[Volume] [decimal](18, 4) NULL,
	[VolumeUnitOfMeasure] [varchar](50) NULL,
	[ItemMasterNumber] [bigint] NOT NULL,
	[AccountId] [bigint] NULL,
 CONSTRAINT [PK_ItemMaster] PRIMARY KEY CLUSTERED 
(
	[ItemId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[InvoiceDetail]    Script Date: 12/2/2020 5:04:15 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[InvoiceDetail](
	[InvoiceDetailId] [bigint] IDENTITY(1,1) NOT NULL,
	[InvoiceId] [bigint] NOT NULL,
	[UnitPrice] [decimal](18, 4) NULL,
	[Quantity] [int] NULL,
	[UnitOfMeasure] [varchar](10) NULL,
	[Amount] [decimal](18, 4) NULL,
	[PurchaseOrderDetailId] [bigint] NULL,
	[SalesOrderDetailId] [bigint] NULL,
	[ItemId] [bigint] NULL,
	[DiscountPercent] [decimal](18, 4) NULL,
	[DiscountAmount] [decimal](18, 4) NULL,
	[ShipmentDetailId] [bigint] NULL,
	[ExtendedDescription] [varchar](255) NULL,
	[DiscountDueDate] [date] NULL,
	[InvoiceDetailNumber] [bigint] NOT NULL,
	[PurchaseOrderId] [bigint] NULL,
	[CustomerId] [bigint] NULL,
	[SupplierId] [bigint] NULL,
 CONSTRAINT [PK_InvoiceLineDetail] PRIMARY KEY CLUSTERED 
(
	[InvoiceDetailId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[InvoiceAndDetailAndCustomerView]    Script Date: 12/2/2020 5:04:15 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO







CREATE View [dbo].[InvoiceAndDetailAndCustomerView]
as
select 
invoice.InvoiceId,
invoice.InvoiceNumber,
invoice.InvoiceDate,
invoice.Amount,
invoice.Description,
invoice.TaxAmount,
invoice.PaymentDueDate,
invoice.PaymentTerms,
itemmaster.description ItemDescription,
itemmaster.itemnumber,
invoicesDetail.UnitPrice,
invoicesDetail.Quantity,
invoicesDetail.Amount DetailAmount,
invoicesDetail.DiscountPercent,
invoicesDetail.DiscountAmount,
invoicesDetail.ExtendedDescription DetailExtendedDescription,
invoice.companyid,
InvoiceHeader.CompanyName,
InvoiceHeader.CompanyCode,
InvoiceHeader.CompanyStreet,
InvoiceHeader.CompanyCity,
InvoiceHeader.CompanyState,
InvoiceHeader.CompanyZipcode,
InvoiceHeader.CustomerName,

InvoiceHeader.[Address Line 1] customerAddress1,
InvoiceHeader.[Address Line 2] customerAddress2,
InvoiceHeader.CustomerCity,
InvoiceHeader.CustomerState,
InvoiceHeader.CustomerZipcode,
InvoiceHeader.CustomerLocationType

from invoice invoice
left join invoiceDetail invoicesDetail on invoice.invoiceId=invoicesDetail.invoiceId
inner join itemmaster itemmaster on invoicesdetail.itemid=itemmaster.itemid
outer apply
(
SELECT 
				addressBook.Name AS CustomerName, 
				Company.CompanyName, Company.CompanyCode, 
				Company.CompanyStreet, 
				Company.CompanyCity, 
				Company.CompanyState, 
				Company.CompanyZipcode, 
				  customerLocationAddress.[Address Line 1],
				  customerLocationAddress.[Address Line 2],
				  customerLocationAddress.City CustomerCity,
				  customerLocationAddress.State customerState,
				  customerLocationAddress.Zipcode CustomerZipcode, 
				  locationTypeudc.Value  CustomerLocationType
FROM     Company company INNER JOIN
                 
                  Customer AS customer 
						ON customer.CustomerId = Invoice.CustomerId 
					JOIN AddressBook AS addressBook 
						ON customer.AddressId = addressBook.AddressId  
					left Join  LocationAddress customerLocationAddress 
						on customerLocationAddress.AddressId=customer.AddressId  
					left join  udc locationTypeUdc 
						on locationTypeUdc.xrefid=customerLocationAddress.TypeXRefId
	  
	where company.CompanyId = Invoice.CompanyId 

) InvoiceHeader


GO
/****** Object:  Table [dbo].[PurchaseOrder]    Script Date: 12/2/2020 5:04:15 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PurchaseOrder](
	[PurchaseOrderId] [bigint] IDENTITY(1,1) NOT NULL,
	[DocType] [varchar](20) NULL,
	[PaymentTerms] [varchar](10) NULL,
	[Remark] [varchar](max) NULL,
	[GLDate] [datetime] NULL,
	[AccountId] [bigint] NOT NULL,
	[SupplierId] [bigint] NULL,
	[ContractId] [bigint] NULL,
	[POQuoteId] [bigint] NULL,
	[Description] [varchar](1000) NULL,
	[PONumber] [varchar](50) NULL,
	[TakenBy] [nchar](100) NULL,
	[BuyerId] [bigint] NULL,
	[RequestedDate] [date] NULL,
	[PromisedDeliveredDate] [date] NULL,
	[Tax] [money] NULL,
	[TransactionDate] [datetime] NULL,
	[AmountPaid] [money] NULL,
	[ShippedToName] [varchar](255) NULL,
	[ShippedToAddress1] [varchar](100) NULL,
	[ShippedToAddress2] [varchar](100) NULL,
	[ShippedToCity] [varchar](50) NULL,
	[ShippedToZipcode] [varchar](20) NULL,
	[ShippedToState] [varchar](20) NULL,
	[TaxCode1] [varchar](20) NULL,
	[TaxCode2] [varchar](20) NULL,
	[PurchaseOrderNumber] [bigint] NOT NULL,
	[Amount] [money] NULL,
	[CarrierId] [bigint] NULL,
	[DiscountPercent] [decimal](18, 2) NULL,
	[DiscountAmount] [money] NULL,
	[FreightAmount] [money] NULL,
	[CustomerId] [bigint] NULL,
	[DiscountDueDate] [date] NULL,
 CONSTRAINT [PK_PurchaseOrder] PRIMARY KEY CLUSTERED 
(
	[PurchaseOrderId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Contract]    Script Date: 12/2/2020 5:04:15 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Contract](
	[ContractId] [bigint] IDENTITY(1,1) NOT NULL,
	[CustomerId] [bigint] NULL,
	[ServiceTypeXRefId] [bigint] NULL,
	[StartDate] [datetime] NULL,
	[EndDate] [datetime] NULL,
	[Cost] [money] NULL,
	[RemainingBalance] [money] NULL,
	[Title] [varchar](200) NULL,
	[ContractNumber] [bigint] NOT NULL,
 CONSTRAINT [PK__Contract__C90D34697E612150] PRIMARY KEY CLUSTERED 
(
	[ContractId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[PurchaseOrderAndDetailView]    Script Date: 12/2/2020 5:04:16 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE View [dbo].[PurchaseOrderAndDetailView]
as
select 
purchaseorder.paymentterms,
purchaseorder.grossamount,
purchaseorder.remark,
purchaseorder.gldate,
purchaseorder.accountid,
purchaseorder.supplierid,
purchaseorder.contractid,
purchaseorder.poquoteid,
purchaseorder.description,
supplier.suppliername,
supplier.suppliercompany,
contract.title,
contract.StartDate,
contract.EndDate
from purchaseorder purchaseorder
outer apply
(select supplierid, name suppliername, identification suppliercompany from supplier supplier
join addressbook addressbook on supplier.AddressId=addressbook.addressId
where purchaseorder.supplierid=addressbook.addressid) supplier

outer apply
(select contractid, title, startdate, enddate from contract contract 
where contract.ContractId=purchaseorder.ContractId) contract


GO
/****** Object:  Table [dbo].[AccountBalance]    Script Date: 12/2/2020 5:04:16 AM ******/
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
/****** Object:  View [dbo].[FinancialView]    Script Date: 12/2/2020 5:04:16 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE View [dbo].[FinancialView]
as
select 
coa.CompanyNumber,
coa.Account,
coa.Level, 
coa.BusUnit,
coa.ObjectNumber,
coa.Subsidiary, 
coa.Description, 
Sum(month.Amount) PeriodAmount,
Sum(AcctBal.amount) AcctBalAmount,
Sum(YTD.amount) YTDAmount,
month.FiscalPeriod,
month.FiscalYear 
from ChartOfAccts COA
outer apply

( select sum(amount) Amount,ab.FiscalPeriod,ab.FiscalYear from AccountBalance AB
	where  COA.accountid=AB.AccountId
	group by AB.AccountId, ab.FiscalPeriod,ab.fiscalyear
)Month
outer apply
(
select sum(amount) Amount from AccountBalance AB
	where  COA.accountid=AB.AccountId

		and cast(cast(ab.fiscalyear as varchar) + right('0'+cast(ab.fiscalPeriod as varchar),2) as int) 
		<=
	cast(cast(Month.FiscalYear as varchar) + right('0'+cast(Month.FiscalPeriod as varchar),2) as int) 

	group by ab.accountid

)AcctBal
outer apply
(
select sum(amount) Amount From AccountBalance AB
where  COA.accountid=AB.AccountId
	and ab.FiscalYear=Month.FiscalYear
	and ab.FiscalPeriod<=Month.FiscalPeriod
)YTD
group by
coa.Account, 
coa.BusUnit,
coa.ObjectNumber,
coa.Subsidiary, 
coa.Description, 
month.FiscalPeriod,
month.FiscalYear,
coa.Level,
coa.CompanyNumber
GO
/****** Object:  View [dbo].[AccountSummaryCY]    Script Date: 12/2/2020 5:04:16 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
/****** Script for SelectTopNRows command from SSMS  ******/
Create View [dbo].[AccountSummaryCY]
as
SELECT
      gl.[AccountId],
	   gl.[FiscalPeriod],
	   gl.[FiscalYear],
	  coa.Description,
	  sum(Amount) Amount
  FROM [dbo].[GeneralLedger] gl 
	join [dbo].[ChartOfAccts] coa
		on gl.AccountId=coa.AccountId
  where ledgertype='AA' and DocType='PV' and FiscalYear=datepart(Year,getDate())
  group by
        gl.[AccountId],
		 gl. [FiscalPeriod],
	 gl. [FiscalYear],
	  coa.Description
GO
/****** Object:  View [dbo].[AccountReceivableOpenView]    Script Date: 12/2/2020 5:04:16 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE View [dbo].[AccountReceivableOpenView]
as
select 
ar.OpenAmount
,ar.GLDate
,ar.InvoiceId
,inv.invoiceNumber
,inv.description InvoiceDescription
,ar.DocNumber
,ar.Remarks
,ar.PaymentTerms
,ar.DiscountDueDate
,ar.CustomerId
,abCust.Name
,la.[Address Line 1]
,la.[Address Line 2]
,la.City
,la.State
,la.Zipcode
,coa.Account
,coa.Description CoaDescription
,(select sum(amount) from GeneralLedger gl 
where gl.AccountId=ar.AccountId and gl.DocNumber=ar.DocNumber) GLAmount
,(select max(checknumber) from GeneralLedger gl
where gl.AccountId=ar.AccountId and gl.DocNumber=ar.DocNumber) LastCheckNumber
from acctrec ar
join Invoice inv
	on ar.invoiceid=inv.invoiceid
join Customer cust
	on ar.CustomerId=cust.CustomerId
join udc udcDocType
	on ar.AcctRecDocTypeXRefId=udcDocType.XRefId
join AddressBook abCust
	on cust.AddressId=abCust.AddressId
join LocationAddress la
	on abCust.AddressId=la.AddressId
join ChartOfAccts coa
	on ar.AccountId=coa.AccountId
where ar.OpenAmount>0
GO
/****** Object:  View [dbo].[ChartOfAccountView]    Script Date: 12/2/2020 5:04:16 AM ******/
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
/****** Object:  View [dbo].[personalExpenseView]    Script Date: 12/2/2020 5:04:16 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE view [dbo].[personalExpenseView]
as
select coa.accountid,
coa.location,
coa.busUnit,
coa.objectNumber,
coa.supcode,
coa.Subsidiary,
coa.subsub,
coa.Account,
coa.Description,
coa.CompanyNumber,
bud.BudgetAmount,
bud.BudgetHours,
bud_range.StartDate,
bud_range.EndDate
 from chartofaccts coa
join budget bud
	on coa.Accountid=bud.AccountId
join budgetrange bud_range
	on bud.AccountId=bud_range.AccountId
	and bud_range.IsActive=1

where coa.objectnumber between 501 and 599


GO
/****** Object:  View [dbo].[TableColumnsView]    Script Date: 12/2/2020 5:04:16 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create View [dbo].[TableColumnsView]
as
select col1.TABLE_NAME
,col1.COLUMN_NAME 
,col1.DATA_TYPE
from INFORMATION_SCHEMA.COLUMNS col1
where 
1=1
--col1.TABLE_NAME='PackingSlip'
--and col1.IS_NULLABLE='NO'
and not exists
(
select col2.COLUMN_NAME
from INFORMATION_SCHEMA.KEY_COLUMN_USAGE col2
where col2.TABLE_NAME=col1.TABLE_NAME
AND col2.TABLE_SCHEMA=col1.TABLE_SCHEMA
and col2.COLUMN_NAME=col1.COLUMN_NAME
)
GO
/****** Object:  Table [dbo].[__MigrationHistory]    Script Date: 12/2/2020 5:04:16 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[__MigrationHistory](
	[MigrationId] [nvarchar](150) NOT NULL,
	[ContextKey] [nvarchar](300) NOT NULL,
	[Model] [varbinary](max) NOT NULL,
	[ProductVersion] [nvarchar](32) NOT NULL,
 CONSTRAINT [PK_dbo.__MigrationHistory] PRIMARY KEY CLUSTERED 
(
	[MigrationId] ASC,
	[ContextKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AccountPayable]    Script Date: 12/2/2020 5:04:16 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AccountPayable](
	[AccountPayableId] [bigint] IDENTITY(1,1) NOT NULL,
	[DocNumber] [bigint] NULL,
	[GrossAmount] [money] NULL,
	[DiscountAmount] [money] NULL,
	[Remark] [varchar](max) NULL,
	[GLDate] [datetime] NULL,
	[SupplierId] [bigint] NOT NULL,
	[ContractId] [bigint] NULL,
	[POQuoteId] [bigint] NULL,
	[Description] [varchar](1000) NULL,
	[PurchaseOrderId] [bigint] NULL,
	[Tax] [money] NULL,
	[AccountId] [bigint] NOT NULL,
	[DocType] [varchar](20) NOT NULL,
	[PaymentTerms] [varchar](20) NULL,
	[DiscountPercent] [decimal](18, 3) NULL,
	[AmountOpen] [money] NULL,
	[OrderNumber] [varchar](50) NULL,
	[DiscountDueDate] [date] NULL,
	[AmountPaid] [money] NULL,
	[AccountPayableNumber] [bigint] NOT NULL,
 CONSTRAINT [PK_AcctPay] PRIMARY KEY CLUSTERED 
(
	[AccountPayableId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AccountPayableDetail]    Script Date: 12/2/2020 5:04:16 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AccountPayableDetail](
	[AccountPayableDetailId] [bigint] IDENTITY(1,1) NOT NULL,
	[InvoiceId] [bigint] NOT NULL,
	[InvoiceDetailId] [bigint] NOT NULL,
	[UnitPrice] [decimal](18, 4) NULL,
	[Quantity] [int] NULL,
	[QuantityReceived] [int] NULL,
	[Amount] [decimal](18, 4) NULL,
	[AmountPaid] [decimal](18, 4) NULL,
	[PurchaseOrderDetailId] [bigint] NULL,
	[SalesOrderDetailId] [bigint] NULL,
	[ItemId] [bigint] NULL,
	[ExtendedDescription] [varchar](255) NULL,
	[AccountPayableDetailNumber] [bigint] NOT NULL,
	[PurchaseOrderId] [bigint] NULL,
	[CustomerId] [bigint] NULL,
	[SupplierId] [bigint] NULL,
	[AccountPayableId] [bigint] NULL,
 CONSTRAINT [PK_AccountPayableDetail] PRIMARY KEY CLUSTERED 
(
	[AccountPayableDetailId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AccountReceivable]    Script Date: 12/2/2020 5:04:16 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AccountReceivable](
	[AccountReceivableId] [bigint] IDENTITY(1,1) NOT NULL,
	[OpenAmount] [money] NULL,
	[DiscountDueDate] [date] NULL,
	[GLDate] [date] NULL,
	[InvoiceId] [bigint] NULL,
	[CreateDate] [date] NULL,
	[DocNumber] [bigint] NULL,
	[Remark] [varchar](255) NULL,
	[PaymentTerms] [varchar](50) NULL,
	[CustomerId] [bigint] NOT NULL,
	[PurchaseOrderId] [bigint] NULL,
	[Description] [varchar](255) NULL,
	[AcctRecDocTypeXRefId] [bigint] NOT NULL,
	[AccountId] [bigint] NOT NULL,
	[Amount] [money] NULL,
	[DebitAmount] [money] NULL,
	[CreditAmount] [money] NULL,
	[DiscountPercent] [decimal](18, 2) NULL,
	[DiscountAmount] [money] NULL,
	[AcctRecDocType] [varchar](20) NULL,
	[InterestPaid] [money] NULL,
	[LateFee] [money] NULL,
	[AccountReceivableNumber] [bigint] NOT NULL,
	[CustomerPurchaseOrder] [varchar](50) NULL,
	[Tax] [money] NULL,
	[PaymentDueDate] [datetime] NULL,
 CONSTRAINT [PK__AcctRec__4B67207728200668] PRIMARY KEY CLUSTERED 
(
	[AccountReceivableId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AccountReceivableDetail]    Script Date: 12/2/2020 5:04:16 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AccountReceivableDetail](
	[AccountReceivableDetailId] [bigint] IDENTITY(1,1) NOT NULL,
	[InvoiceId] [bigint] NOT NULL,
	[AccountReceivableId] [bigint] NOT NULL,
	[UnitPrice] [decimal](18, 4) NULL,
	[Quantity] [int] NULL,
	[UnitOfMeasure] [varchar](10) NULL,
	[Amount] [decimal](18, 4) NULL,
	[AmountReceived] [decimal](18, 4) NULL,
	[PurchaseOrderDetailId] [bigint] NULL,
	[SalesOrderDetailId] [bigint] NULL,
	[ItemId] [bigint] NULL,
	[AccountReceivableDetailNumber] [bigint] NOT NULL,
	[PurchaseOrderId] [bigint] NULL,
	[CustomerId] [bigint] NULL,
	[SupplierId] [bigint] NULL,
	[QuantityDelivered] [bigint] NULL,
	[Comment] [varchar](255) NULL,
	[TypeOfPayment] [varchar](20) NULL,
	[InvoiceDetailId] [bigint] NOT NULL,
 CONSTRAINT [PK_AccountReceivableDetail] PRIMARY KEY CLUSTERED 
(
	[AccountReceivableDetailId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AccountReceivableFee]    Script Date: 12/2/2020 5:04:16 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AccountReceivableFee](
	[AccountReceivableFeeId] [bigint] IDENTITY(1,1) NOT NULL,
	[FeeAmount] [money] NULL,
	[PaymentDueDate] [date] NULL,
	[CustomerId] [bigint] NOT NULL,
	[DocNumber] [bigint] NOT NULL,
	[AcctRecDocType] [varchar](20) NOT NULL,
	[AccountReceivableId] [bigint] NOT NULL,
	[AccountReceivableFeeNumber] [bigint] NOT NULL,
 CONSTRAINT [PK_AcctRecFee] PRIMARY KEY CLUSTERED 
(
	[AccountReceivableFeeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AccountReceivableInterest]    Script Date: 12/2/2020 5:04:17 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AccountReceivableInterest](
	[AcctRecInterestId] [bigint] IDENTITY(1,1) NOT NULL,
	[Amount] [money] NULL,
	[InterestRate] [money] NULL,
	[InterestFromDate] [date] NULL,
	[InterestToDate] [date] NULL,
	[DocNumber] [bigint] NOT NULL,
	[PaymentTerms] [varchar](50) NULL,
	[PaymentDueDate] [date] NULL,
	[CustomerId] [bigint] NOT NULL,
	[AcctRecDocType] [varchar](20) NOT NULL,
	[LastInterestDueDate] [date] NULL,
	[AccountReceivableId] [bigint] NOT NULL,
	[AccountReceivableInterestNumber] [bigint] NOT NULL,
 CONSTRAINT [PK_AcctRecInterest] PRIMARY KEY CLUSTERED 
(
	[AcctRecInterestId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetRoles]    Script Date: 12/2/2020 5:04:17 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetRoles](
	[Id] [nvarchar](128) NOT NULL,
	[Name] [nvarchar](256) NOT NULL,
 CONSTRAINT [PK_dbo.AspNetRoles] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUserClaims]    Script Date: 12/2/2020 5:04:17 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserClaims](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [nvarchar](128) NOT NULL,
	[ClaimType] [nvarchar](max) NULL,
	[ClaimValue] [nvarchar](max) NULL,
 CONSTRAINT [PK_dbo.AspNetUserClaims] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUserLogins]    Script Date: 12/2/2020 5:04:17 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserLogins](
	[LoginProvider] [nvarchar](128) NOT NULL,
	[ProviderKey] [nvarchar](128) NOT NULL,
	[UserId] [nvarchar](128) NOT NULL,
 CONSTRAINT [PK_dbo.AspNetUserLogins] PRIMARY KEY CLUSTERED 
(
	[LoginProvider] ASC,
	[ProviderKey] ASC,
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUserRoles]    Script Date: 12/2/2020 5:04:17 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserRoles](
	[UserId] [nvarchar](128) NOT NULL,
	[RoleId] [nvarchar](128) NOT NULL,
 CONSTRAINT [PK_dbo.AspNetUserRoles] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUsers]    Script Date: 12/2/2020 5:04:17 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUsers](
	[Id] [nvarchar](128) NOT NULL,
	[Email] [nvarchar](256) NULL,
	[EmailConfirmed] [bit] NOT NULL,
	[PasswordHash] [nvarchar](max) NULL,
	[SecurityStamp] [nvarchar](max) NULL,
	[PhoneNumber] [nvarchar](max) NULL,
	[PhoneNumberConfirmed] [bit] NOT NULL,
	[TwoFactorEnabled] [bit] NOT NULL,
	[LockoutEndDateUtc] [datetime] NULL,
	[LockoutEnabled] [bit] NOT NULL,
	[AccessFailedCount] [int] NOT NULL,
	[UserName] [nvarchar](256) NOT NULL,
 CONSTRAINT [PK_dbo.AspNetUsers] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Asset]    Script Date: 12/2/2020 5:04:17 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Asset](
	[AssetId] [bigint] IDENTITY(1,1) NOT NULL,
	[AssetCode] [varchar](50) NULL,
	[TagCode] [varchar](50) NULL,
	[ClassCode] [varchar](50) NULL,
	[Description] [varchar](255) NULL,
	[Manufacturer] [varchar](100) NULL,
	[Model] [varchar](50) NULL,
	[SerialNumber] [varchar](50) NULL,
	[AcquiredDate] [date] NULL,
	[OriginalCost] [decimal](18, 2) NULL,
	[ReplacementCost] [decimal](18, 2) NULL,
	[Depreciation] [decimal](18, 2) NULL,
	[Location] [varchar](50) NULL,
	[SubLocation] [varchar](50) NULL,
	[Quantity] [int] NULL,
	[EquipmentStatusXRefId] [bigint] NOT NULL,
	[GenericLocationLevel1] [varchar](50) NULL,
	[GenericLocationLevel2] [varchar](50) NULL,
	[GenericLocationLevel3] [varchar](50) NULL,
	[AssetNumber] [bigint] NOT NULL,
 CONSTRAINT [PK_Assets] PRIMARY KEY CLUSTERED 
(
	[AssetId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[BudgetNote]    Script Date: 12/2/2020 5:04:18 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BudgetNote](
	[BudgetNoteId] [bigint] IDENTITY(1,1) NOT NULL,
	[BudgetId] [bigint] NOT NULL,
	[Note] [ntext] NOT NULL,
	[Create] [datetime] NOT NULL,
	[BudgetNoteNumber] [bigint] NOT NULL,
 CONSTRAINT [PK_BudgetNote] PRIMARY KEY CLUSTERED 
(
	[BudgetNoteId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[BudgetSnapShot]    Script Date: 12/2/2020 5:04:18 AM ******/
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
/****** Object:  Table [dbo].[ChartOfAccount]    Script Date: 12/2/2020 5:04:18 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ChartOfAccount](
	[AccountId] [bigint] IDENTITY(1,1) NOT NULL,
	[Location] [varchar](10) NULL,
	[BusUnit] [varchar](10) NULL,
	[Subsidiary] [varchar](10) NULL,
	[SubSub] [varchar](10) NULL,
	[Account] [varchar](30) NULL,
	[Description] [varchar](255) NULL,
	[CompanyCode] [varchar](10) NULL,
	[GenCode] [varchar](3) NULL,
	[SubCode] [varchar](3) NULL,
	[ObjectNumber] [varchar](20) NULL,
	[SupCode] [varchar](10) NULL,
	[ThirdAccount] [varchar](20) NULL,
	[CategoryCode1] [varchar](10) NULL,
	[CategoryCode2] [varchar](10) NULL,
	[CategoryCode3] [varchar](10) NULL,
	[PostEditCode] [varchar](10) NULL,
	[CompanyId] [bigint] NOT NULL,
	[Level] [int] NOT NULL,
	[ChartOfAccountNumber] [bigint] NOT NULL,
 CONSTRAINT [PK__chartOfA__349DA5A6F015CCF2] PRIMARY KEY CLUSTERED 
(
	[AccountId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Comment]    Script Date: 12/2/2020 5:04:18 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Comment](
	[CommentId] [bigint] IDENTITY(1,1) NOT NULL,
	[EntityId] [bigint] NOT NULL,
	[EntityType] [varchar](50) NOT NULL,
	[CommentContent] [varchar](max) NULL,
	[CommentNumber] [bigint] NOT NULL,
 CONSTRAINT [PK_Comments] PRIMARY KEY CLUSTERED 
(
	[CommentId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ContractInvoice]    Script Date: 12/2/2020 5:04:18 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ContractInvoice](
	[ContractInvoiceId] [bigint] IDENTITY(1,1) NOT NULL,
	[ContractId] [bigint] NOT NULL,
	[InvoiceId] [bigint] NOT NULL,
	[ContractInvoiceNumber] [bigint] NOT NULL,
 CONSTRAINT [PK_ContractInvoice] PRIMARY KEY CLUSTERED 
(
	[ContractInvoiceId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ContractItem]    Script Date: 12/2/2020 5:04:18 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ContractItem](
	[ContractItemId] [bigint] IDENTITY(1,1) NOT NULL,
	[ContractId] [bigint] NOT NULL,
	[WBS] [varchar](50) NULL,
	[ItemDescription] [varchar](2000) NULL,
	[UnitOfMeasure] [varchar](50) NULL,
	[Quantity] [int] NULL,
	[UnitPrice] [money] NULL,
	[ExtendedCost] [money] NULL,
	[Fees] [money] NULL,
	[ContractType] [varchar](10) NULL,
	[PaymentMethod] [varchar](50) NULL,
	[DurationHours] [int] NULL,
	[EstimatedStartDate] [date] NULL,
	[EstimatedEndDate] [date] NULL,
	[ContractItemNumber] [bigint] NOT NULL,
 CONSTRAINT [PK_ContractContent] PRIMARY KEY CLUSTERED 
(
	[ContractItemId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CustomerLedger]    Script Date: 12/2/2020 5:04:19 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CustomerLedger](
	[CustomerLedgerId] [bigint] IDENTITY(1,1) NOT NULL,
	[CustomerId] [bigint] NOT NULL,
	[InvoiceId] [bigint] NOT NULL,
	[AccountReceivableId] [bigint] NOT NULL,
	[Amount] [money] NULL,
	[GLDate] [date] NULL,
	[AccountId] [bigint] NOT NULL,
	[GeneralLedgerId] [bigint] NOT NULL,
	[DocNumber] [bigint] NOT NULL,
	[Comment] [varchar](255) NULL,
	[AddressId] [bigint] NOT NULL,
	[CreatedDate] [datetime] NULL,
	[DocType] [varchar](50) NOT NULL,
	[DebitAmount] [money] NULL,
	[CreditAmount] [money] NULL,
	[FiscalYear] [int] NOT NULL,
	[FiscalPeriod] [int] NOT NULL,
	[CheckNumber] [varchar](20) NULL,
	[CustomerLedgerNumber] [bigint] NOT NULL,
 CONSTRAINT [PK_CustomerLedger] PRIMARY KEY CLUSTERED 
(
	[CustomerLedgerId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[EmployeePosition]    Script Date: 12/2/2020 5:04:19 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EmployeePosition](
	[PositionCodeId] [bigint] IDENTITY(1,1) NOT NULL,
	[PositionCode] [varchar](20) NOT NULL,
	[CompanyId] [bigint] NOT NULL,
	[Description] [varchar](100) NULL,
	[PositionCodeNumber] [bigint] NOT NULL,
 CONSTRAINT [PK_EmployeePosition] PRIMARY KEY CLUSTERED 
(
	[PositionCodeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[EmployeeSalary]    Script Date: 12/2/2020 5:04:19 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EmployeeSalary](
	[EmployeeSalaryId] [bigint] IDENTITY(1,1) NOT NULL,
	[Employee] [bigint] NOT NULL,
	[AnnualSalary] [money] NOT NULL,
	[StartEffectiveDate] [date] NOT NULL,
	[EndEffectiveDate] [date] NOT NULL,
 CONSTRAINT [PK_EmployeeSalary] PRIMARY KEY CLUSTERED 
(
	[EmployeeSalaryId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Equations]    Script Date: 12/2/2020 5:04:19 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Equations](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[equation] [varchar](255) NULL,
	[queueid] [varchar](20) NULL,
	[evaluated] [varchar](255) NULL,
	[cellname] [varchar](10) NULL,
 CONSTRAINT [PK_equations] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Equipment]    Script Date: 12/2/2020 5:04:19 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Equipment](
	[EquipmentId] [bigint] IDENTITY(1,1) NOT NULL,
	[Model] [varchar](100) NULL,
	[Make] [varchar](50) NULL,
	[VIN] [varchar](200) NULL,
	[PurchasePrice] [money] NULL,
	[CurrentAppraisalPrice] [money] NULL,
	[SalesPrice] [money] NULL,
	[Description] [varchar](max) NULL,
	[SaleOption] [varchar](20) NULL,
	[YearPurchased] [int] NULL,
	[LocationCity] [varchar](100) NULL,
	[LocationState] [varchar](2) NULL,
	[Category1] [varchar](20) NULL,
	[Category2] [varchar](20) NULL,
	[Category3] [varchar](20) NULL,
	[EquipmentNumber] [bigint] NOT NULL,
 CONSTRAINT [PK_Equipment] PRIMARY KEY CLUSTERED 
(
	[EquipmentId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[HumanResourcesSalary]    Script Date: 12/2/2020 5:04:20 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[HumanResourcesSalary](
	[HumanResourcesSalaryId] [bigint] IDENTITY(1,1) NOT NULL,
	[Employee] [bigint] NOT NULL,
	[AnnualizedSalary] [money] NULL,
	[HourlyRate] [money] NULL,
	[EffectiveDate] [date] NOT NULL,
 CONSTRAINT [PK_HumanResourcesSalary] PRIMARY KEY CLUSTERED 
(
	[HumanResourcesSalaryId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Inventory]    Script Date: 12/2/2020 5:04:20 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Inventory](
	[InventoryId] [bigint] IDENTITY(1,1) NOT NULL,
	[Description] [varchar](100) NULL,
	[Remarks] [varchar](2000) NULL,
	[UnitOfMeasure] [varchar](100) NULL,
	[Quantity] [int] NULL,
	[ExtendedPrice] [money] NULL,
	[DistributionAccountId] [bigint] NULL,
	[PackingSlipDetailId] [bigint] NULL,
	[ItemId] [bigint] NOT NULL,
	[Branch] [varchar](50) NULL,
	[InventoryNumber] [bigint] NOT NULL,
 CONSTRAINT [PK_Inventory] PRIMARY KEY CLUSTERED 
(
	[InventoryId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[JobChangeOrder]    Script Date: 12/2/2020 5:04:21 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[JobChangeOrder](
	[JobChangeOrderId] [bigint] IDENTITY(1,1) NOT NULL,
	[JobMasterId] [bigint] NOT NULL,
	[ContractId] [bigint] NOT NULL,
	[ContractItemId] [bigint] NOT NULL,
	[Description] [varchar](1000) NULL,
	[InvoiceId] [bigint] NULL,
	[AcctRecId] [bigint] NULL,
	[CustomerId] [bigint] NULL,
	[ChangeAmount] [money] NOT NULL,
	[EstimatedAmount] [money] NULL,
	[JobChangeOrderNumber] [bigint] NOT NULL,
 CONSTRAINT [PK_JobChangeOrder] PRIMARY KEY CLUSTERED 
(
	[JobChangeOrderId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[JobCostLedger]    Script Date: 12/2/2020 5:04:21 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[JobCostLedger](
	[JobCostLedgerId] [bigint] IDENTITY(1,1) NOT NULL,
	[JobMasterId] [bigint] NOT NULL,
	[ContractId] [bigint] NOT NULL,
	[EstimatedHours] [decimal](18, 2) NULL,
	[EstimatedAmount] [money] NULL,
	[JobPhaseId] [bigint] NULL,
	[ActualHours] [decimal](18, 2) NULL,
	[ActualCost] [money] NULL,
	[ProjectedHours] [decimal](18, 2) NULL,
	[ProjectedAmount] [money] NULL,
	[CommittedHours] [decimal](18, 2) NULL,
	[CommittedAmount] [money] NULL,
	[Description] [varchar](255) NULL,
	[TransactionType] [varchar](20) NULL,
	[Source] [varchar](100) NULL,
	[JobCostLedgerNumber] [bigint] NOT NULL,
	[CustomerId] [bigint] NULL,
	[JobCostTypeId] [bigint] NOT NULL,
	[SupplierId] [bigint] NULL,
	[PurchaseOrderId] [bigint] NULL,
	[InvoiceId] [bigint] NULL,
	[TaxAmount] [money] NULL,
 CONSTRAINT [PK_JobCostDetail] PRIMARY KEY CLUSTERED 
(
	[JobCostLedgerId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[JobCostType]    Script Date: 12/2/2020 5:04:21 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[JobCostType](
	[JobCostTypeId] [bigint] IDENTITY(1,1) NOT NULL,
	[CostCode] [varchar](20) NULL,
	[Description] [varchar](100) NULL,
	[Account] [varchar](50) NULL,
	[JobCostTypeNumber] [bigint] NOT NULL,
 CONSTRAINT [PK_JobCostType] PRIMARY KEY CLUSTERED 
(
	[JobCostTypeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[JobMaster]    Script Date: 12/2/2020 5:04:21 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[JobMaster](
	[JobMasterId] [bigint] IDENTITY(1,1) NOT NULL,
	[ContractId] [bigint] NOT NULL,
	[JobDescription] [varchar](255) NULL,
	[Address1] [varchar](100) NULL,
	[Address2] [varchar](100) NULL,
	[ProjectManagerId] [bigint] NULL,
	[City] [varchar](50) NULL,
	[State] [varchar](20) NULL,
	[Zipcode] [varchar](20) NULL,
	[StartDate] [date] NULL,
	[CompleteDate] [date] NULL,
	[TotalCommittedAmount] [money] NULL,
	[ActualAmount] [money] NULL,
	[ActualHours] [decimal](18, 2) NULL,
	[EstimatedAmount] [money] NULL,
	[EstimatedHours] [decimal](18, 2) NULL,
	[ProjectedAmount] [money] NULL,
	[ProjectHours] [decimal](18, 2) NULL,
	[RemainingCommittedAmount] [money] NULL,
	[RetainageAmount] [money] NULL,
	[JobMasterNumber] [bigint] NOT NULL,
	[CustomerId] [bigint] NOT NULL,
 CONSTRAINT [PK_JobMaster] PRIMARY KEY CLUSTERED 
(
	[JobMasterId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[JobPhase]    Script Date: 12/2/2020 5:04:21 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[JobPhase](
	[JobPhaseId] [bigint] IDENTITY(1,1) NOT NULL,
	[PhaseGroup] [int] NOT NULL,
	[JobMasterId] [bigint] NOT NULL,
	[Phase] [varchar](100) NOT NULL,
	[ContractId] [bigint] NOT NULL,
	[JobPhaseNumber] [bigint] NOT NULL,
	[JobCostTypeId] [bigint] NOT NULL,
 CONSTRAINT [PK_JobPhaseId] PRIMARY KEY CLUSTERED 
(
	[JobPhaseId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[NetTerms]    Script Date: 12/2/2020 5:04:21 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[NetTerms](
	[NetTermId] [bigint] IDENTITY(1,1) NOT NULL,
	[NetTerms] [varchar](50) NULL,
	[DiscountPercent] [decimal](18, 2) NULL,
 CONSTRAINT [PK_NetTerms] PRIMARY KEY CLUSTERED 
(
	[NetTermId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[NextNumber]    Script Date: 12/2/2020 5:04:22 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[NextNumber](
	[NextNumberId] [bigint] IDENTITY(1,1) NOT NULL,
	[NextNumberName] [varchar](255) NULL,
	[NextNumberValue] [bigint] NOT NULL,
 CONSTRAINT [PK_NextNumber] PRIMARY KEY CLUSTERED 
(
	[NextNumberId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PackingSlip]    Script Date: 12/2/2020 5:04:22 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PackingSlip](
	[PackingSlipId] [bigint] IDENTITY(1,1) NOT NULL,
	[SupplierId] [bigint] NOT NULL,
	[ReceivedDate] [datetime] NOT NULL,
	[SlipDocument] [varchar](50) NULL,
	[PONumber] [varchar](50) NULL,
	[Remark] [varchar](max) NULL,
	[SlipType] [varchar](20) NULL,
	[Amount] [money] NULL,
	[PackingSlipNumber] [bigint] NOT NULL,
 CONSTRAINT [PK_Receipt] PRIMARY KEY CLUSTERED 
(
	[PackingSlipId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PackingSlipDetail]    Script Date: 12/2/2020 5:04:22 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PackingSlipDetail](
	[PackingSlipDetailId] [bigint] IDENTITY(1,1) NOT NULL,
	[PackingSlipId] [bigint] NOT NULL,
	[ItemId] [bigint] NOT NULL,
	[Quantity] [int] NULL,
	[UnitPrice] [decimal](18, 4) NULL,
	[ExtendedCost] [decimal](18, 4) NULL,
	[UnitOfMeasure] [varchar](20) NULL,
	[Description] [varchar](200) NULL,
	[PackingSlipDetailNumber] [bigint] NOT NULL,
 CONSTRAINT [PK_PackingSlipDetail] PRIMARY KEY CLUSTERED 
(
	[PackingSlipDetailId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PayRollBenefitOption]    Script Date: 12/2/2020 5:04:23 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PayRollBenefitOption](
	[PayRollBenefitOptionId] [bigint] IDENTITY(1,1) NOT NULL,
	[PayRollBenefitOption] [int] NOT NULL,
	[Description] [varchar](200) NULL,
 CONSTRAINT [PK_PayRollBenefitOption] PRIMARY KEY CLUSTERED 
(
	[PayRollBenefitOptionId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PayRollCurrentPaySequence]    Script Date: 12/2/2020 5:04:23 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PayRollCurrentPaySequence](
	[PayRollCurrentPaySequenceId] [bigint] IDENTITY(1,1) NOT NULL,
	[PaySequence] [bigint] NOT NULL,
	[PayRollCurrentPaySequenceNumber] [bigint] NOT NULL,
	[PayRollCode] [bigint] NOT NULL,
	[PayRollBeginDate] [date] NOT NULL,
	[PayRollEndDate] [date] NOT NULL,
	[Frequency] [varchar](50) NOT NULL,
	[Active] [bit] NOT NULL,
 CONSTRAINT [PK_PayRollCurrentPaySequence] PRIMARY KEY CLUSTERED 
(
	[PayRollCurrentPaySequenceId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PayRollDeductionLiabilities]    Script Date: 12/2/2020 5:04:23 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PayRollDeductionLiabilities](
	[PayRollDeductionLiabilitiesId] [bigint] IDENTITY(1,1) NOT NULL,
	[DeductionLiabilitiesCode] [int] NOT NULL,
	[Amount] [money] NULL,
	[Percentage] [money] NULL,
	[Description] [varchar](255) NOT NULL,
	[DeductionLiabilitiesType] [varchar](10) NOT NULL,
	[LimitAmount] [money] NULL,
	[PayRollDeductionLiabilitiesNumber] [bigint] NOT NULL,
 CONSTRAINT [PK_PayRollDeductionLiabilities] PRIMARY KEY CLUSTERED 
(
	[PayRollDeductionLiabilitiesId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PayRollEarnings]    Script Date: 12/2/2020 5:04:23 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PayRollEarnings](
	[PayRollEarningsId] [bigint] IDENTITY(1,1) NOT NULL,
	[EarningCode] [int] NOT NULL,
	[Description] [varchar](255) NOT NULL,
	[EarningType] [varchar](10) NULL,
	[PayRollEarningsNumber] [bigint] NOT NULL,
 CONSTRAINT [PK_PayRollEarnings] PRIMARY KEY CLUSTERED 
(
	[PayRollEarningsId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PayRollEmployeeBenefit]    Script Date: 12/2/2020 5:04:23 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PayRollEmployeeBenefit](
	[PayrollEmployeeBenefitsId] [bigint] IDENTITY(1,1) NOT NULL,
	[Employee] [bigint] NOT NULL,
	[BenefitCode] [varchar](50) NOT NULL,
	[Amount] [money] NOT NULL,
	[TransactionCode] [bigint] NOT NULL,
	[Percentage] [money] NULL,
	[Frequency] [varchar](50) NOT NULL,
	[BenefitOption] [int] NULL,
 CONSTRAINT [PK_PayRollEmployeeBenefit] PRIMARY KEY CLUSTERED 
(
	[PayrollEmployeeBenefitsId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PayRollEmployeeDeductionLiabilities]    Script Date: 12/2/2020 5:04:24 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PayRollEmployeeDeductionLiabilities](
	[PayRollEmployeeDeductionLiabilitiesId] [bigint] IDENTITY(1,1) NOT NULL,
	[Employee] [bigint] NOT NULL,
	[DeductionLiabilitiesCode] [int] NOT NULL,
	[Amount] [money] NULL,
	[Percentage] [money] NULL,
	[Description] [varchar](255) NOT NULL,
	[DeductionLiabilitiesType] [varchar](10) NOT NULL,
	[LimitAmount] [money] NULL,
	[BenefitOption] [int] NULL,
	[Frequency] [varchar](50) NULL,
	[PayRollEmployeeDeductionLiabilitiesNumber] [bigint] NOT NULL,
 CONSTRAINT [PK_PayRollEmployeeDeductionLiabilities] PRIMARY KEY CLUSTERED 
(
	[PayRollEmployeeDeductionLiabilitiesId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PayRollEmployeeEarnings]    Script Date: 12/2/2020 5:04:24 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PayRollEmployeeEarnings](
	[PayRollEmployeeEarningsId] [bigint] IDENTITY(1,1) NOT NULL,
	[Employee] [bigint] NOT NULL,
	[EarningCode] [int] NOT NULL,
	[Description] [varchar](255) NOT NULL,
	[EarningType] [varchar](10) NULL,
	[Amount] [money] NOT NULL,
 CONSTRAINT [PK_PayEmployeeRollEarnings] PRIMARY KEY CLUSTERED 
(
	[PayRollEmployeeEarningsId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PayRollEmployeeSequence]    Script Date: 12/2/2020 5:04:24 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PayRollEmployeeSequence](
	[EmployeePaySequenceId] [bigint] IDENTITY(1,1) NOT NULL,
	[Employee] [bigint] NOT NULL,
	[PayRollBeginDate] [date] NOT NULL,
	[PayRollEndDate] [date] NOT NULL,
	[PaySequence] [bigint] NOT NULL,
	[PayRollCode] [int] NOT NULL,
 CONSTRAINT [PK_PayRollEmployeeSequence] PRIMARY KEY CLUSTERED 
(
	[EmployeePaySequenceId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PayRollGroup]    Script Date: 12/2/2020 5:04:24 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PayRollGroup](
	[PayRollGroupId] [bigint] IDENTITY(1,1) NOT NULL,
	[PayRollGroupCode] [int] NOT NULL,
	[Description] [varchar](200) NULL,
	[PayRollGroupNumber] [bigint] NOT NULL,
 CONSTRAINT [PK_PayRollGroup] PRIMARY KEY CLUSTERED 
(
	[PayRollGroupId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PayRollInsurance]    Script Date: 12/2/2020 5:04:25 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PayRollInsurance](
	[PayRollInsuranceId] [bigint] NOT NULL,
	[InsuranceCode] [varchar](50) NOT NULL,
	[Description] [varchar](255) NOT NULL,
	[EligibleAmount] [money] NOT NULL,
	[Rate] [money] NOT NULL,
	[Employee] [bigint] NOT NULL,
 CONSTRAINT [PK_PayRollInsurance] PRIMARY KEY CLUSTERED 
(
	[PayRollInsuranceId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PayRollLedger]    Script Date: 12/2/2020 5:04:25 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PayRollLedger](
	[PayRollLedgerID] [bigint] IDENTITY(1,1) NOT NULL,
	[EmployeeId] [bigint] NOT NULL,
	[PayRollTransactionCode] [bigint] NOT NULL,
	[PayRollType] [varchar](20) NOT NULL,
	[Amount] [money] NOT NULL,
	[PaidDate] [date] NOT NULL,
	[PayPeriodBegin] [datetime] NOT NULL,
	[PayPeriodEnd] [datetime] NOT NULL,
	[PayRollGroupCode] [int] NOT NULL,
	[ReversingEntry] [varchar](1) NULL,
	[UpdateEntry] [varchar](1) NULL,
	[PayRollLedgerNumber] [bigint] NOT NULL,
	[PaySequence] [bigint] NOT NULL,
	[TransactionType] [varchar](1) NOT NULL,
 CONSTRAINT [PK_PayRollLedger] PRIMARY KEY CLUSTERED 
(
	[PayRollLedgerID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PayRollPaySequence]    Script Date: 12/2/2020 5:04:25 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PayRollPaySequence](
	[PayRollPaySequenceId] [bigint] IDENTITY(1,1) NOT NULL,
	[PaySequence] [bigint] NOT NULL,
	[PayRollBeginDate] [date] NOT NULL,
	[PayRollEndDate] [date] NOT NULL,
	[Frequency] [varchar](50) NOT NULL,
	[PayRollGroupCode] [int] NOT NULL,
	[PayRollPaySequenceNumber] [bigint] NOT NULL,
 CONSTRAINT [PK_PayRollPaySequence] PRIMARY KEY CLUSTERED 
(
	[PayRollPaySequenceId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [PayRollPaySequenceConstraint] UNIQUE NONCLUSTERED 
(
	[PaySequence] ASC,
	[PayRollBeginDate] ASC,
	[PayRollEndDate] ASC,
	[PayRollGroupCode] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PayRollTotals]    Script Date: 12/2/2020 5:04:25 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PayRollTotals](
	[PayRollTotalsId] [bigint] IDENTITY(1,1) NOT NULL,
	[Employee] [bigint] NOT NULL,
	[EarningCode] [int] NULL,
	[EarningType] [varchar](10) NULL,
	[DeductionLiabilitiesCode] [int] NULL,
	[DeductionLiabilitiesType] [varchar](10) NULL,
	[Amount] [money] NULL,
	[PayRollGroupCode] [int] NOT NULL,
	[PaySeqence] [bigint] NOT NULL,
	[PayRollBeginDate] [date] NOT NULL,
	[PayRollEndDate] [date] NOT NULL,
	[PayRollTotalsNumber] [bigint] NOT NULL,
 CONSTRAINT [PK_PayRollTotals] PRIMARY KEY CLUSTERED 
(
	[PayRollTotalsId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PayRollTransactionControl]    Script Date: 12/2/2020 5:04:25 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PayRollTransactionControl](
	[PayRollTransactionControlId] [bigint] IDENTITY(1,1) NOT NULL,
	[Description] [varchar](max) NULL,
	[CompanyCode] [varchar](10) NULL,
	[PayRollType] [varchar](20) NULL,
	[RateAmount] [money] NULL,
	[RateType] [varchar](50) NULL,
	[PayRollTransactionCode] [int] NOT NULL,
	[UpperLimit1] [money] NULL,
	[UpperLimit2] [money] NULL,
	[PayRollTransactionControlNumber] [bigint] NULL,
 CONSTRAINT [PK_PayRollBenefit] PRIMARY KEY CLUSTERED 
(
	[PayRollTransactionControlId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PayRollTransactionsByEmployee]    Script Date: 12/2/2020 5:04:25 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PayRollTransactionsByEmployee](
	[PayRollTransactionsByEmployeeId] [bigint] IDENTITY(1,1) NOT NULL,
	[Employee] [bigint] NOT NULL,
	[PayRollTransactionCode] [bigint] NOT NULL,
	[Amount] [money] NOT NULL,
	[TaxPercentOfGross] [money] NULL,
	[AdditionalAmount] [money] NULL,
	[PayRollGroupCode] [int] NULL,
	[BenefitOption] [int] NULL,
	[PayRollTransactionsByEmployeeNumber] [bigint] NULL,
	[PayRollType] [varchar](20) NULL,
	[TransactionType] [varchar](1) NULL,
 CONSTRAINT [PK_PayRollTransactionsByEmployee] PRIMARY KEY CLUSTERED 
(
	[PayRollTransactionsByEmployeeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PayRollTransactionTypes]    Script Date: 12/2/2020 5:04:25 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PayRollTransactionTypes](
	[PayRollTransactionTypesId] [bigint] IDENTITY(1,1) NOT NULL,
	[PayRollTranactionCode] [int] NOT NULL,
	[Description] [varchar](255) NOT NULL,
	[PayRollTransactionTypesNumber] [bigint] NOT NULL,
 CONSTRAINT [PK_PayRollTransactionControl] PRIMARY KEY CLUSTERED 
(
	[PayRollTransactionTypesId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PayRollW4]    Script Date: 12/2/2020 5:04:25 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PayRollW4](
	[PayRollW4Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Allowances] [int] NOT NULL,
	[Employee] [bigint] NOT NULL,
	[Married] [bit] NULL,
	[Single] [bit] NULL,
	[PayFrequency] [varchar](50) NOT NULL,
 CONSTRAINT [PK_PayRollW4] PRIMARY KEY CLUSTERED 
(
	[PayRollW4Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[POQuote]    Script Date: 12/2/2020 5:04:26 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[POQuote](
	[POQuoteId] [bigint] IDENTITY(1,1) NOT NULL,
	[QuoteAmount] [money] NULL,
	[SubmittedDate] [date] NULL,
	[PurchaseOrderId] [bigint] NOT NULL,
	[Remarks] [varchar](255) NULL,
	[CustomerId] [bigint] NOT NULL,
	[SupplierId] [bigint] NOT NULL,
	[SKU] [varchar](50) NULL,
	[Description] [varchar](255) NULL,
	[PoquoteNumber] [bigint] NOT NULL,
 CONSTRAINT [PK__POQuote__13714E72D8039EDD] PRIMARY KEY CLUSTERED 
(
	[POQuoteId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ProjectManagementMilestone]    Script Date: 12/2/2020 5:04:26 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProjectManagementMilestone](
	[MilestoneId] [bigint] IDENTITY(1,1) NOT NULL,
	[MilestoneName] [varchar](255) NULL,
	[ProjectId] [bigint] NULL,
	[EstimatedHours] [decimal](18, 2) NULL,
	[ActualDays] [int] NULL,
	[EstimatedDays] [int] NULL,
	[ActualHours] [decimal](18, 2) NULL,
	[ActualStartDate] [datetime] NULL,
	[ActualEndDate] [datetime] NULL,
	[EstimatedStartDate] [datetime] NULL,
	[EstimatedEndDate] [datetime] NULL,
	[Cost] [decimal](18, 2) NULL,
	[WBS] [varchar](50) NULL,
	[MileStoneNumber] [bigint] NULL,
 CONSTRAINT [PK_ProjectManagementMilestones] PRIMARY KEY CLUSTERED 
(
	[MilestoneId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ProjectManagementProject]    Script Date: 12/2/2020 5:04:26 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProjectManagementProject](
	[ProjectId] [bigint] IDENTITY(1,1) NOT NULL,
	[ProjectName] [varchar](255) NULL,
	[Version] [varchar](50) NULL,
	[Description] [varchar](2000) NULL,
	[ActualHours] [decimal](18, 2) NULL,
	[ActualStartDate] [datetime] NULL,
	[ActualEndDate] [datetime] NULL,
	[EstimatedStartDate] [datetime] NULL,
	[EstimatedHours] [decimal](18, 2) NULL,
	[EstimatedEndDate] [datetime] NULL,
	[Cost] [decimal](18, 2) NULL,
	[ActualDays] [int] NULL,
	[EstimatedDays] [int] NULL,
	[BudgetAmount] [money] NULL,
	[BudgetHours] [decimal](18, 2) NULL,
	[ProjectNumber] [bigint] NULL,
 CONSTRAINT [PK_ProjectManagementProject] PRIMARY KEY CLUSTERED 
(
	[ProjectId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ProjectManagementTask]    Script Date: 12/2/2020 5:04:26 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProjectManagementTask](
	[TaskId] [bigint] IDENTITY(1,1) NOT NULL,
	[WBS] [varchar](50) NULL,
	[TaskName] [varchar](255) NULL,
	[Description] [varchar](2000) NULL,
	[EstimatedStartDate] [datetime] NULL,
	[EstimatedHours] [decimal](18, 2) NULL,
	[EstimatedEndDate] [datetime] NULL,
	[ActualStartDate] [datetime] NULL,
	[ActualHours] [decimal](18, 2) NULL,
	[ActualEndDate] [datetime] NULL,
	[Cost] [decimal](18, 2) NULL,
	[MileStoneId] [bigint] NOT NULL,
	[StatusXrefId] [bigint] NOT NULL,
	[EstimatedCost] [decimal](18, 2) NULL,
	[ActualDays] [int] NULL,
	[EstimatedDays] [int] NULL,
	[ProjectId] [bigint] NOT NULL,
	[AccountNumber] [varchar](100) NULL,
	[WorkOrderId] [bigint] NULL,
	[TaskNumber] [bigint] NULL,
 CONSTRAINT [PK_ProjectManagementTask] PRIMARY KEY CLUSTERED 
(
	[TaskId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ProjectManagementTaskToEmployee]    Script Date: 12/2/2020 5:04:26 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProjectManagementTaskToEmployee](
	[TaskToEmployeeId] [bigint] IDENTITY(1,1) NOT NULL,
	[EmployeeId] [bigint] NOT NULL,
	[TaskId] [bigint] NOT NULL,
	[TaskToEmployeeNumber] [bigint] NOT NULL,
 CONSTRAINT [PK_ProjectManagementTaskToEmployee] PRIMARY KEY CLUSTERED 
(
	[TaskToEmployeeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ProjectManagementWorkOrder]    Script Date: 12/2/2020 5:04:26 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProjectManagementWorkOrder](
	[WorkOrderId] [bigint] IDENTITY(1,1) NOT NULL,
	[Description] [varchar](100) NULL,
	[StartDate] [datetime] NULL,
	[EndDate] [datetime] NULL,
	[ActualAmount] [money] NULL,
	[ActualHours] [decimal](18, 2) NULL,
	[EstimatedAmount] [money] NULL,
	[EstimatedHours] [decimal](18, 2) NULL,
	[AccountNumber] [varchar](50) NULL,
	[Instructions] [varchar](2000) NULL,
	[ProjectId] [bigint] NOT NULL,
	[Status] [varchar](20) NULL,
	[Location] [varchar](200) NULL,
	[WorkOrderNumber] [bigint] NULL,
	[AccountId] [bigint] NOT NULL,
 CONSTRAINT [PK_ProjectManagementWorkOrder] PRIMARY KEY CLUSTERED 
(
	[WorkOrderId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ProjectManagementWorkOrderToEmployee]    Script Date: 12/2/2020 5:04:26 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProjectManagementWorkOrderToEmployee](
	[WorkOrderToEmployeeId] [bigint] IDENTITY(1,1) NOT NULL,
	[EmployeeId] [bigint] NOT NULL,
	[WorkOrderId] [bigint] NOT NULL,
	[WorkOrderToEmployeeNumber] [bigint] NOT NULL,
 CONSTRAINT [PK_ProjectManagementWorkOrderToEmployee] PRIMARY KEY CLUSTERED 
(
	[WorkOrderToEmployeeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ProjectManager]    Script Date: 12/2/2020 5:04:27 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProjectManager](
	[ProjectManagerId] [bigint] IDENTITY(1,1) NOT NULL,
	[AddressBookId] [bigint] NOT NULL,
 CONSTRAINT [PK_ProjectManager] PRIMARY KEY CLUSTERED 
(
	[ProjectManagerId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PurchaseOrderDetail]    Script Date: 12/2/2020 5:04:27 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PurchaseOrderDetail](
	[PurchaseOrderDetailId] [bigint] IDENTITY(1,1) NOT NULL,
	[PurchaseOrderId] [bigint] NOT NULL,
	[Amount] [decimal](18, 4) NULL,
	[OrderedQuantity] [decimal](18, 4) NULL,
	[UnitPrice] [decimal](18, 4) NULL,
	[UnitOfMeasure] [varchar](10) NULL,
	[ReceivedDate] [date] NULL,
	[ExpectedDeliveryDate] [date] NULL,
	[OrderDate] [date] NULL,
	[ReceivedQuantity] [int] NULL,
	[RemainingQuantity] [int] NULL,
	[LineDescription] [varchar](255) NULL,
	[PurchaseOrderDetailNumber] [bigint] NOT NULL,
	[DiscountAmount] [money] NULL,
	[InvoiceId] [bigint] NULL,
	[LineNumber] [bigint] NOT NULL,
	[SupplierId] [bigint] NULL,
	[CustomerId] [bigint] NULL,
	[ItemId] [bigint] NULL,
 CONSTRAINT [PK_PurchaseOrderDetail] PRIMARY KEY CLUSTERED 
(
	[PurchaseOrderDetailId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ScheduleEvent]    Script Date: 12/2/2020 5:04:27 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ScheduleEvent](
	[ScheduleEventId] [bigint] IDENTITY(1,1) NOT NULL,
	[EmployeeId] [bigint] NOT NULL,
	[EventDateTime] [datetime] NULL,
	[ServiceId] [bigint] NOT NULL,
	[DurationMinutes] [bigint] NULL,
	[CustomerId] [bigint] NULL,
	[ScheduleEventNumber] [bigint] NOT NULL,
 CONSTRAINT [PK__Schedule__9EA964918AAF2100] PRIMARY KEY CLUSTERED 
(
	[ScheduleEventId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ServiceInformation]    Script Date: 12/2/2020 5:04:27 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ServiceInformation](
	[ServiceId] [bigint] IDENTITY(1,1) NOT NULL,
	[ServiceDescription] [varchar](255) NULL,
	[Price] [money] NULL,
	[AddOns] [varchar](1000) NULL,
	[ServiceTypeXRefId] [bigint] NULL,
	[CreatedDate] [datetime] NULL,
	[LocationId] [bigint] NOT NULL,
	[CustomerId] [bigint] NOT NULL,
	[ContractId] [bigint] NOT NULL,
	[SquareFeetOfStructure] [int] NULL,
	[LocationDescription] [varchar](255) NULL,
	[LocationGPS] [varchar](255) NULL,
	[Comments] [varchar](1000) NULL,
	[Status] [bit] NOT NULL,
	[ServiceInformationNumber] [bigint] NOT NULL,
 CONSTRAINT [PK__ServiceI__C51BB00AAC530496] PRIMARY KEY CLUSTERED 
(
	[ServiceId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ServiceInformationInvoice]    Script Date: 12/2/2020 5:04:27 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ServiceInformationInvoice](
	[ServiceInformationInvoiceId] [bigint] IDENTITY(1,1) NOT NULL,
	[InvoiceId] [bigint] NOT NULL,
	[ServiceId] [bigint] NOT NULL,
	[ServiceInformationInvoiceNumber] [bigint] NOT NULL,
 CONSTRAINT [PK_ServiceInformationInvoice] PRIMARY KEY CLUSTERED 
(
	[ServiceInformationInvoiceId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Shipment]    Script Date: 12/2/2020 5:04:27 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Shipment](
	[ShipmentId] [bigint] IDENTITY(1,1) NOT NULL,
	[ShipmentDate] [datetime] NULL,
	[CustomerId] [bigint] NOT NULL,
	[CarrierId] [bigint] NOT NULL,
	[TrackingNumber] [varchar](50) NULL,
	[ActualWeight] [decimal](18, 4) NULL,
	[BillableWeight] [decimal](18, 4) NULL,
	[Duty] [decimal](18, 4) NULL,
	[Tax] [decimal](18, 4) NULL,
	[ShippingCost] [decimal](18, 4) NULL,
	[Amount] [decimal](18, 4) NULL,
	[CodAmount] [decimal](18, 4) NULL,
	[ShippedFromLocationId] [bigint] NOT NULL,
	[ShippedToLocationId] [bigint] NULL,
	[PurchaseOrderId] [bigint] NULL,
	[VendorInvoiceId] [bigint] NULL,
	[VendorShippingCost] [decimal](18, 4) NULL,
	[VendorHandlingCost] [decimal](18, 4) NULL,
	[OrderNumber] [varchar](20) NOT NULL,
	[OrderType] [varchar](20) NOT NULL,
	[WeightUOM] [varchar](20) NOT NULL,
	[SalesOrderId] [bigint] NULL,
	[ShipmentNumber] [bigint] NOT NULL,
 CONSTRAINT [PK_Shipments] PRIMARY KEY CLUSTERED 
(
	[ShipmentId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ShipmentDetail]    Script Date: 12/2/2020 5:04:27 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ShipmentDetail](
	[ShipmentDetailId] [bigint] IDENTITY(1,1) NOT NULL,
	[ShipmentId] [bigint] NOT NULL,
	[ItemId] [bigint] NOT NULL,
	[Quantity] [bigint] NULL,
	[Amount] [money] NULL,
	[SalesOrderDetailId] [bigint] NOT NULL,
	[InvoiceDetailId] [bigint] NULL,
	[ShipmentDetailNumber] [bigint] NOT NULL,
	[QuantityShipped] [bigint] NULL,
	[AmountShipped] [money] NULL,
	[Note] [varchar](max) NULL,
	[UnitPrice] [money] NULL,
	[Weight] [decimal](18, 4) NULL,
	[WeightUnitOfMeasure] [varchar](50) NULL,
	[Volume] [decimal](18, 4) NULL,
	[VolumeUnitOfMeasure] [varchar](50) NULL,
	[ShippedDate] [datetime] NULL,
 CONSTRAINT [PK_ShipmentDetail] PRIMARY KEY CLUSTERED 
(
	[ShipmentDetailId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SupervisorEmployees]    Script Date: 12/2/2020 5:04:27 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SupervisorEmployees](
	[SupervisorEmployeesId] [bigint] IDENTITY(1,1) NOT NULL,
	[SupervisorId] [bigint] NOT NULL,
	[EmployeeId] [bigint] NOT NULL,
 CONSTRAINT [PK_SupervisorEmployees] PRIMARY KEY CLUSTERED 
(
	[SupervisorEmployeesId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SupplierInvoice]    Script Date: 12/2/2020 5:04:28 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SupplierInvoice](
	[SupplierInvoiceId] [bigint] IDENTITY(1,1) NOT NULL,
	[SupplierInvoiceNumber] [bigint] NOT NULL,
	[SupplierInvoiceDate] [date] NULL,
	[PONumber] [varchar](50) NULL,
	[Amount] [decimal](18, 4) NULL,
	[Description] [varchar](2000) NULL,
	[TaxAmount] [decimal](18, 4) NULL,
	[PaymentDueDate] [date] NULL,
	[PaymentTerms] [varchar](50) NULL,
	[DiscountDueDate] [date] NULL,
	[SupplierId] [bigint] NOT NULL,
	[FreightCost] [decimal](18, 4) NULL,
	[DiscountAmount] [decimal](18, 4) NULL,
	[InvoiceDocument] [varchar](50) NULL,
	[PurchaseOrderId] [bigint] NOT NULL,
	[InvoiceId] [bigint] NULL,
 CONSTRAINT [PK_SupplierInvoices] PRIMARY KEY CLUSTERED 
(
	[SupplierInvoiceId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SupplierInvoiceDetail]    Script Date: 12/2/2020 5:04:28 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SupplierInvoiceDetail](
	[SupplierInvoiceDetailId] [bigint] IDENTITY(1,1) NOT NULL,
	[SupplierInvoiceId] [bigint] NOT NULL,
	[UnitPrice] [decimal](18, 4) NULL,
	[Quantity] [int] NULL,
	[UnitOfMeasure] [varchar](10) NULL,
	[ExtendedCost] [decimal](18, 4) NULL,
	[ItemId] [bigint] NOT NULL,
	[Description] [varchar](255) NULL,
	[DiscountDueDate] [date] NULL,
	[DiscountAmount] [decimal](18, 4) NULL,
	[DiscountPercent] [decimal](18, 4) NULL,
	[SupplierInvoiceDetailNumber] [bigint] NOT NULL,
	[InvoiceId] [bigint] NULL,
	[InvoiceDetailId] [bigint] NULL,
 CONSTRAINT [PK_SupplierInvoiceLineDetail] PRIMARY KEY CLUSTERED 
(
	[SupplierInvoiceDetailId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SupplierLedger]    Script Date: 12/2/2020 5:04:28 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SupplierLedger](
	[SupplierLedgerId] [bigint] IDENTITY(1,1) NOT NULL,
	[SupplierId] [bigint] NOT NULL,
	[InvoiceId] [bigint] NOT NULL,
	[AcctPayId] [bigint] NOT NULL,
	[Amount] [money] NULL,
	[GLDate] [date] NULL,
	[AccountId] [bigint] NOT NULL,
	[GeneralLedgerId] [bigint] NOT NULL,
	[DocNumber] [bigint] NOT NULL,
	[Comment] [varchar](255) NULL,
	[AddressId] [bigint] NOT NULL,
	[CreatedDate] [datetime] NULL,
	[DocType] [varchar](50) NOT NULL,
	[DebitAmount] [money] NULL,
	[CreditAmount] [money] NULL,
	[FiscalYear] [int] NOT NULL,
	[FiscalPeriod] [int] NOT NULL,
	[SupplierLedgerNumber] [bigint] NOT NULL,
 CONSTRAINT [PK_SupplierLedger] PRIMARY KEY CLUSTERED 
(
	[SupplierLedgerId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TaxRatesByCode]    Script Date: 12/2/2020 5:04:28 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TaxRatesByCode](
	[TaxRatesByCodeId] [bigint] IDENTITY(1,1) NOT NULL,
	[TaxCode] [varchar](20) NULL,
	[TaxRate] [money] NULL,
	[State] [varchar](2) NULL,
	[TaxRatesByCodeNumber] [bigint] NOT NULL,
 CONSTRAINT [PK_TaxRatesByCode] PRIMARY KEY CLUSTERED 
(
	[TaxRatesByCodeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TimeAndAttendanceScheduledToWork]    Script Date: 12/2/2020 5:04:28 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TimeAndAttendanceScheduledToWork](
	[ScheduledToWorkId] [bigint] IDENTITY(1,1) NOT NULL,
	[EmployeeId] [bigint] NOT NULL,
	[ScheduleId] [bigint] NOT NULL,
	[ScheduleName] [varchar](255) NULL,
	[StartDateTime] [varchar](16) NULL,
	[EndDateTime] [varchar](16) NULL,
	[StartDate] [date] NULL,
	[EndDate] [date] NULL,
	[EmployeeName] [varchar](255) NULL,
	[ShiftId] [bigint] NOT NULL,
	[JobCode] [varchar](20) NULL,
	[PayCode] [varchar](20) NULL,
	[WorkedJobCode] [varchar](20) NULL,
	[ScheduledToWorkNumber] [bigint] NOT NULL,
	[JobCodeXrefId] [bigint] NULL,
	[PayCodeXrefId] [bigint] NULL,
	[WorkedJobCodeXrefId] [bigint] NULL,
	[Note] [varchar](255) NULL,
 CONSTRAINT [PK_TimeAndAttendanceScheduledToWork] PRIMARY KEY CLUSTERED 
(
	[ScheduledToWorkId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TimeAndAttendanceSetup]    Script Date: 12/2/2020 5:04:28 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TimeAndAttendanceSetup](
	[TimeAndAttendanceSetupId] [bigint] IDENTITY(1,1) NOT NULL,
	[TimeZone] [varchar](50) NULL,
	[DaylightSavings] [bit] NULL,
	[Offset] [int] NULL,
	[TimeAndAttendanceSetupNumber] [bigint] NOT NULL,
 CONSTRAINT [PK_TimeAndAttendanceSetup] PRIMARY KEY CLUSTERED 
(
	[TimeAndAttendanceSetupId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[WorkFlow]    Script Date: 12/2/2020 5:04:28 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[WorkFlow](
	[WorkflowId] [bigint] IDENTITY(1,1) NOT NULL,
	[DataEntityId] [bigint] NOT NULL,
	[DataEntityType] [varchar](100) NULL,
	[NextStep] [varchar](50) NULL,
	[PreviousStep] [varchar](50) NULL,
	[Sequence] [bigint] NULL,
	[PreviousWorkflowId] [bigint] NOT NULL,
 CONSTRAINT [PK_WorkFlow] PRIMARY KEY CLUSTERED 
(
	[WorkflowId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[BudgetNote] ADD  CONSTRAINT [DF_BudgetNote_Create]  DEFAULT (getdate()) FOR [Create]
GO
ALTER TABLE [dbo].[CustomerClaim] ADD  CONSTRAINT [DF_CustomerClaim_CreatedDate]  DEFAULT (getdate()) FOR [CreatedDate]
GO
ALTER TABLE [dbo].[NextNumber] ADD  CONSTRAINT [DF_NextNumber_NextNumberValue]  DEFAULT ((1)) FOR [NextNumberValue]
GO
ALTER TABLE [dbo].[AccountBalance]  WITH CHECK ADD  CONSTRAINT [FK_AccountBalance_ChartOfAccts] FOREIGN KEY([AccountId])
REFERENCES [dbo].[ChartOfAccount] ([AccountId])
GO
ALTER TABLE [dbo].[AccountBalance] CHECK CONSTRAINT [FK_AccountBalance_ChartOfAccts]
GO
ALTER TABLE [dbo].[AccountPayable]  WITH CHECK ADD  CONSTRAINT [FK__AcctPay__Purchas__031C6FA4] FOREIGN KEY([PurchaseOrderId])
REFERENCES [dbo].[PurchaseOrder] ([PurchaseOrderId])
GO
ALTER TABLE [dbo].[AccountPayable] CHECK CONSTRAINT [FK__AcctPay__Purchas__031C6FA4]
GO
ALTER TABLE [dbo].[AccountPayable]  WITH CHECK ADD  CONSTRAINT [FK_AcctPay_ChartOfAccts] FOREIGN KEY([AccountId])
REFERENCES [dbo].[ChartOfAccount] ([AccountId])
GO
ALTER TABLE [dbo].[AccountPayable] CHECK CONSTRAINT [FK_AcctPay_ChartOfAccts]
GO
ALTER TABLE [dbo].[AccountPayable]  WITH CHECK ADD  CONSTRAINT [FK_AcctPay_Contract] FOREIGN KEY([ContractId])
REFERENCES [dbo].[Contract] ([ContractId])
GO
ALTER TABLE [dbo].[AccountPayable] CHECK CONSTRAINT [FK_AcctPay_Contract]
GO
ALTER TABLE [dbo].[AccountPayable]  WITH CHECK ADD  CONSTRAINT [FK_AcctPay_POQuote] FOREIGN KEY([POQuoteId])
REFERENCES [dbo].[POQuote] ([POQuoteId])
GO
ALTER TABLE [dbo].[AccountPayable] CHECK CONSTRAINT [FK_AcctPay_POQuote]
GO
ALTER TABLE [dbo].[AccountPayable]  WITH CHECK ADD  CONSTRAINT [FK_AcctPay_Supplier] FOREIGN KEY([SupplierId])
REFERENCES [dbo].[Supplier] ([SupplierId])
GO
ALTER TABLE [dbo].[AccountPayable] CHECK CONSTRAINT [FK_AcctPay_Supplier]
GO
ALTER TABLE [dbo].[AccountPayableDetail]  WITH CHECK ADD  CONSTRAINT [FK_AccountPayableDetail_AccountPayable] FOREIGN KEY([AccountPayableId])
REFERENCES [dbo].[AccountPayable] ([AccountPayableId])
GO
ALTER TABLE [dbo].[AccountPayableDetail] CHECK CONSTRAINT [FK_AccountPayableDetail_AccountPayable]
GO
ALTER TABLE [dbo].[AccountReceivable]  WITH CHECK ADD  CONSTRAINT [FK_AcctRec_ChartOfAccts] FOREIGN KEY([AccountId])
REFERENCES [dbo].[ChartOfAccount] ([AccountId])
GO
ALTER TABLE [dbo].[AccountReceivable] CHECK CONSTRAINT [FK_AcctRec_ChartOfAccts]
GO
ALTER TABLE [dbo].[AccountReceivable]  WITH CHECK ADD  CONSTRAINT [FK_AcctRec_Customer] FOREIGN KEY([CustomerId])
REFERENCES [dbo].[Customer] ([CustomerId])
GO
ALTER TABLE [dbo].[AccountReceivable] CHECK CONSTRAINT [FK_AcctRec_Customer]
GO
ALTER TABLE [dbo].[AccountReceivable]  WITH CHECK ADD  CONSTRAINT [FK_AcctRec_Invoices] FOREIGN KEY([InvoiceId])
REFERENCES [dbo].[Invoice] ([InvoiceId])
GO
ALTER TABLE [dbo].[AccountReceivable] CHECK CONSTRAINT [FK_AcctRec_Invoices]
GO
ALTER TABLE [dbo].[AccountReceivable]  WITH CHECK ADD  CONSTRAINT [FK_AcctRec_UDC] FOREIGN KEY([AcctRecDocTypeXRefId])
REFERENCES [dbo].[UDC] ([XRefId])
GO
ALTER TABLE [dbo].[AccountReceivable] CHECK CONSTRAINT [FK_AcctRec_UDC]
GO
ALTER TABLE [dbo].[AccountReceivableDetail]  WITH CHECK ADD  CONSTRAINT [FK_AccountReceivableDetail_AccountReceivable] FOREIGN KEY([AccountReceivableId])
REFERENCES [dbo].[AccountReceivable] ([AccountReceivableId])
GO
ALTER TABLE [dbo].[AccountReceivableDetail] CHECK CONSTRAINT [FK_AccountReceivableDetail_AccountReceivable]
GO
ALTER TABLE [dbo].[AccountReceivableFee]  WITH CHECK ADD  CONSTRAINT [FK_AcctRecFee_AcctRec] FOREIGN KEY([AccountReceivableId])
REFERENCES [dbo].[AccountReceivable] ([AccountReceivableId])
GO
ALTER TABLE [dbo].[AccountReceivableFee] CHECK CONSTRAINT [FK_AcctRecFee_AcctRec]
GO
ALTER TABLE [dbo].[AccountReceivableFee]  WITH CHECK ADD  CONSTRAINT [FK_AcctRecFee_Customer] FOREIGN KEY([CustomerId])
REFERENCES [dbo].[Customer] ([CustomerId])
GO
ALTER TABLE [dbo].[AccountReceivableFee] CHECK CONSTRAINT [FK_AcctRecFee_Customer]
GO
ALTER TABLE [dbo].[AccountReceivableInterest]  WITH CHECK ADD  CONSTRAINT [FK_AcctRecInterest_AcctRec] FOREIGN KEY([AccountReceivableId])
REFERENCES [dbo].[AccountReceivable] ([AccountReceivableId])
GO
ALTER TABLE [dbo].[AccountReceivableInterest] CHECK CONSTRAINT [FK_AcctRecInterest_AcctRec]
GO
ALTER TABLE [dbo].[AccountReceivableInterest]  WITH CHECK ADD  CONSTRAINT [FK_AcctRecInterest_Customer] FOREIGN KEY([CustomerId])
REFERENCES [dbo].[Customer] ([CustomerId])
GO
ALTER TABLE [dbo].[AccountReceivableInterest] CHECK CONSTRAINT [FK_AcctRecInterest_Customer]
GO
ALTER TABLE [dbo].[AspNetUserClaims]  WITH CHECK ADD  CONSTRAINT [FK_dbo.AspNetUserClaims_dbo.AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserClaims] CHECK CONSTRAINT [FK_dbo.AspNetUserClaims_dbo.AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[AspNetUserLogins]  WITH CHECK ADD  CONSTRAINT [FK_dbo.AspNetUserLogins_dbo.AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserLogins] CHECK CONSTRAINT [FK_dbo.AspNetUserLogins_dbo.AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[AspNetUserRoles]  WITH CHECK ADD  CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.AspNetRoles_RoleId] FOREIGN KEY([RoleId])
REFERENCES [dbo].[AspNetRoles] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserRoles] CHECK CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.AspNetRoles_RoleId]
GO
ALTER TABLE [dbo].[AspNetUserRoles]  WITH CHECK ADD  CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserRoles] CHECK CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[Asset]  WITH CHECK ADD  CONSTRAINT [FK_Assets_UDC] FOREIGN KEY([EquipmentStatusXRefId])
REFERENCES [dbo].[UDC] ([XRefId])
GO
ALTER TABLE [dbo].[Asset] CHECK CONSTRAINT [FK_Assets_UDC]
GO
ALTER TABLE [dbo].[Budget]  WITH CHECK ADD  CONSTRAINT [FK_Budget_BudgetRange] FOREIGN KEY([RangeId])
REFERENCES [dbo].[BudgetRange] ([RangeId])
GO
ALTER TABLE [dbo].[Budget] CHECK CONSTRAINT [FK_Budget_BudgetRange]
GO
ALTER TABLE [dbo].[Budget]  WITH CHECK ADD  CONSTRAINT [FK_Budget_ChartOfAccts] FOREIGN KEY([AccountId])
REFERENCES [dbo].[ChartOfAccount] ([AccountId])
GO
ALTER TABLE [dbo].[Budget] CHECK CONSTRAINT [FK_Budget_ChartOfAccts]
GO
ALTER TABLE [dbo].[BudgetNote]  WITH CHECK ADD  CONSTRAINT [FK_BudgetNote_Budget] FOREIGN KEY([BudgetId])
REFERENCES [dbo].[Budget] ([BudgetId])
GO
ALTER TABLE [dbo].[BudgetNote] CHECK CONSTRAINT [FK_BudgetNote_Budget]
GO
ALTER TABLE [dbo].[BudgetRange]  WITH CHECK ADD  CONSTRAINT [FK_BudgetRange_ChartOfAccts] FOREIGN KEY([AccountId])
REFERENCES [dbo].[ChartOfAccount] ([AccountId])
GO
ALTER TABLE [dbo].[BudgetRange] CHECK CONSTRAINT [FK_BudgetRange_ChartOfAccts]
GO
ALTER TABLE [dbo].[Buyer]  WITH CHECK ADD  CONSTRAINT [FK_Buyer_AddressBook] FOREIGN KEY([AddressId])
REFERENCES [dbo].[AddressBook] ([AddressId])
GO
ALTER TABLE [dbo].[Buyer] CHECK CONSTRAINT [FK_Buyer_AddressBook]
GO
ALTER TABLE [dbo].[Carrier]  WITH CHECK ADD  CONSTRAINT [FK__Carrier__Carrier__668030F6] FOREIGN KEY([CarrierTypeXrefId])
REFERENCES [dbo].[UDC] ([XRefId])
GO
ALTER TABLE [dbo].[Carrier] CHECK CONSTRAINT [FK__Carrier__Carrier__668030F6]
GO
ALTER TABLE [dbo].[Carrier]  WITH CHECK ADD  CONSTRAINT [FK_Carier_AddressBook] FOREIGN KEY([AddressId])
REFERENCES [dbo].[AddressBook] ([AddressId])
GO
ALTER TABLE [dbo].[Carrier] CHECK CONSTRAINT [FK_Carier_AddressBook]
GO
ALTER TABLE [dbo].[ChartOfAccount]  WITH CHECK ADD  CONSTRAINT [FK_ChartOfAccts_Company] FOREIGN KEY([CompanyId])
REFERENCES [dbo].[Company] ([CompanyId])
GO
ALTER TABLE [dbo].[ChartOfAccount] CHECK CONSTRAINT [FK_ChartOfAccts_Company]
GO
ALTER TABLE [dbo].[Contract]  WITH CHECK ADD  CONSTRAINT [FK_Contract_Customer] FOREIGN KEY([CustomerId])
REFERENCES [dbo].[Customer] ([CustomerId])
GO
ALTER TABLE [dbo].[Contract] CHECK CONSTRAINT [FK_Contract_Customer]
GO
ALTER TABLE [dbo].[Contract]  WITH CHECK ADD  CONSTRAINT [FK_Contract_UDC] FOREIGN KEY([ServiceTypeXRefId])
REFERENCES [dbo].[UDC] ([XRefId])
GO
ALTER TABLE [dbo].[Contract] CHECK CONSTRAINT [FK_Contract_UDC]
GO
ALTER TABLE [dbo].[ContractInvoice]  WITH CHECK ADD  CONSTRAINT [FK_ContractInvoice_Contract] FOREIGN KEY([ContractId])
REFERENCES [dbo].[Contract] ([ContractId])
GO
ALTER TABLE [dbo].[ContractInvoice] CHECK CONSTRAINT [FK_ContractInvoice_Contract]
GO
ALTER TABLE [dbo].[ContractInvoice]  WITH CHECK ADD  CONSTRAINT [FK_ContractInvoice_Invoice] FOREIGN KEY([InvoiceId])
REFERENCES [dbo].[Invoice] ([InvoiceId])
GO
ALTER TABLE [dbo].[ContractInvoice] CHECK CONSTRAINT [FK_ContractInvoice_Invoice]
GO
ALTER TABLE [dbo].[ContractItem]  WITH CHECK ADD  CONSTRAINT [FK_ContractContent_Contract] FOREIGN KEY([ContractId])
REFERENCES [dbo].[Contract] ([ContractId])
GO
ALTER TABLE [dbo].[ContractItem] CHECK CONSTRAINT [FK_ContractContent_Contract]
GO
ALTER TABLE [dbo].[Customer]  WITH CHECK ADD  CONSTRAINT [FK_Customer_AddressBook] FOREIGN KEY([AddressId])
REFERENCES [dbo].[AddressBook] ([AddressId])
GO
ALTER TABLE [dbo].[Customer] CHECK CONSTRAINT [FK_Customer_AddressBook]
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
ALTER TABLE [dbo].[CustomerLedger]  WITH CHECK ADD  CONSTRAINT [FK_CustomerLedger_AcctRec] FOREIGN KEY([AccountReceivableId])
REFERENCES [dbo].[AccountReceivable] ([AccountReceivableId])
GO
ALTER TABLE [dbo].[CustomerLedger] CHECK CONSTRAINT [FK_CustomerLedger_AcctRec]
GO
ALTER TABLE [dbo].[CustomerLedger]  WITH CHECK ADD  CONSTRAINT [FK_CustomerLedger_Customer] FOREIGN KEY([CustomerId])
REFERENCES [dbo].[Customer] ([CustomerId])
GO
ALTER TABLE [dbo].[CustomerLedger] CHECK CONSTRAINT [FK_CustomerLedger_Customer]
GO
ALTER TABLE [dbo].[CustomerLedger]  WITH CHECK ADD  CONSTRAINT [FK_CustomerLedger_GeneralLedger] FOREIGN KEY([GeneralLedgerId])
REFERENCES [dbo].[GeneralLedger] ([GeneralLedgerId])
GO
ALTER TABLE [dbo].[CustomerLedger] CHECK CONSTRAINT [FK_CustomerLedger_GeneralLedger]
GO
ALTER TABLE [dbo].[CustomerLedger]  WITH CHECK ADD  CONSTRAINT [FK_CustomerLedger_Invoice] FOREIGN KEY([InvoiceId])
REFERENCES [dbo].[Invoice] ([InvoiceId])
GO
ALTER TABLE [dbo].[CustomerLedger] CHECK CONSTRAINT [FK_CustomerLedger_Invoice]
GO
ALTER TABLE [dbo].[EmailEntity]  WITH CHECK ADD  CONSTRAINT [FK_Emails_AddressBook] FOREIGN KEY([AddressId])
REFERENCES [dbo].[AddressBook] ([AddressId])
GO
ALTER TABLE [dbo].[EmailEntity] CHECK CONSTRAINT [FK_Emails_AddressBook]
GO
ALTER TABLE [dbo].[Employee]  WITH CHECK ADD  CONSTRAINT [FK__Employee__Addres__4AD81681] FOREIGN KEY([AddressId])
REFERENCES [dbo].[AddressBook] ([AddressId])
GO
ALTER TABLE [dbo].[Employee] CHECK CONSTRAINT [FK__Employee__Addres__4AD81681]
GO
ALTER TABLE [dbo].[Employee]  WITH CHECK ADD  CONSTRAINT [FK__Employee__Employ__49E3F248] FOREIGN KEY([EmploymentStatusXRefId])
REFERENCES [dbo].[UDC] ([XRefId])
GO
ALTER TABLE [dbo].[Employee] CHECK CONSTRAINT [FK__Employee__Employ__49E3F248]
GO
ALTER TABLE [dbo].[Employee]  WITH CHECK ADD  CONSTRAINT [FK__Employee__JobTit__48EFCE0F] FOREIGN KEY([JobTitleXrefId])
REFERENCES [dbo].[UDC] ([XRefId])
GO
ALTER TABLE [dbo].[Employee] CHECK CONSTRAINT [FK__Employee__JobTit__48EFCE0F]
GO
ALTER TABLE [dbo].[EmployeePosition]  WITH CHECK ADD  CONSTRAINT [FK_EmployeePosition_Company] FOREIGN KEY([CompanyId])
REFERENCES [dbo].[Company] ([CompanyId])
GO
ALTER TABLE [dbo].[EmployeePosition] CHECK CONSTRAINT [FK_EmployeePosition_Company]
GO
ALTER TABLE [dbo].[GeneralLedger]  WITH CHECK ADD  CONSTRAINT [FK_GeneralLedger_AddressBook] FOREIGN KEY([AddressId])
REFERENCES [dbo].[AddressBook] ([AddressId])
GO
ALTER TABLE [dbo].[GeneralLedger] CHECK CONSTRAINT [FK_GeneralLedger_AddressBook]
GO
ALTER TABLE [dbo].[GeneralLedger]  WITH CHECK ADD  CONSTRAINT [FK_GeneralLedger_ChartOfAccts] FOREIGN KEY([AccountId])
REFERENCES [dbo].[ChartOfAccount] ([AccountId])
GO
ALTER TABLE [dbo].[GeneralLedger] CHECK CONSTRAINT [FK_GeneralLedger_ChartOfAccts]
GO
ALTER TABLE [dbo].[Inventory]  WITH CHECK ADD  CONSTRAINT [FK_Inventory_ItemMaster] FOREIGN KEY([ItemId])
REFERENCES [dbo].[ItemMaster] ([ItemId])
GO
ALTER TABLE [dbo].[Inventory] CHECK CONSTRAINT [FK_Inventory_ItemMaster]
GO
ALTER TABLE [dbo].[Invoice]  WITH CHECK ADD  CONSTRAINT [FK_Invoice_Company] FOREIGN KEY([CompanyId])
REFERENCES [dbo].[Company] ([CompanyId])
GO
ALTER TABLE [dbo].[Invoice] CHECK CONSTRAINT [FK_Invoice_Company]
GO
ALTER TABLE [dbo].[Invoice]  WITH CHECK ADD  CONSTRAINT [FK_Invoice_TaxRatesByCode] FOREIGN KEY([TaxRatesByCodeId])
REFERENCES [dbo].[TaxRatesByCode] ([TaxRatesByCodeId])
GO
ALTER TABLE [dbo].[Invoice] CHECK CONSTRAINT [FK_Invoice_TaxRatesByCode]
GO
ALTER TABLE [dbo].[InvoiceDetail]  WITH CHECK ADD  CONSTRAINT [FK_InvoicesDetail_Invoices] FOREIGN KEY([InvoiceId])
REFERENCES [dbo].[Invoice] ([InvoiceId])
GO
ALTER TABLE [dbo].[InvoiceDetail] CHECK CONSTRAINT [FK_InvoicesDetail_Invoices]
GO
ALTER TABLE [dbo].[JobChangeOrder]  WITH CHECK ADD  CONSTRAINT [FK_JobChangeOrder_Contract] FOREIGN KEY([ContractId])
REFERENCES [dbo].[Contract] ([ContractId])
GO
ALTER TABLE [dbo].[JobChangeOrder] CHECK CONSTRAINT [FK_JobChangeOrder_Contract]
GO
ALTER TABLE [dbo].[JobChangeOrder]  WITH CHECK ADD  CONSTRAINT [FK_JobChangeOrder_ContractItem] FOREIGN KEY([ContractItemId])
REFERENCES [dbo].[ContractItem] ([ContractItemId])
GO
ALTER TABLE [dbo].[JobChangeOrder] CHECK CONSTRAINT [FK_JobChangeOrder_ContractItem]
GO
ALTER TABLE [dbo].[JobChangeOrder]  WITH CHECK ADD  CONSTRAINT [FK_JobChangeOrder_JobMaster] FOREIGN KEY([JobMasterId])
REFERENCES [dbo].[JobMaster] ([JobMasterId])
GO
ALTER TABLE [dbo].[JobChangeOrder] CHECK CONSTRAINT [FK_JobChangeOrder_JobMaster]
GO
ALTER TABLE [dbo].[JobCostLedger]  WITH CHECK ADD  CONSTRAINT [FK_JobCostLedger_Contract] FOREIGN KEY([ContractId])
REFERENCES [dbo].[Contract] ([ContractId])
GO
ALTER TABLE [dbo].[JobCostLedger] CHECK CONSTRAINT [FK_JobCostLedger_Contract]
GO
ALTER TABLE [dbo].[JobCostLedger]  WITH CHECK ADD  CONSTRAINT [FK_JobCostLedger_Customer] FOREIGN KEY([CustomerId])
REFERENCES [dbo].[Customer] ([CustomerId])
GO
ALTER TABLE [dbo].[JobCostLedger] CHECK CONSTRAINT [FK_JobCostLedger_Customer]
GO
ALTER TABLE [dbo].[JobCostLedger]  WITH CHECK ADD  CONSTRAINT [FK_JobCostLedger_JobCostType] FOREIGN KEY([JobCostTypeId])
REFERENCES [dbo].[JobCostType] ([JobCostTypeId])
GO
ALTER TABLE [dbo].[JobCostLedger] CHECK CONSTRAINT [FK_JobCostLedger_JobCostType]
GO
ALTER TABLE [dbo].[JobCostLedger]  WITH CHECK ADD  CONSTRAINT [FK_JobCostLedger_JobMaster] FOREIGN KEY([JobMasterId])
REFERENCES [dbo].[JobMaster] ([JobMasterId])
GO
ALTER TABLE [dbo].[JobCostLedger] CHECK CONSTRAINT [FK_JobCostLedger_JobMaster]
GO
ALTER TABLE [dbo].[JobCostLedger]  WITH CHECK ADD  CONSTRAINT [FK_JobCostLedger_JobPhase] FOREIGN KEY([JobPhaseId])
REFERENCES [dbo].[JobPhase] ([JobPhaseId])
GO
ALTER TABLE [dbo].[JobCostLedger] CHECK CONSTRAINT [FK_JobCostLedger_JobPhase]
GO
ALTER TABLE [dbo].[JobMaster]  WITH CHECK ADD  CONSTRAINT [FK_JobMaster_Contract] FOREIGN KEY([ContractId])
REFERENCES [dbo].[Contract] ([ContractId])
GO
ALTER TABLE [dbo].[JobMaster] CHECK CONSTRAINT [FK_JobMaster_Contract]
GO
ALTER TABLE [dbo].[JobMaster]  WITH CHECK ADD  CONSTRAINT [FK_JobMaster_Customer] FOREIGN KEY([CustomerId])
REFERENCES [dbo].[Customer] ([CustomerId])
GO
ALTER TABLE [dbo].[JobMaster] CHECK CONSTRAINT [FK_JobMaster_Customer]
GO
ALTER TABLE [dbo].[JobPhase]  WITH CHECK ADD  CONSTRAINT [FK_JobPhase_Contract] FOREIGN KEY([ContractId])
REFERENCES [dbo].[Contract] ([ContractId])
GO
ALTER TABLE [dbo].[JobPhase] CHECK CONSTRAINT [FK_JobPhase_Contract]
GO
ALTER TABLE [dbo].[JobPhase]  WITH CHECK ADD  CONSTRAINT [FK_JobPhase_JobMaster] FOREIGN KEY([JobMasterId])
REFERENCES [dbo].[JobMaster] ([JobMasterId])
GO
ALTER TABLE [dbo].[JobPhase] CHECK CONSTRAINT [FK_JobPhase_JobMaster]
GO
ALTER TABLE [dbo].[LocationAddress]  WITH CHECK ADD  CONSTRAINT [FK_LocationAddress_AddressBook] FOREIGN KEY([AddressId])
REFERENCES [dbo].[AddressBook] ([AddressId])
GO
ALTER TABLE [dbo].[LocationAddress] CHECK CONSTRAINT [FK_LocationAddress_AddressBook]
GO
ALTER TABLE [dbo].[LocationAddress]  WITH CHECK ADD  CONSTRAINT [FK_LocationAddress_UDCType] FOREIGN KEY([TypeXRefId])
REFERENCES [dbo].[UDC] ([XRefId])
GO
ALTER TABLE [dbo].[LocationAddress] CHECK CONSTRAINT [FK_LocationAddress_UDCType]
GO
ALTER TABLE [dbo].[PackingSlip]  WITH CHECK ADD  CONSTRAINT [FK_Receipt_Supplier] FOREIGN KEY([SupplierId])
REFERENCES [dbo].[Supplier] ([SupplierId])
GO
ALTER TABLE [dbo].[PackingSlip] CHECK CONSTRAINT [FK_Receipt_Supplier]
GO
ALTER TABLE [dbo].[PackingSlipDetail]  WITH CHECK ADD  CONSTRAINT [FK_PackingSlipDetail_PackingSlip] FOREIGN KEY([PackingSlipId])
REFERENCES [dbo].[PackingSlip] ([PackingSlipId])
GO
ALTER TABLE [dbo].[PackingSlipDetail] CHECK CONSTRAINT [FK_PackingSlipDetail_PackingSlip]
GO
ALTER TABLE [dbo].[PhoneEntity]  WITH CHECK ADD  CONSTRAINT [FK_Phones_AddressBook] FOREIGN KEY([AddressId])
REFERENCES [dbo].[AddressBook] ([AddressId])
GO
ALTER TABLE [dbo].[PhoneEntity] CHECK CONSTRAINT [FK_Phones_AddressBook]
GO
ALTER TABLE [dbo].[POQuote]  WITH CHECK ADD  CONSTRAINT [FK_POQuote_Customer] FOREIGN KEY([CustomerId])
REFERENCES [dbo].[Customer] ([CustomerId])
GO
ALTER TABLE [dbo].[POQuote] CHECK CONSTRAINT [FK_POQuote_Customer]
GO
ALTER TABLE [dbo].[POQuote]  WITH CHECK ADD  CONSTRAINT [FK_POQuote_Supplier] FOREIGN KEY([SupplierId])
REFERENCES [dbo].[Supplier] ([SupplierId])
GO
ALTER TABLE [dbo].[POQuote] CHECK CONSTRAINT [FK_POQuote_Supplier]
GO
ALTER TABLE [dbo].[ProjectManagementMilestone]  WITH CHECK ADD  CONSTRAINT [FK_ProjectManagementMilestones_ProjectManagementProject] FOREIGN KEY([ProjectId])
REFERENCES [dbo].[ProjectManagementProject] ([ProjectId])
GO
ALTER TABLE [dbo].[ProjectManagementMilestone] CHECK CONSTRAINT [FK_ProjectManagementMilestones_ProjectManagementProject]
GO
ALTER TABLE [dbo].[ProjectManagementTask]  WITH CHECK ADD  CONSTRAINT [FK_ProjectManagementTask_ProjectManagementMilestones] FOREIGN KEY([MileStoneId])
REFERENCES [dbo].[ProjectManagementMilestone] ([MilestoneId])
GO
ALTER TABLE [dbo].[ProjectManagementTask] CHECK CONSTRAINT [FK_ProjectManagementTask_ProjectManagementMilestones]
GO
ALTER TABLE [dbo].[ProjectManagementTask]  WITH CHECK ADD  CONSTRAINT [FK_ProjectManagementTask_ProjectManagementProject] FOREIGN KEY([ProjectId])
REFERENCES [dbo].[ProjectManagementProject] ([ProjectId])
GO
ALTER TABLE [dbo].[ProjectManagementTask] CHECK CONSTRAINT [FK_ProjectManagementTask_ProjectManagementProject]
GO
ALTER TABLE [dbo].[ProjectManagementTask]  WITH CHECK ADD  CONSTRAINT [FK_ProjectManagementTask_UDC] FOREIGN KEY([StatusXrefId])
REFERENCES [dbo].[UDC] ([XRefId])
GO
ALTER TABLE [dbo].[ProjectManagementTask] CHECK CONSTRAINT [FK_ProjectManagementTask_UDC]
GO
ALTER TABLE [dbo].[ProjectManagementTaskToEmployee]  WITH CHECK ADD  CONSTRAINT [FK__ProjectMa__Emplo__4DB4832C] FOREIGN KEY([EmployeeId])
REFERENCES [dbo].[Employee] ([EmployeeId])
GO
ALTER TABLE [dbo].[ProjectManagementTaskToEmployee] CHECK CONSTRAINT [FK__ProjectMa__Emplo__4DB4832C]
GO
ALTER TABLE [dbo].[ProjectManagementTaskToEmployee]  WITH CHECK ADD  CONSTRAINT [FK_ProjectManagementTaskToEmployee_ProjectManagementTask] FOREIGN KEY([TaskId])
REFERENCES [dbo].[ProjectManagementTask] ([TaskId])
GO
ALTER TABLE [dbo].[ProjectManagementTaskToEmployee] CHECK CONSTRAINT [FK_ProjectManagementTaskToEmployee_ProjectManagementTask]
GO
ALTER TABLE [dbo].[ProjectManagementWorkOrder]  WITH CHECK ADD  CONSTRAINT [FK_ProjectManagementWorkOrder_ProjectManagementWorkOrder] FOREIGN KEY([ProjectId])
REFERENCES [dbo].[ProjectManagementProject] ([ProjectId])
GO
ALTER TABLE [dbo].[ProjectManagementWorkOrder] CHECK CONSTRAINT [FK_ProjectManagementWorkOrder_ProjectManagementWorkOrder]
GO
ALTER TABLE [dbo].[ProjectManagementWorkOrderToEmployee]  WITH CHECK ADD  CONSTRAINT [FK_ProjectManagementWorkOrderToEmployee_Employee] FOREIGN KEY([EmployeeId])
REFERENCES [dbo].[Employee] ([EmployeeId])
GO
ALTER TABLE [dbo].[ProjectManagementWorkOrderToEmployee] CHECK CONSTRAINT [FK_ProjectManagementWorkOrderToEmployee_Employee]
GO
ALTER TABLE [dbo].[ProjectManagementWorkOrderToEmployee]  WITH CHECK ADD  CONSTRAINT [FK_ProjectManagementWorkOrderToEmployee_ProjectManagementWorkOrder] FOREIGN KEY([WorkOrderId])
REFERENCES [dbo].[ProjectManagementWorkOrder] ([WorkOrderId])
GO
ALTER TABLE [dbo].[ProjectManagementWorkOrderToEmployee] CHECK CONSTRAINT [FK_ProjectManagementWorkOrderToEmployee_ProjectManagementWorkOrder]
GO
ALTER TABLE [dbo].[PurchaseOrder]  WITH CHECK ADD  CONSTRAINT [FK_PurchaseOrder_ChartOfAccts] FOREIGN KEY([AccountId])
REFERENCES [dbo].[ChartOfAccount] ([AccountId])
GO
ALTER TABLE [dbo].[PurchaseOrder] CHECK CONSTRAINT [FK_PurchaseOrder_ChartOfAccts]
GO
ALTER TABLE [dbo].[PurchaseOrderDetail]  WITH CHECK ADD  CONSTRAINT [FK_PurchaseOrderDetail_PurchaseOrder] FOREIGN KEY([PurchaseOrderId])
REFERENCES [dbo].[PurchaseOrder] ([PurchaseOrderId])
GO
ALTER TABLE [dbo].[PurchaseOrderDetail] CHECK CONSTRAINT [FK_PurchaseOrderDetail_PurchaseOrder]
GO
ALTER TABLE [dbo].[SalesOrder]  WITH CHECK ADD  CONSTRAINT [FK_SalesOrder_Customer] FOREIGN KEY([CustomerId])
REFERENCES [dbo].[Customer] ([CustomerId])
GO
ALTER TABLE [dbo].[SalesOrder] CHECK CONSTRAINT [FK_SalesOrder_Customer]
GO
ALTER TABLE [dbo].[SalesOrderDetail]  WITH CHECK ADD  CONSTRAINT [FK_SalesOrderDetail_ItemMaster] FOREIGN KEY([ItemId])
REFERENCES [dbo].[ItemMaster] ([ItemId])
GO
ALTER TABLE [dbo].[SalesOrderDetail] CHECK CONSTRAINT [FK_SalesOrderDetail_ItemMaster]
GO
ALTER TABLE [dbo].[SalesOrderDetail]  WITH CHECK ADD  CONSTRAINT [FK_SalesOrderDetail_SalesOrder] FOREIGN KEY([SalesOrderId])
REFERENCES [dbo].[SalesOrder] ([SalesOrderId])
GO
ALTER TABLE [dbo].[SalesOrderDetail] CHECK CONSTRAINT [FK_SalesOrderDetail_SalesOrder]
GO
ALTER TABLE [dbo].[ScheduleEvent]  WITH CHECK ADD  CONSTRAINT [FK__ScheduleE__Emplo__2759D01A] FOREIGN KEY([EmployeeId])
REFERENCES [dbo].[Employee] ([EmployeeId])
GO
ALTER TABLE [dbo].[ScheduleEvent] CHECK CONSTRAINT [FK__ScheduleE__Emplo__2759D01A]
GO
ALTER TABLE [dbo].[ScheduleEvent]  WITH CHECK ADD  CONSTRAINT [FK__ScheduleE__Servi__2942188C] FOREIGN KEY([ServiceId])
REFERENCES [dbo].[ServiceInformation] ([ServiceId])
GO
ALTER TABLE [dbo].[ScheduleEvent] CHECK CONSTRAINT [FK__ScheduleE__Servi__2942188C]
GO
ALTER TABLE [dbo].[ScheduleEvent]  WITH CHECK ADD  CONSTRAINT [FK_ScheduleEvent_Customer] FOREIGN KEY([CustomerId])
REFERENCES [dbo].[Customer] ([CustomerId])
GO
ALTER TABLE [dbo].[ScheduleEvent] CHECK CONSTRAINT [FK_ScheduleEvent_Customer]
GO
ALTER TABLE [dbo].[ServiceInformation]  WITH CHECK ADD  CONSTRAINT [FK__ServiceIn__Contr__33BFA6FF] FOREIGN KEY([ContractId])
REFERENCES [dbo].[Contract] ([ContractId])
GO
ALTER TABLE [dbo].[ServiceInformation] CHECK CONSTRAINT [FK__ServiceIn__Contr__33BFA6FF]
GO
ALTER TABLE [dbo].[ServiceInformation]  WITH CHECK ADD  CONSTRAINT [FK__ServiceIn__Custo__2A363CC5] FOREIGN KEY([CustomerId])
REFERENCES [dbo].[Customer] ([CustomerId])
GO
ALTER TABLE [dbo].[ServiceInformation] CHECK CONSTRAINT [FK__ServiceIn__Custo__2A363CC5]
GO
ALTER TABLE [dbo].[ServiceInformation]  WITH CHECK ADD  CONSTRAINT [FK__ServiceIn__Locat__39788055] FOREIGN KEY([LocationId])
REFERENCES [dbo].[LocationAddress] ([LocationAddressId])
GO
ALTER TABLE [dbo].[ServiceInformation] CHECK CONSTRAINT [FK__ServiceIn__Locat__39788055]
GO
ALTER TABLE [dbo].[ServiceInformation]  WITH CHECK ADD  CONSTRAINT [FK__ServiceIn__Servi__22951AFD] FOREIGN KEY([ServiceTypeXRefId])
REFERENCES [dbo].[UDC] ([XRefId])
GO
ALTER TABLE [dbo].[ServiceInformation] CHECK CONSTRAINT [FK__ServiceIn__Servi__22951AFD]
GO
ALTER TABLE [dbo].[ServiceInformationInvoice]  WITH CHECK ADD  CONSTRAINT [FK_ServiceInformationInvoice_Invoice] FOREIGN KEY([InvoiceId])
REFERENCES [dbo].[Invoice] ([InvoiceId])
GO
ALTER TABLE [dbo].[ServiceInformationInvoice] CHECK CONSTRAINT [FK_ServiceInformationInvoice_Invoice]
GO
ALTER TABLE [dbo].[ServiceInformationInvoice]  WITH CHECK ADD  CONSTRAINT [FK_ServiceInformationInvoice_ServiceInformation] FOREIGN KEY([ServiceId])
REFERENCES [dbo].[ServiceInformation] ([ServiceId])
GO
ALTER TABLE [dbo].[ServiceInformationInvoice] CHECK CONSTRAINT [FK_ServiceInformationInvoice_ServiceInformation]
GO
ALTER TABLE [dbo].[Shipment]  WITH CHECK ADD  CONSTRAINT [FK_Shipments_Customer] FOREIGN KEY([CustomerId])
REFERENCES [dbo].[Customer] ([CustomerId])
GO
ALTER TABLE [dbo].[Shipment] CHECK CONSTRAINT [FK_Shipments_Customer]
GO
ALTER TABLE [dbo].[ShipmentDetail]  WITH CHECK ADD  CONSTRAINT [FK_ShipmentsDetail_ItemMaster] FOREIGN KEY([ItemId])
REFERENCES [dbo].[ItemMaster] ([ItemId])
GO
ALTER TABLE [dbo].[ShipmentDetail] CHECK CONSTRAINT [FK_ShipmentsDetail_ItemMaster]
GO
ALTER TABLE [dbo].[ShipmentDetail]  WITH CHECK ADD  CONSTRAINT [FK_ShipmentsDetail_Shipments] FOREIGN KEY([ShipmentId])
REFERENCES [dbo].[Shipment] ([ShipmentId])
GO
ALTER TABLE [dbo].[ShipmentDetail] CHECK CONSTRAINT [FK_ShipmentsDetail_Shipments]
GO
ALTER TABLE [dbo].[Supervisor]  WITH CHECK ADD  CONSTRAINT [FK_Supervisor_AddressBook] FOREIGN KEY([AddressId])
REFERENCES [dbo].[AddressBook] ([AddressId])
GO
ALTER TABLE [dbo].[Supervisor] CHECK CONSTRAINT [FK_Supervisor_AddressBook]
GO
ALTER TABLE [dbo].[Supervisor]  WITH CHECK ADD  CONSTRAINT [FK_Supervisor_UDC] FOREIGN KEY([JobTitleXrefId])
REFERENCES [dbo].[UDC] ([XRefId])
GO
ALTER TABLE [dbo].[Supervisor] CHECK CONSTRAINT [FK_Supervisor_UDC]
GO
ALTER TABLE [dbo].[SupervisorEmployees]  WITH CHECK ADD  CONSTRAINT [FK__Superviso__Emplo__4BCC3ABA] FOREIGN KEY([EmployeeId])
REFERENCES [dbo].[Employee] ([EmployeeId])
GO
ALTER TABLE [dbo].[SupervisorEmployees] CHECK CONSTRAINT [FK__Superviso__Emplo__4BCC3ABA]
GO
ALTER TABLE [dbo].[SupervisorEmployees]  WITH CHECK ADD  CONSTRAINT [FK_SupervisorEmployees_Supervisor] FOREIGN KEY([SupervisorId])
REFERENCES [dbo].[Supervisor] ([SupervisorId])
GO
ALTER TABLE [dbo].[SupervisorEmployees] CHECK CONSTRAINT [FK_SupervisorEmployees_Supervisor]
GO
ALTER TABLE [dbo].[Supplier]  WITH CHECK ADD  CONSTRAINT [FK_Supplier_AddressBook] FOREIGN KEY([AddressId])
REFERENCES [dbo].[AddressBook] ([AddressId])
GO
ALTER TABLE [dbo].[Supplier] CHECK CONSTRAINT [FK_Supplier_AddressBook]
GO
ALTER TABLE [dbo].[SupplierInvoice]  WITH CHECK ADD  CONSTRAINT [FK_SupplierInvoice_Supplier] FOREIGN KEY([SupplierId])
REFERENCES [dbo].[Supplier] ([SupplierId])
GO
ALTER TABLE [dbo].[SupplierInvoice] CHECK CONSTRAINT [FK_SupplierInvoice_Supplier]
GO
ALTER TABLE [dbo].[SupplierInvoiceDetail]  WITH CHECK ADD  CONSTRAINT [FK_SupplierInvoicesDetail_ItemMaster] FOREIGN KEY([ItemId])
REFERENCES [dbo].[ItemMaster] ([ItemId])
GO
ALTER TABLE [dbo].[SupplierInvoiceDetail] CHECK CONSTRAINT [FK_SupplierInvoicesDetail_ItemMaster]
GO
ALTER TABLE [dbo].[SupplierInvoiceDetail]  WITH CHECK ADD  CONSTRAINT [FK_SupplierInvoicesDetail_SupplierInvoices] FOREIGN KEY([SupplierInvoiceId])
REFERENCES [dbo].[SupplierInvoice] ([SupplierInvoiceId])
GO
ALTER TABLE [dbo].[SupplierInvoiceDetail] CHECK CONSTRAINT [FK_SupplierInvoicesDetail_SupplierInvoices]
GO
ALTER TABLE [dbo].[SupplierLedger]  WITH CHECK ADD  CONSTRAINT [FK_SupplierLedger_AcctPay] FOREIGN KEY([AcctPayId])
REFERENCES [dbo].[AccountPayable] ([AccountPayableId])
GO
ALTER TABLE [dbo].[SupplierLedger] CHECK CONSTRAINT [FK_SupplierLedger_AcctPay]
GO
ALTER TABLE [dbo].[SupplierLedger]  WITH CHECK ADD  CONSTRAINT [FK_SupplierLedger_GeneralLedger] FOREIGN KEY([GeneralLedgerId])
REFERENCES [dbo].[GeneralLedger] ([GeneralLedgerId])
GO
ALTER TABLE [dbo].[SupplierLedger] CHECK CONSTRAINT [FK_SupplierLedger_GeneralLedger]
GO
ALTER TABLE [dbo].[SupplierLedger]  WITH CHECK ADD  CONSTRAINT [FK_SupplierLedger_Supplier] FOREIGN KEY([SupplierId])
REFERENCES [dbo].[Supplier] ([SupplierId])
GO
ALTER TABLE [dbo].[SupplierLedger] CHECK CONSTRAINT [FK_SupplierLedger_Supplier]
GO
ALTER TABLE [dbo].[SupplierLedger]  WITH CHECK ADD  CONSTRAINT [FK_SupplierLedger_SupplierInvoice] FOREIGN KEY([InvoiceId])
REFERENCES [dbo].[SupplierInvoice] ([SupplierInvoiceId])
GO
ALTER TABLE [dbo].[SupplierLedger] CHECK CONSTRAINT [FK_SupplierLedger_SupplierInvoice]
GO
ALTER TABLE [dbo].[TimeAndAttendancePunchIn]  WITH CHECK ADD  CONSTRAINT [FK__TimeAndAt__Emplo__4CC05EF3] FOREIGN KEY([EmployeeId])
REFERENCES [dbo].[Employee] ([EmployeeId])
GO
ALTER TABLE [dbo].[TimeAndAttendancePunchIn] CHECK CONSTRAINT [FK__TimeAndAt__Emplo__4CC05EF3]
GO
ALTER TABLE [dbo].[TimeAndAttendancePunchIn]  WITH CHECK ADD  CONSTRAINT [FK_TimeAndAttendancePunchIn_Supervisor] FOREIGN KEY([SupervisorId])
REFERENCES [dbo].[Supervisor] ([SupervisorId])
GO
ALTER TABLE [dbo].[TimeAndAttendancePunchIn] CHECK CONSTRAINT [FK_TimeAndAttendancePunchIn_Supervisor]
GO
ALTER TABLE [dbo].[TimeAndAttendancePunchIn]  WITH CHECK ADD  CONSTRAINT [FK_TimeAndAttendancePunchIn_TimeAndAttendanceSchedule] FOREIGN KEY([SupervisorId])
REFERENCES [dbo].[TimeAndAttendanceSchedule] ([ScheduleId])
GO
ALTER TABLE [dbo].[TimeAndAttendancePunchIn] CHECK CONSTRAINT [FK_TimeAndAttendancePunchIn_TimeAndAttendanceSchedule]
GO
ALTER TABLE [dbo].[TimeAndAttendancePunchIn]  WITH CHECK ADD  CONSTRAINT [FK_TimeAndAttendancePunchIn_TimeAndAttendanceShift] FOREIGN KEY([ShiftId])
REFERENCES [dbo].[TimeAndAttendanceShift] ([ShiftId])
GO
ALTER TABLE [dbo].[TimeAndAttendancePunchIn] CHECK CONSTRAINT [FK_TimeAndAttendancePunchIn_TimeAndAttendanceShift]
GO
ALTER TABLE [dbo].[TimeAndAttendancePunchIn]  WITH CHECK ADD  CONSTRAINT [FK_TimeAndAttendancePunchIn_UDC] FOREIGN KEY([TypeOfTimeUdcXrefId])
REFERENCES [dbo].[UDC] ([XRefId])
GO
ALTER TABLE [dbo].[TimeAndAttendancePunchIn] CHECK CONSTRAINT [FK_TimeAndAttendancePunchIn_UDC]
GO
ALTER TABLE [dbo].[TimeAndAttendanceSchedule]  WITH CHECK ADD  CONSTRAINT [FK_TimeAndAttendanceSchedule_TimeAndAttendanceShift] FOREIGN KEY([ShiftId])
REFERENCES [dbo].[TimeAndAttendanceShift] ([ShiftId])
GO
ALTER TABLE [dbo].[TimeAndAttendanceSchedule] CHECK CONSTRAINT [FK_TimeAndAttendanceSchedule_TimeAndAttendanceShift]
GO
ALTER TABLE [dbo].[TimeAndAttendanceScheduledToWork]  WITH CHECK ADD  CONSTRAINT [FK_TimeAndAttendanceScheduledToWork_Employee] FOREIGN KEY([EmployeeId])
REFERENCES [dbo].[Employee] ([EmployeeId])
GO
ALTER TABLE [dbo].[TimeAndAttendanceScheduledToWork] CHECK CONSTRAINT [FK_TimeAndAttendanceScheduledToWork_Employee]
GO
ALTER TABLE [dbo].[TimeAndAttendanceScheduledToWork]  WITH CHECK ADD  CONSTRAINT [FK_TimeAndAttendanceScheduledToWork_TimeAndAttendanceSchedule] FOREIGN KEY([ScheduleId])
REFERENCES [dbo].[TimeAndAttendanceSchedule] ([ScheduleId])
GO
ALTER TABLE [dbo].[TimeAndAttendanceScheduledToWork] CHECK CONSTRAINT [FK_TimeAndAttendanceScheduledToWork_TimeAndAttendanceSchedule]
GO
ALTER TABLE [dbo].[TimeAndAttendanceScheduledToWork]  WITH CHECK ADD  CONSTRAINT [FK_TimeAndAttendanceScheduledToWork_TimeAndAttendanceShift] FOREIGN KEY([ShiftId])
REFERENCES [dbo].[TimeAndAttendanceShift] ([ShiftId])
GO
ALTER TABLE [dbo].[TimeAndAttendanceScheduledToWork] CHECK CONSTRAINT [FK_TimeAndAttendanceScheduledToWork_TimeAndAttendanceShift]
GO
/****** Object:  StoredProcedure [dbo].[usp_AddressBook]    Script Date: 12/2/2020 5:04:29 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[usp_AddressBook]( @Id as BigInt)
AS
BEGIN
    -- SET NOCOUNT ON added to prevent extra result sets from
    -- interfering with SELECT statements.
    SET NOCOUNT ON

	select
	[AddressId] 
	,[Name]
	,[FirstName]
	,[LastName] 
	from [dbo].[AddressBook]
where [AddressId]=@Id


END
GO
/****** Object:  StoredProcedure [dbo].[usp_CreateAccount]    Script Date: 12/2/2020 5:04:29 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author:      <Author, , Name>
-- Create Date: <Create Date, , >
-- Description: <Description, , >
-- =============================================
CREATE PROCEDURE [dbo].[usp_CreateAccount]
(
  @ParamHashTable as AccountRegistrationTableType readonly
)
AS
BEGIN
   
   select * from @ParamHashTable

END
GO
/****** Object:  StoredProcedure [dbo].[usp_GetNextNumber]    Script Date: 12/2/2020 5:04:29 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[usp_GetNextNumber]( @NextNumberName as varchar(255))
as
begin
begin transaction
select * into #temp from NextNumber where NextNumberName = @NextNumberName
update NextNumber set nextNumberValue+=1 where NextNumberName=@NextNumberName;
commit transaction
select * from #temp
end
GO
/****** Object:  StoredProcedure [dbo].[usp_RollupGeneralLedgerBalance]    Script Date: 12/2/2020 5:04:29 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO




CREATE procedure [dbo].[usp_RollupGeneralLedgerBalance]
(
@AccountId as varchar(20),
@FiscalPeriod as int,
@FiscalYear as int,
@DocType as varchar(20)

)
as

declare @Count as int;

select @Count=count(*) from accountbalance
where accountid=@AccountId and fiscalyear=@FiscalYear and fiscalperiod=@FiscalPeriod


begin transaction
if @Count>0 
begin
  update  AccountBalance     
  set amount=accountbalances.amount

  from 
  (
  select accountid, fiscalperiod, fiscalyear, ledgertype, Sum(amount) Amount
  from generalledger gl 
  where
  accountid=@AccountId
  and  fiscalyear=@FiscalYear 
   and fiscalperiod=@FiscalPeriod
   and docType=@DocType
   group by accountid, fiscalperiod, fiscalyear,ledgertype
  ) accountbalances
  where
   AccountBalance.accountid=accountbalances.accountid
   and
   AccountBalance.AccountId=@AccountId
   and
   AccountBalance.FiscalPeriod=@FiscalPeriod
   and
   AccountBalance.FiscalYear=@FiscalYear
  
 end
else
begin
insert into AccountBalance
(accountid
,AccountBalanceType
,FiscalYear
,FiscalPeriod
,Amount
)
select accountId, 
ledgertype,fiscalyear, 
fiscalperiod,
sum(Amount) Amount from generalledger
group by fiscalyear, accountid, fiscalperiod,ledgertype
having accountid=@AccountId and fiscalyear=@FiscalYear and fiscalperiod=@FiscalPeriod
end

commit transaction
GO
