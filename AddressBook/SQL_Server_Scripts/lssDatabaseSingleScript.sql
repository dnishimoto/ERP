USE [listensoftwareDB]
GO
/****** Object:  UserDefinedTableType [dbo].[AccountRegistrationTableType]    Script Date: 10/8/2018 5:15:46 AM ******/
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
/****** Object:  Table [dbo].[Budget]    Script Date: 10/8/2018 5:15:47 AM ******/
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
 CONSTRAINT [PK_Budget] PRIMARY KEY CLUSTERED 
(
	[BudgetId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ChartOfAccts]    Script Date: 10/8/2018 5:15:52 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ChartOfAccts](
	[AccountId] [bigint] IDENTITY(1,1) NOT NULL,
	[Location] [varchar](10) NULL,
	[BusUnit] [varchar](10) NULL,
	[Subsidiary] [varchar](10) NULL,
	[SubSub] [varchar](10) NULL,
	[Account] [varchar](30) NULL,
	[Description] [varchar](255) NULL,
	[CompanyNumber] [varchar](10) NULL,
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
 CONSTRAINT [PK__chartOfA__349DA5A6F015CCF2] PRIMARY KEY CLUSTERED 
(
	[AccountId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[BudgetRange]    Script Date: 10/8/2018 5:15:52 AM ******/
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
	[BusinessUnit] [varchar](10) NULL,
	[Subsidiary] [varchar](10) NULL,
	[AccountId] [bigint] NULL,
	[SupervisorCode] [varchar](50) NULL,
	[LastUpdated] [datetime] NULL,
	[ObjectNumber] [varchar](10) NULL,
	[IsActive] [bit] NULL,
 CONSTRAINT [PK_BudgetRange] PRIMARY KEY CLUSTERED 
(
	[RangeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[BudgetView]    Script Date: 10/8/2018 5:15:52 AM ******/
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
/****** Object:  Table [dbo].[AddressBook]    Script Date: 10/8/2018 5:15:52 AM ******/
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
/****** Object:  Table [dbo].[Contract]    Script Date: 10/8/2018 5:15:52 AM ******/
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
 CONSTRAINT [PK__Contract__C90D34697E612150] PRIMARY KEY CLUSTERED 
(
	[ContractId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Customer]    Script Date: 10/8/2018 5:15:53 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Customer](
	[CustomerId] [bigint] IDENTITY(1,1) NOT NULL,
	[AddressId] [bigint] NOT NULL,
	[PrimaryShippedToLocationId] [bigint] NULL,
	[PrimaryEmailId] [bigint] NULL,
	[PrimaryPhoneId] [bigint] NULL,
	[MailingLocationId] [bigint] NULL,
	[PrimaryBillingLocationId] [bigint] NULL,
	[TaxIdentification] [varchar](50) NULL,
 CONSTRAINT [PK__Customer__091C2AFB7C8C5421] PRIMARY KEY CLUSTERED 
(
	[CustomerId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Supplier]    Script Date: 10/8/2018 5:15:53 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Supplier](
	[SupplierId] [bigint] IDENTITY(1,1) NOT NULL,
	[AddressId] [bigint] NOT NULL,
	[Identification] [varchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[SupplierId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PurchaseOrder]    Script Date: 10/8/2018 5:15:53 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PurchaseOrder](
	[PurchaseOrderId] [bigint] IDENTITY(1,1) NOT NULL,
	[DocType] [varchar](20) NULL,
	[PaymentTerms] [varchar](10) NULL,
	[GrossAmount] [money] NULL,
	[Remark] [varchar](max) NULL,
	[GLDate] [datetime] NULL,
	[AccountId] [bigint] NOT NULL,
	[SupplierId] [bigint] NOT NULL,
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
 CONSTRAINT [PK_PurchaseOrder] PRIMARY KEY CLUSTERED 
(
	[PurchaseOrderId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  View [dbo].[PurchaseOrderAndDetailView]    Script Date: 10/8/2018 5:15:54 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE View [dbo].[PurchaseOrderAndDetailView]
as
select 
purchaseorder.potype,
purchaseorder.paymentterms,
purchaseorder.grossamount,
purchaseorder.remark,
purchaseorder.gldate,
purchaseorder.accountid,
purchaseorder.supplierid,
purchaseorder.customerid,
purchaseorder.contractid,
purchaseorder.poquoteid,
purchaseorder.description,
supplier.suppliername,
supplier.suppliercompany,
customer.customername,
contract.title,
contract.StartDate,
contract.EndDate
from purchaseorder purchaseorder
outer apply
(select supplierid, name suppliername, identification suppliercompany from supplier supplier
join addressbook addressbook on supplier.AddressId=addressbook.addressId
where purchaseorder.supplierid=addressbook.addressid) supplier

outer apply
(select customerid, name customername from customer customer
join addressbook addressbook on customer.AddressId=addressbook.addressId
where purchaseorder.supplierid=addressbook.addressid) customer

outer apply
(select contractid, title, startdate, enddate from contract contract 
where contract.ContractId=purchaseorder.ContractId) contract


GO
/****** Object:  Table [dbo].[SalesOrder]    Script Date: 10/8/2018 5:15:54 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SalesOrder](
	[SalesOrderId] [bigint] IDENTITY(1,1) NOT NULL,
	[Quantity] [int] NULL,
	[Amount] [decimal](18, 4) NULL,
	[OrderNumber] [varchar](20) NULL,
	[OrderType] [varchar](10) NULL,
	[CustomerId] [bigint] NOT NULL,
	[DeliveredToLocationId] [bigint] NULL,
	[ShippedToLocationId] [bigint] NULL,
	[InvoiceId] [bigint] NULL,
	[TakenBy] [nchar](10) NULL,
	[UnitOfMeasure] [nchar](10) NULL,
	[FreightAmount] [decimal](18, 4) NULL,
	[CarrierId] [bigint] NULL,
	[BuyerId] [bigint] NULL,
	[PaymentInstrument] [nchar](10) NULL,
	[PaymentTerms] [varchar](10) NULL,
	[TransactionDate] [date] NULL,
	[ScheduledPickupDate] [datetime] NULL,
	[ActualPickupDate] [datetime] NULL,
 CONSTRAINT [PK_SalesOrder] PRIMARY KEY CLUSTERED 
(
	[SalesOrderId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Buyer]    Script Date: 10/8/2018 5:15:54 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Buyer](
	[BuyerId] [bigint] IDENTITY(1,1) NOT NULL,
	[AddressId] [bigint] NOT NULL,
	[Title] [varchar](100) NULL,
 CONSTRAINT [PK_Buyer] PRIMARY KEY CLUSTERED 
(
	[BuyerId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Invoice]    Script Date: 10/8/2018 5:15:54 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Invoice](
	[InvoiceId] [bigint] IDENTITY(1,1) NOT NULL,
	[InvoiceNumber] [varchar](20) NULL,
	[InvoiceDate] [date] NULL,
	[Amount] [decimal](18, 4) NULL,
	[CustomerId] [bigint] NOT NULL,
	[Description] [varchar](2000) NULL,
	[TaxAmount] [decimal](18, 4) NULL,
	[PaymentDueDate] [date] NULL,
	[PaymentTerms] [varchar](50) NULL,
	[CompanyId] [bigint] NOT NULL,
	[DiscountDueDate] [date] NULL,
	[FreightCost] [decimal](18, 4) NULL,
	[DiscountAmount] [decimal](18, 4) NULL,
 CONSTRAINT [PK_Invoices] PRIMARY KEY CLUSTERED 
(
	[InvoiceId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Carrier]    Script Date: 10/8/2018 5:15:54 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Carrier](
	[CarrierId] [bigint] IDENTITY(1,1) NOT NULL,
	[AddressId] [bigint] NOT NULL,
	[CarrierTypeXrefId] [bigint] NOT NULL,
 CONSTRAINT [PK_Carier] PRIMARY KEY CLUSTERED 
(
	[CarrierId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[SalesOrderAndInvoiceView]    Script Date: 10/8/2018 5:15:54 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE View [dbo].[SalesOrderAndInvoiceView]
as
select 
invoices.InvoiceId
,invoices.InvoiceNumber
,invoices.Amount
,invoices.Description InvoiceDescription
,invoices.TaxAmount
,invoices.PaymentDueDate
,invoices.PaymentTerms 

,salesorder.quantity
,salesorder.amount SalesOrderAmount
,salesorder.ordernumber
,salesorder.ordertype
,salesorder.CustomerId
,salesorder.DeliveredToLocationId
,salesorder.ShippedToLocationId
,salesorder.TakenBy
,salesorder.UnitOfMeasure
,salesorder.FreightAmount
,salesorder.CarrierId
,salesorder.BuyerId
,salesorder.PaymentInstrument
,salesorder.TransactionDate
,salesorder.ScheduledPickupDate
,salesorder.ActualPickupDate
,customer.customername
,buyer.buyername
,carrier.carriername

from salesorder salesorder
left join invoice invoices on salesorder.invoiceid=invoices.invoiceid

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
/****** Object:  Table [dbo].[SalesOrderDetail]    Script Date: 10/8/2018 5:15:54 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SalesOrderDetail](
	[SalesOrderDetailId] [bigint] IDENTITY(1,1) NOT NULL,
	[SalesOrderId] [bigint] NOT NULL,
	[ItemId] [bigint] NOT NULL,
	[Description] [varchar](255) NULL,
	[Quantity] [int] NULL,
	[Amount] [decimal](18, 4) NULL,
	[UnitOfMeasure] [varchar](10) NULL,
	[UnitPrice] [decimal](18, 4) NULL,
	[InvoiceDetailId] [bigint] NULL,
 CONSTRAINT [PK_SalesOrderDetail] PRIMARY KEY CLUSTERED 
(
	[SalesOrderDetailId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[InvoiceDetail]    Script Date: 10/8/2018 5:15:54 AM ******/
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
	[PurchaseOrderLineId] [bigint] NULL,
	[SalesOrderDetailId] [bigint] NULL,
	[ItemId] [bigint] NOT NULL,
	[DiscountPercent] [decimal](18, 4) NULL,
	[DiscountAmount] [decimal](18, 4) NULL,
	[ShipmentDetailId] [bigint] NULL,
	[ExtendedDescription] [varchar](255) NULL,
	[DiscountDueDate] [date] NULL,
 CONSTRAINT [PK_InvoiceLineDetail] PRIMARY KEY CLUSTERED 
(
	[InvoiceDetailId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[SalesOrderDetailView]    Script Date: 10/8/2018 5:15:55 AM ******/
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
	  ,InvoiceDetail.InvoiceDetailDescription
  FROM SalesOrder SalesOrder
  left join [dbo].[SalesOrderDetail] SalesOrderDetail on SalesOrder.SalesOrderID=SalesOrderDetail.SalesOrderID
  outer apply
  (
	select Description InvoiceDetailDescription from invoiceDetail invoiceDetail where
	invoiceDetail.InvoiceDetailId=salesorderdetail.InvoiceDetailId
  ) InvoiceDetail
  
GO
/****** Object:  Table [dbo].[Emails]    Script Date: 10/8/2018 5:15:55 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Emails](
	[EmailId] [bigint] IDENTITY(1,1) NOT NULL,
	[Password] [varchar](20) NULL,
	[LoginEmail] [bit] NULL,
	[Email] [varchar](30) NOT NULL,
	[AddressId] [bigint] NOT NULL,
 CONSTRAINT [PK_Emails] PRIMARY KEY CLUSTERED 
(
	[EmailId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Phones]    Script Date: 10/8/2018 5:15:55 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Phones](
	[PhoneId] [bigint] IDENTITY(1,1) NOT NULL,
	[PhoneNumber] [varchar](50) NULL,
	[PhoneType] [varchar](10) NULL,
	[Extension] [varchar](10) NULL,
	[AddressId] [bigint] NOT NULL,
 CONSTRAINT [PK_Phones] PRIMARY KEY CLUSTERED 
(
	[PhoneId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UDC]    Script Date: 10/8/2018 5:15:55 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UDC](
	[XRefId] [bigint] IDENTITY(1,1) NOT NULL,
	[ProductCode] [varchar](20) NULL,
	[KeyCode] [varchar](50) NULL,
	[Value] [varchar](255) NULL,
PRIMARY KEY CLUSTERED 
(
	[XRefId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[LocationAddress]    Script Date: 10/8/2018 5:15:55 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LocationAddress](
	[LocationId] [bigint] IDENTITY(1,1) NOT NULL,
	[Address Line 1] [varchar](255) NULL,
	[Address Line 2] [varchar](255) NULL,
	[City] [varchar](50) NULL,
	[Zipcode] [varchar](20) NULL,
	[TypeXRefId] [bigint] NOT NULL,
	[AddressId] [bigint] NOT NULL,
	[State] [nchar](2) NULL,
	[Country] [varchar](50) NULL,
 CONSTRAINT [PK_LocationAddress] PRIMARY KEY CLUSTERED 
(
	[LocationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[CustomerView]    Script Date: 10/8/2018 5:15:55 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



CREATE VIEW [dbo].[CustomerView] AS
SELECT Customer.CustomerId, Customer.AddressId, AddressBook.Name, 
LocationAddress.LocationId, LocationAddress.[Address Line 1], 
LocationAddress.[Address Line 2], LocationAddress.City, LocationAddress.State, 
                  LocationAddress.Zipcode, LocationAddress.Country, LocationAddress.TypeXRefId, 
phones.phonenumber,
phones.phonetype,
emails.email,
locationType_udc.value locationType
FROM     Customer INNER JOIN
                  LocationAddress ON Customer.AddressId = LocationAddress.AddressId LEFT OUTER JOIN
                  AddressBook ON Customer.AddressId = AddressBook.AddressId LEFT OUTER JOIN
                  phones ON phones.addressid = Customer.AddressId LEFT OUTER JOIN
                  emails ON emails.addressid = Customer.AddressId   join
		  udc locationType_udc on locationType_udc.xrefid=LocationAddress.TypeXRefId


GO
/****** Object:  View [dbo].[SupplierView]    Script Date: 10/8/2018 5:15:55 AM ******/
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
/****** Object:  Table [dbo].[AcctRec]    Script Date: 10/8/2018 5:15:55 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AcctRec](
	[AcctRecId] [bigint] IDENTITY(1,1) NOT NULL,
	[OpenAmount] [money] NULL,
	[DiscountDueDate] [date] NULL,
	[GLDate] [date] NULL,
	[InvoiceId] [bigint] NOT NULL,
	[CreateDate] [date] NULL,
	[DocNumber] [bigint] NULL,
	[Remarks] [varchar](255) NULL,
	[PaymentTerms] [varchar](50) NULL,
	[CustomerId] [bigint] NOT NULL,
	[PurchaseOrderId] [bigint] NULL,
	[Description] [varchar](255) NULL,
	[AcctRecDocTypeXRefId] [bigint] NOT NULL,
	[AccountId] [bigint] NOT NULL,
	[Amount] [money] NULL,
	[DebitAmount] [money] NULL,
	[CreditAmount] [money] NULL,
	[PaymentDueDate] [date] NULL,
	[DiscountPercent] [decimal](18, 2) NULL,
	[DiscountAmount] [money] NULL,
 CONSTRAINT [PK__AcctRec__4B67207728200668] PRIMARY KEY CLUSTERED 
(
	[AcctRecId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[GeneralLedger]    Script Date: 10/8/2018 5:15:56 AM ******/
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
 CONSTRAINT [PK__generalL__3214EC07AC773B83] PRIMARY KEY CLUSTERED 
(
	[GeneralLedgerId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[AR_GL_INV_View]    Script Date: 10/8/2018 5:15:56 AM ******/
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
/****** Object:  Table [dbo].[Company]    Script Date: 10/8/2018 5:15:56 AM ******/
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
 CONSTRAINT [PK_Company] PRIMARY KEY CLUSTERED 
(
	[CompanyId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[CompanyForInvoiceView]    Script Date: 10/8/2018 5:15:56 AM ******/
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
/****** Object:  Table [dbo].[CustomerClaim]    Script Date: 10/8/2018 5:15:56 AM ******/
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
 CONSTRAINT [PK_CustomerClaim] PRIMARY KEY CLUSTERED 
(
	[ClaimId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  View [dbo].[CustomrClaimView]    Script Date: 10/8/2018 5:15:56 AM ******/
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
/****** Object:  Table [dbo].[Employee]    Script Date: 10/8/2018 5:15:56 AM ******/
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
/****** Object:  View [dbo].[EmployeeView]    Script Date: 10/8/2018 5:15:56 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create view [dbo].[EmployeeView]
as
select
employeeid,
name employeeName
from employee employee join
addressbook addressbook on employee.addressid=addressbook.addressid        
GO
/****** Object:  Table [dbo].[Supervisor]    Script Date: 10/8/2018 5:15:56 AM ******/
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
 CONSTRAINT [PK__Supervisor__091C2AFB7C8C5421] PRIMARY KEY CLUSTERED 
(
	[SupervisorId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[supervisorView]    Script Date: 10/8/2018 5:15:56 AM ******/
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
/****** Object:  Table [dbo].[TimeAndAttendancePunchIn]    Script Date: 10/8/2018 5:15:56 AM ******/
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
 CONSTRAINT [PK_TimeAndAttendancePunchIn] PRIMARY KEY CLUSTERED 
(
	[TimePunchinId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TimeAndAttendanceSchedule]    Script Date: 10/8/2018 5:15:56 AM ******/
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
 CONSTRAINT [PK_TimeAndAttendanceSchedule] PRIMARY KEY CLUSTERED 
(
	[ScheduleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[TimeAndAttendencePunchinView]    Script Date: 10/8/2018 5:15:56 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create view [dbo].[TimeAndAttendencePunchinView]
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
  FROM [dbo].[TimeAndAttendancePunchIn] taPunchin join
  udc udcTypeOfTime on udctypeofTime.xrefid=taPunchin.TypeOfTimeUdcXrefId join
  udc udcJobCode on udcjobcode.xrefid=taPunchin.JobCodeXrefId left join
  udc udcPayCode on udcPayCode.xrefid=taPunchin.PayCodeXrefId join
 TimeAndAttendanceSchedule taSchedule on taschedule.ScheduleId=taPunchin.ScheduleId join

 supervisor supervisor on supervisor.SupervisorId=taPunchin.SupervisorId join
 addressbook supervisorAddressbook on supervisor.AddressId=supervisoraddressbook.addressid join

 employee employee on employee.employeeid = tapunchin.EmployeeId join
 addressbook employeeaddressbook on employee.AddressId=employeeaddressbook.addressid join

 addressbook addressbookApproving on addressbookApproving.AddressId=tapunchin.ApprovingAddressId

GO
/****** Object:  Table [dbo].[ShippedToAddresses]    Script Date: 10/8/2018 5:15:56 AM ******/
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
/****** Object:  View [dbo].[CustomerPrimaryShippedToView]    Script Date: 10/8/2018 5:15:57 AM ******/
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
/****** Object:  View [dbo].[AccountReceivableView]    Script Date: 10/8/2018 5:15:57 AM ******/
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
/****** Object:  Table [dbo].[ItemMaster]    Script Date: 10/8/2018 5:15:57 AM ******/
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
	[ItemNumber] [varchar](20) NOT NULL,
	[UnitPrice] [money] NULL,
 CONSTRAINT [PK_ItemMaster] PRIMARY KEY CLUSTERED 
(
	[ItemId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[InvoiceAndDetailAndCustomerView]    Script Date: 10/8/2018 5:15:57 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO





CREATE View [dbo].[InvoiceAndDetailAndCustomerView]
as
select 
invoices.InvoiceId,
invoices.InvoiceNumber,
invoices.InvoiceDate,
invoices.Amount,
invoices.Description,
invoices.TaxAmount,
invoices.PaymentDueDate,
invoices.PaymentTerms,
itemmaster.description ItemDescription,
itemmaster.itemnumber,
invoicesDetail.UnitPrice,
invoicesDetail.Quantity,
invoicesDetail.Amount DetailAmount,
invoicesDetail.DiscountPercent,
invoicesDetail.DiscountAmount,
invoicesDetail.ExtendedDescription DetailExtendedDescription,
invoices.companyid,
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

from invoice invoices
left join invoiceDetail invoicesDetail on invoices.invoiceId=invoicesDetail.invoiceId
inner join itemmaster itemmaster on invoicesdetail.itemid=itemmaster.itemid
outer apply
(
SELECT addressBook.Name AS CustomerName, Company.CompanyName, Company.CompanyCode, Company.CompanyStreet, Company.CompanyCity, Company.CompanyState, Company.CompanyZipcode, Invoice.InvoiceId, 
                  Invoice.InvoiceNumber,customerLocationAddress.[Address Line 1],customerLocationAddress.[Address Line 2],customerLocationAddress.City CustomerCity,
				  customerLocationAddress.State customerState,customerLocationAddress.Zipcode CustomerZipcode, locationTypeudc.Value  CustomerLocationType
FROM     Company INNER JOIN
                  Invoice ON Company.CompanyId = Invoice.CompanyId INNER JOIN
                  Customer AS customer ON customer.CustomerId = Invoice.CustomerId INNER JOIN
                  AddressBook AS addressBook ON customer.AddressId = addressBook.AddressId  left Join
				  LocationAddress customerLocationAddress on customerLocationAddress.AddressId=customer.AddressId  left join
				  udc locationTypeUdc on locationTypeUdc.xrefid=customerLocationAddress.TypeXRefId
	  
	where invoice.invoiceid=invoices.InvoiceId
) InvoiceHeader
GO
/****** Object:  Table [dbo].[AccountBalance]    Script Date: 10/8/2018 5:15:57 AM ******/
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
/****** Object:  View [dbo].[FinancialView]    Script Date: 10/8/2018 5:15:57 AM ******/
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
	and ab.FiscalPeriod<=ab.FiscalPeriod
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
/****** Object:  View [dbo].[ChartOfAccountView]    Script Date: 10/8/2018 5:15:57 AM ******/
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
/****** Object:  Table [dbo].[__MigrationHistory]    Script Date: 10/8/2018 5:15:57 AM ******/
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
/****** Object:  Table [dbo].[AcctPay]    Script Date: 10/8/2018 5:15:58 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AcctPay](
	[AcctPayId] [bigint] IDENTITY(1,1) NOT NULL,
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
	[InvoiceId] [bigint] NULL,
	[AccountId] [bigint] NOT NULL,
	[DocType] [varchar](20) NOT NULL,
	[PaymentTerms] [varchar](20) NULL,
	[DiscountPercent] [decimal](18, 3) NULL,
	[AmountOpen] [money] NULL,
	[OrderNumber] [varchar](50) NULL,
	[DiscountDueDate] [date] NULL,
	[AmountPaid] [money] NULL,
 CONSTRAINT [PK_AcctPay] PRIMARY KEY CLUSTERED 
(
	[AcctPayId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetRoles]    Script Date: 10/8/2018 5:15:58 AM ******/
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
/****** Object:  Table [dbo].[AspNetUserClaims]    Script Date: 10/8/2018 5:15:58 AM ******/
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
/****** Object:  Table [dbo].[AspNetUserLogins]    Script Date: 10/8/2018 5:15:58 AM ******/
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
/****** Object:  Table [dbo].[AspNetUserRoles]    Script Date: 10/8/2018 5:15:58 AM ******/
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
/****** Object:  Table [dbo].[AspNetUsers]    Script Date: 10/8/2018 5:15:59 AM ******/
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
/****** Object:  Table [dbo].[Assets]    Script Date: 10/8/2018 5:15:59 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Assets](
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
 CONSTRAINT [PK_Assets] PRIMARY KEY CLUSTERED 
(
	[AssetId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[BudgetNote]    Script Date: 10/8/2018 5:15:59 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BudgetNote](
	[BudgetNoteId] [bigint] IDENTITY(1,1) NOT NULL,
	[BudgetId] [bigint] NOT NULL,
	[Note] [ntext] NOT NULL,
	[Create] [datetime] NOT NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[BudgetSnapShot]    Script Date: 10/8/2018 5:15:59 AM ******/
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
/****** Object:  Table [dbo].[ContractContent]    Script Date: 10/8/2018 5:16:00 AM ******/
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
/****** Object:  Table [dbo].[ContractInvoice]    Script Date: 10/8/2018 5:16:00 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ContractInvoice](
	[ContractInvoiceId] [bigint] IDENTITY(1,1) NOT NULL,
	[ContractId] [bigint] NOT NULL,
	[InvoiceId] [bigint] NOT NULL,
 CONSTRAINT [PK_ContractInvoice] PRIMARY KEY CLUSTERED 
(
	[ContractInvoiceId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CustomerLedger]    Script Date: 10/8/2018 5:16:00 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CustomerLedger](
	[CustomerLedgerId] [bigint] IDENTITY(1,1) NOT NULL,
	[CustomerId] [bigint] NOT NULL,
	[InvoiceId] [bigint] NOT NULL,
	[AcctRecId] [bigint] NOT NULL,
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
 CONSTRAINT [PK_CustomerLedger] PRIMARY KEY CLUSTERED 
(
	[CustomerLedgerId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Equations]    Script Date: 10/8/2018 5:16:00 AM ******/
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
/****** Object:  Table [dbo].[Inventory]    Script Date: 10/8/2018 5:16:00 AM ******/
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
 CONSTRAINT [PK_Inventory] PRIMARY KEY CLUSTERED 
(
	[InventoryId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[NetTerms]    Script Date: 10/8/2018 5:16:01 AM ******/
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
/****** Object:  Table [dbo].[NextNumber]    Script Date: 10/8/2018 5:16:01 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[NextNumber](
	[NextNumberId] [bigint] IDENTITY(1,1) NOT NULL,
	[NextNumberName] [varchar](20) NULL,
	[NextNumberValue] [bigint] NOT NULL,
 CONSTRAINT [PK_NextNumber] PRIMARY KEY CLUSTERED 
(
	[NextNumberId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PackingSlip]    Script Date: 10/8/2018 5:16:01 AM ******/
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
 CONSTRAINT [PK_Receipt] PRIMARY KEY CLUSTERED 
(
	[PackingSlipId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PackingSlipDetail]    Script Date: 10/8/2018 5:16:01 AM ******/
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
 CONSTRAINT [PK_PackingSlipDetail] PRIMARY KEY CLUSTERED 
(
	[PackingSlipDetailId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[POQuote]    Script Date: 10/8/2018 5:16:01 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[POQuote](
	[POQuoteId] [bigint] IDENTITY(1,1) NOT NULL,
	[QuoteAmount] [money] NULL,
	[SubmittedDate] [date] NULL,
	[PoId] [bigint] NOT NULL,
	[DocNumber] [bigint] NOT NULL,
	[Remarks] [varchar](255) NULL,
	[CustomerId] [bigint] NOT NULL,
	[SupplierId] [bigint] NOT NULL,
	[SKU] [varchar](50) NULL,
	[Description] [varchar](255) NULL,
PRIMARY KEY CLUSTERED 
(
	[POQuoteId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ProjectManagementMilestones]    Script Date: 10/8/2018 5:16:01 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProjectManagementMilestones](
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
 CONSTRAINT [PK_ProjectManagementMilestones] PRIMARY KEY CLUSTERED 
(
	[MilestoneId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ProjectManagementProject]    Script Date: 10/8/2018 5:16:01 AM ******/
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
 CONSTRAINT [PK_ProjectManagementProject] PRIMARY KEY CLUSTERED 
(
	[ProjectId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ProjectManagementTask]    Script Date: 10/8/2018 5:16:02 AM ******/
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
 CONSTRAINT [PK_ProjectManagementTask] PRIMARY KEY CLUSTERED 
(
	[TaskId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ProjectManagementTaskToEmployee]    Script Date: 10/8/2018 5:16:02 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProjectManagementTaskToEmployee](
	[TaskToEmployeeId] [bigint] IDENTITY(1,1) NOT NULL,
	[EmployeeId] [bigint] NULL,
	[TaskId] [bigint] NULL,
 CONSTRAINT [PK_ProjectManagementTaskToEmployee] PRIMARY KEY CLUSTERED 
(
	[TaskToEmployeeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PurchaseOrderDetail]    Script Date: 10/8/2018 5:16:02 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PurchaseOrderDetail](
	[PurchaseOrderDetailId] [bigint] IDENTITY(1,1) NOT NULL,
	[PurchaseOrderId] [bigint] NOT NULL,
	[Amount] [decimal](18, 4) NULL,
	[OrderedQuantity] [decimal](18, 4) NULL,
	[ItemId] [bigint] NOT NULL,
	[UnitPrice] [decimal](18, 4) NULL,
	[UnitOfMeasure] [varchar](10) NULL,
	[ReceivedDate] [date] NULL,
	[ExpectedDeliveryDate] [date] NULL,
	[OrderDate] [date] NULL,
	[ReceivedQuantity] [int] NULL,
	[RemainingQuantity] [int] NULL,
	[Description] [varchar](255) NULL,
 CONSTRAINT [PK_PurchaseOrderDetail] PRIMARY KEY CLUSTERED 
(
	[PurchaseOrderDetailId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ScheduleEvent]    Script Date: 10/8/2018 5:16:02 AM ******/
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
PRIMARY KEY CLUSTERED 
(
	[ScheduleEventId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ServiceInformation]    Script Date: 10/8/2018 5:16:02 AM ******/
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
PRIMARY KEY CLUSTERED 
(
	[ServiceId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ServiceInformationInvoice]    Script Date: 10/8/2018 5:16:02 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ServiceInformationInvoice](
	[ServiceInformationInvoiceId] [bigint] IDENTITY(1,1) NOT NULL,
	[InvoiceId] [bigint] NOT NULL,
	[ServiceId] [bigint] NOT NULL,
 CONSTRAINT [PK_ServiceInformationInvoice] PRIMARY KEY CLUSTERED 
(
	[ServiceInformationInvoiceId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Shipments]    Script Date: 10/8/2018 5:16:02 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Shipments](
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
 CONSTRAINT [PK_Shipments] PRIMARY KEY CLUSTERED 
(
	[ShipmentId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ShipmentsDetail]    Script Date: 10/8/2018 5:16:03 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ShipmentsDetail](
	[ShipmentDetailId] [bigint] NOT NULL,
	[ShipmentId] [bigint] NOT NULL,
	[ItemId] [bigint] NOT NULL,
	[Quantity] [int] NULL,
	[Amount] [decimal](18, 4) NULL,
	[SalesOrderDetailId] [bigint] NOT NULL,
 CONSTRAINT [PK_ShipmentDetail] PRIMARY KEY CLUSTERED 
(
	[ShipmentDetailId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SupervisorEmployees]    Script Date: 10/8/2018 5:16:03 AM ******/
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
/****** Object:  Table [dbo].[SupplierInvoice]    Script Date: 10/8/2018 5:16:04 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SupplierInvoice](
	[SupplierInvoiceId] [bigint] IDENTITY(1,1) NOT NULL,
	[SupplierInvoiceNumber] [varchar](50) NULL,
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
 CONSTRAINT [PK_SupplierInvoices] PRIMARY KEY CLUSTERED 
(
	[SupplierInvoiceId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SupplierInvoiceDetail]    Script Date: 10/8/2018 5:16:04 AM ******/
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
 CONSTRAINT [PK_SupplierInvoiceLineDetail] PRIMARY KEY CLUSTERED 
(
	[SupplierInvoiceDetailId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SupplierLedger]    Script Date: 10/8/2018 5:16:04 AM ******/
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
 CONSTRAINT [PK_SupplierLedger] PRIMARY KEY CLUSTERED 
(
	[SupplierLedgerId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TaxRatesByCode]    Script Date: 10/8/2018 5:16:04 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TaxRatesByCode](
	[TaxId] [bigint] IDENTITY(1,1) NOT NULL,
	[TaxCode] [varchar](20) NULL,
	[TaxRate] [money] NULL,
	[State] [varchar](2) NULL,
 CONSTRAINT [PK_TaxRatesByCode] PRIMARY KEY CLUSTERED 
(
	[TaxId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TimeAndAttendanceScheduledToWork]    Script Date: 10/8/2018 5:16:04 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TimeAndAttendanceScheduledToWork](
	[ScheduledToWorkId] [bigint] IDENTITY(1,1) NOT NULL,
	[EmployeeId] [bigint] NOT NULL,
	[ScheduleId] [bigint] NOT NULL,
	[ScheduleName] [varchar](255) NULL,
	[StartDateTime] [varchar](20) NULL,
	[EndDateTime] [varchar](20) NULL,
	[StartDate] [date] NULL,
	[EndDate] [date] NULL,
	[EmployeeName] [varchar](255) NULL,
 CONSTRAINT [PK_TimeAndAttendanceScheduledToWork] PRIMARY KEY CLUSTERED 
(
	[ScheduledToWorkId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TimeAndAttendanceShift]    Script Date: 10/8/2018 5:16:05 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TimeAndAttendanceShift](
	[ShiftId] [bigint] IDENTITY(1,1) NOT NULL,
	[ShiftName] [char](20) NULL,
	[ShiftStartTime] [int] NULL,
	[ShiftEndTime] [int] NULL,
	[ShiftType] [varchar](50) NULL,
 CONSTRAINT [PK_TimeAndAttendanceShift] PRIMARY KEY CLUSTERED 
(
	[ShiftId] ASC
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
REFERENCES [dbo].[ChartOfAccts] ([AccountId])
GO
ALTER TABLE [dbo].[AccountBalance] CHECK CONSTRAINT [FK_AccountBalance_ChartOfAccts]
GO
ALTER TABLE [dbo].[AcctPay]  WITH CHECK ADD  CONSTRAINT [FK__AcctPay__Invoice__06ED0088] FOREIGN KEY([InvoiceId])
REFERENCES [dbo].[Invoice] ([InvoiceId])
GO
ALTER TABLE [dbo].[AcctPay] CHECK CONSTRAINT [FK__AcctPay__Invoice__06ED0088]
GO
ALTER TABLE [dbo].[AcctPay]  WITH CHECK ADD  CONSTRAINT [FK__AcctPay__Purchas__031C6FA4] FOREIGN KEY([PurchaseOrderId])
REFERENCES [dbo].[PurchaseOrder] ([PurchaseOrderId])
GO
ALTER TABLE [dbo].[AcctPay] CHECK CONSTRAINT [FK__AcctPay__Purchas__031C6FA4]
GO
ALTER TABLE [dbo].[AcctPay]  WITH CHECK ADD  CONSTRAINT [FK_AcctPay_ChartOfAccts] FOREIGN KEY([AccountId])
REFERENCES [dbo].[ChartOfAccts] ([AccountId])
GO
ALTER TABLE [dbo].[AcctPay] CHECK CONSTRAINT [FK_AcctPay_ChartOfAccts]
GO
ALTER TABLE [dbo].[AcctPay]  WITH CHECK ADD  CONSTRAINT [FK_AcctPay_Contract] FOREIGN KEY([ContractId])
REFERENCES [dbo].[Contract] ([ContractId])
GO
ALTER TABLE [dbo].[AcctPay] CHECK CONSTRAINT [FK_AcctPay_Contract]
GO
ALTER TABLE [dbo].[AcctPay]  WITH CHECK ADD  CONSTRAINT [FK_AcctPay_POQuote] FOREIGN KEY([POQuoteId])
REFERENCES [dbo].[POQuote] ([POQuoteId])
GO
ALTER TABLE [dbo].[AcctPay] CHECK CONSTRAINT [FK_AcctPay_POQuote]
GO
ALTER TABLE [dbo].[AcctPay]  WITH CHECK ADD  CONSTRAINT [FK_AcctPay_Supplier] FOREIGN KEY([SupplierId])
REFERENCES [dbo].[Supplier] ([SupplierId])
GO
ALTER TABLE [dbo].[AcctPay] CHECK CONSTRAINT [FK_AcctPay_Supplier]
GO
ALTER TABLE [dbo].[AcctRec]  WITH CHECK ADD  CONSTRAINT [FK_AcctRec_ChartOfAccts] FOREIGN KEY([AccountId])
REFERENCES [dbo].[ChartOfAccts] ([AccountId])
GO
ALTER TABLE [dbo].[AcctRec] CHECK CONSTRAINT [FK_AcctRec_ChartOfAccts]
GO
ALTER TABLE [dbo].[AcctRec]  WITH CHECK ADD  CONSTRAINT [FK_AcctRec_Customer] FOREIGN KEY([CustomerId])
REFERENCES [dbo].[Customer] ([CustomerId])
GO
ALTER TABLE [dbo].[AcctRec] CHECK CONSTRAINT [FK_AcctRec_Customer]
GO
ALTER TABLE [dbo].[AcctRec]  WITH CHECK ADD  CONSTRAINT [FK_AcctRec_Invoices] FOREIGN KEY([InvoiceId])
REFERENCES [dbo].[Invoice] ([InvoiceId])
GO
ALTER TABLE [dbo].[AcctRec] CHECK CONSTRAINT [FK_AcctRec_Invoices]
GO
ALTER TABLE [dbo].[AcctRec]  WITH CHECK ADD  CONSTRAINT [FK_AcctRec_UDC] FOREIGN KEY([AcctRecDocTypeXRefId])
REFERENCES [dbo].[UDC] ([XRefId])
GO
ALTER TABLE [dbo].[AcctRec] CHECK CONSTRAINT [FK_AcctRec_UDC]
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
ALTER TABLE [dbo].[Assets]  WITH CHECK ADD  CONSTRAINT [FK_Assets_UDC] FOREIGN KEY([EquipmentStatusXRefId])
REFERENCES [dbo].[UDC] ([XRefId])
GO
ALTER TABLE [dbo].[Assets] CHECK CONSTRAINT [FK_Assets_UDC]
GO
ALTER TABLE [dbo].[Budget]  WITH CHECK ADD  CONSTRAINT [FK_Budget_BudgetRange] FOREIGN KEY([RangeId])
REFERENCES [dbo].[BudgetRange] ([RangeId])
GO
ALTER TABLE [dbo].[Budget] CHECK CONSTRAINT [FK_Budget_BudgetRange]
GO
ALTER TABLE [dbo].[Budget]  WITH CHECK ADD  CONSTRAINT [FK_Budget_ChartOfAccts] FOREIGN KEY([AccountId])
REFERENCES [dbo].[ChartOfAccts] ([AccountId])
GO
ALTER TABLE [dbo].[Budget] CHECK CONSTRAINT [FK_Budget_ChartOfAccts]
GO
ALTER TABLE [dbo].[BudgetNote]  WITH CHECK ADD  CONSTRAINT [FK_BudgetNote_Budget] FOREIGN KEY([BudgetId])
REFERENCES [dbo].[Budget] ([BudgetId])
GO
ALTER TABLE [dbo].[BudgetNote] CHECK CONSTRAINT [FK_BudgetNote_Budget]
GO
ALTER TABLE [dbo].[BudgetRange]  WITH CHECK ADD  CONSTRAINT [FK_BudgetRange_ChartOfAccts] FOREIGN KEY([AccountId])
REFERENCES [dbo].[ChartOfAccts] ([AccountId])
GO
ALTER TABLE [dbo].[BudgetRange] CHECK CONSTRAINT [FK_BudgetRange_ChartOfAccts]
GO
ALTER TABLE [dbo].[Buyer]  WITH CHECK ADD  CONSTRAINT [FK_Buyer_AddressBook] FOREIGN KEY([AddressId])
REFERENCES [dbo].[AddressBook] ([AddressId])
GO
ALTER TABLE [dbo].[Buyer] CHECK CONSTRAINT [FK_Buyer_AddressBook]
GO
ALTER TABLE [dbo].[Carrier]  WITH CHECK ADD FOREIGN KEY([CarrierTypeXrefId])
REFERENCES [dbo].[UDC] ([XRefId])
GO
ALTER TABLE [dbo].[Carrier]  WITH CHECK ADD  CONSTRAINT [FK_Carier_AddressBook] FOREIGN KEY([AddressId])
REFERENCES [dbo].[AddressBook] ([AddressId])
GO
ALTER TABLE [dbo].[Carrier] CHECK CONSTRAINT [FK_Carier_AddressBook]
GO
ALTER TABLE [dbo].[ChartOfAccts]  WITH CHECK ADD  CONSTRAINT [FK_ChartOfAccts_Company] FOREIGN KEY([CompanyId])
REFERENCES [dbo].[Company] ([CompanyId])
GO
ALTER TABLE [dbo].[ChartOfAccts] CHECK CONSTRAINT [FK_ChartOfAccts_Company]
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
ALTER TABLE [dbo].[ContractContent]  WITH CHECK ADD  CONSTRAINT [FK_ContractContent_Contract] FOREIGN KEY([ContractId])
REFERENCES [dbo].[Contract] ([ContractId])
GO
ALTER TABLE [dbo].[ContractContent] CHECK CONSTRAINT [FK_ContractContent_Contract]
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
ALTER TABLE [dbo].[CustomerLedger]  WITH CHECK ADD  CONSTRAINT [FK_CustomerLedger_AcctRec] FOREIGN KEY([AcctRecId])
REFERENCES [dbo].[AcctRec] ([AcctRecId])
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
ALTER TABLE [dbo].[Emails]  WITH CHECK ADD  CONSTRAINT [FK_Emails_AddressBook] FOREIGN KEY([AddressId])
REFERENCES [dbo].[AddressBook] ([AddressId])
GO
ALTER TABLE [dbo].[Emails] CHECK CONSTRAINT [FK_Emails_AddressBook]
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
ALTER TABLE [dbo].[GeneralLedger]  WITH CHECK ADD  CONSTRAINT [FK_GeneralLedger_AddressBook] FOREIGN KEY([AddressId])
REFERENCES [dbo].[AddressBook] ([AddressId])
GO
ALTER TABLE [dbo].[GeneralLedger] CHECK CONSTRAINT [FK_GeneralLedger_AddressBook]
GO
ALTER TABLE [dbo].[GeneralLedger]  WITH CHECK ADD  CONSTRAINT [FK_GeneralLedger_ChartOfAccts] FOREIGN KEY([AccountId])
REFERENCES [dbo].[ChartOfAccts] ([AccountId])
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
ALTER TABLE [dbo].[Invoice]  WITH CHECK ADD  CONSTRAINT [FK_Invoices_Customer] FOREIGN KEY([CustomerId])
REFERENCES [dbo].[Customer] ([CustomerId])
GO
ALTER TABLE [dbo].[Invoice] CHECK CONSTRAINT [FK_Invoices_Customer]
GO
ALTER TABLE [dbo].[InvoiceDetail]  WITH CHECK ADD  CONSTRAINT [FK_InvoicesDetail_Invoices] FOREIGN KEY([InvoiceId])
REFERENCES [dbo].[Invoice] ([InvoiceId])
GO
ALTER TABLE [dbo].[InvoiceDetail] CHECK CONSTRAINT [FK_InvoicesDetail_Invoices]
GO
ALTER TABLE [dbo].[InvoiceDetail]  WITH CHECK ADD  CONSTRAINT [FK_InvoicesDetail_ItemMaster] FOREIGN KEY([ItemId])
REFERENCES [dbo].[ItemMaster] ([ItemId])
GO
ALTER TABLE [dbo].[InvoiceDetail] CHECK CONSTRAINT [FK_InvoicesDetail_ItemMaster]
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
ALTER TABLE [dbo].[Phones]  WITH CHECK ADD  CONSTRAINT [FK_Phones_AddressBook] FOREIGN KEY([AddressId])
REFERENCES [dbo].[AddressBook] ([AddressId])
GO
ALTER TABLE [dbo].[Phones] CHECK CONSTRAINT [FK_Phones_AddressBook]
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
ALTER TABLE [dbo].[ProjectManagementMilestones]  WITH CHECK ADD  CONSTRAINT [FK_ProjectManagementMilestones_ProjectManagementProject] FOREIGN KEY([ProjectId])
REFERENCES [dbo].[ProjectManagementProject] ([ProjectId])
GO
ALTER TABLE [dbo].[ProjectManagementMilestones] CHECK CONSTRAINT [FK_ProjectManagementMilestones_ProjectManagementProject]
GO
ALTER TABLE [dbo].[ProjectManagementTask]  WITH CHECK ADD  CONSTRAINT [FK_ProjectManagementTask_ProjectManagementMilestones] FOREIGN KEY([MileStoneId])
REFERENCES [dbo].[ProjectManagementMilestones] ([MilestoneId])
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
ALTER TABLE [dbo].[ProjectManagementTaskToEmployee]  WITH CHECK ADD FOREIGN KEY([EmployeeId])
REFERENCES [dbo].[Employee] ([EmployeeId])
GO
ALTER TABLE [dbo].[ProjectManagementTaskToEmployee]  WITH CHECK ADD  CONSTRAINT [FK_ProjectManagementTaskToEmployee_ProjectManagementTask] FOREIGN KEY([TaskId])
REFERENCES [dbo].[ProjectManagementTask] ([TaskId])
GO
ALTER TABLE [dbo].[ProjectManagementTaskToEmployee] CHECK CONSTRAINT [FK_ProjectManagementTaskToEmployee_ProjectManagementTask]
GO
ALTER TABLE [dbo].[PurchaseOrder]  WITH CHECK ADD  CONSTRAINT [FK_PurchaseOrder_ChartOfAccts] FOREIGN KEY([AccountId])
REFERENCES [dbo].[ChartOfAccts] ([AccountId])
GO
ALTER TABLE [dbo].[PurchaseOrder] CHECK CONSTRAINT [FK_PurchaseOrder_ChartOfAccts]
GO
ALTER TABLE [dbo].[PurchaseOrder]  WITH CHECK ADD  CONSTRAINT [FK_PurchaseOrder_Supplier] FOREIGN KEY([SupplierId])
REFERENCES [dbo].[Supplier] ([SupplierId])
GO
ALTER TABLE [dbo].[PurchaseOrder] CHECK CONSTRAINT [FK_PurchaseOrder_Supplier]
GO
ALTER TABLE [dbo].[PurchaseOrderDetail]  WITH CHECK ADD  CONSTRAINT [FK_PurchaseOrderDetail_ItemMaster] FOREIGN KEY([ItemId])
REFERENCES [dbo].[ItemMaster] ([ItemId])
GO
ALTER TABLE [dbo].[PurchaseOrderDetail] CHECK CONSTRAINT [FK_PurchaseOrderDetail_ItemMaster]
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
ALTER TABLE [dbo].[ScheduleEvent]  WITH CHECK ADD FOREIGN KEY([EmployeeId])
REFERENCES [dbo].[Employee] ([EmployeeId])
GO
ALTER TABLE [dbo].[ScheduleEvent]  WITH CHECK ADD FOREIGN KEY([ServiceId])
REFERENCES [dbo].[ServiceInformation] ([ServiceId])
GO
ALTER TABLE [dbo].[ScheduleEvent]  WITH CHECK ADD  CONSTRAINT [FK_ScheduleEvent_Customer] FOREIGN KEY([CustomerId])
REFERENCES [dbo].[Customer] ([CustomerId])
GO
ALTER TABLE [dbo].[ScheduleEvent] CHECK CONSTRAINT [FK_ScheduleEvent_Customer]
GO
ALTER TABLE [dbo].[ServiceInformation]  WITH CHECK ADD FOREIGN KEY([ContractId])
REFERENCES [dbo].[Contract] ([ContractId])
GO
ALTER TABLE [dbo].[ServiceInformation]  WITH CHECK ADD FOREIGN KEY([CustomerId])
REFERENCES [dbo].[Customer] ([CustomerId])
GO
ALTER TABLE [dbo].[ServiceInformation]  WITH CHECK ADD FOREIGN KEY([LocationId])
REFERENCES [dbo].[LocationAddress] ([LocationId])
GO
ALTER TABLE [dbo].[ServiceInformation]  WITH CHECK ADD FOREIGN KEY([ServiceTypeXRefId])
REFERENCES [dbo].[UDC] ([XRefId])
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
ALTER TABLE [dbo].[Shipments]  WITH CHECK ADD  CONSTRAINT [FK_Shipments_Customer] FOREIGN KEY([CustomerId])
REFERENCES [dbo].[Customer] ([CustomerId])
GO
ALTER TABLE [dbo].[Shipments] CHECK CONSTRAINT [FK_Shipments_Customer]
GO
ALTER TABLE [dbo].[ShipmentsDetail]  WITH CHECK ADD  CONSTRAINT [FK_ShipmentsDetail_ItemMaster] FOREIGN KEY([ItemId])
REFERENCES [dbo].[ItemMaster] ([ItemId])
GO
ALTER TABLE [dbo].[ShipmentsDetail] CHECK CONSTRAINT [FK_ShipmentsDetail_ItemMaster]
GO
ALTER TABLE [dbo].[ShipmentsDetail]  WITH CHECK ADD  CONSTRAINT [FK_ShipmentsDetail_Shipments] FOREIGN KEY([ShipmentId])
REFERENCES [dbo].[Shipments] ([ShipmentId])
GO
ALTER TABLE [dbo].[ShipmentsDetail] CHECK CONSTRAINT [FK_ShipmentsDetail_Shipments]
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
ALTER TABLE [dbo].[SupervisorEmployees]  WITH CHECK ADD FOREIGN KEY([EmployeeId])
REFERENCES [dbo].[Employee] ([EmployeeId])
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
REFERENCES [dbo].[AcctPay] ([AcctPayId])
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
ALTER TABLE [dbo].[TimeAndAttendancePunchIn]  WITH CHECK ADD FOREIGN KEY([EmployeeId])
REFERENCES [dbo].[Employee] ([EmployeeId])
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
/****** Object:  StoredProcedure [dbo].[usp_AddressBook]    Script Date: 10/8/2018 5:16:05 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:      <Author, , Name>
-- Create Date: <Create Date, , >
-- Description: <Description, , >
-- =============================================
CREATE PROCEDURE [dbo].[usp_AddressBook]( @Id as BigInt)
AS
BEGIN
    -- SET NOCOUNT ON added to prevent extra result sets from
    -- interfering with SELECT statements.
    SET NOCOUNT ON

	select
	[Id] 
	,[Name]
	,[FirstName]
	,[LastName] 
	,[Company] 
	,[CellPhone]
	,[MailingCity]
	,[MailingState]
	,[MailingAddress]
	,[MailingZipcode]
	,[BillingCity] 
	,[BillingState] 
	,[BillingZipcode]
	,[BillingAddress]
from [dbo].[AddressBook]
where [Id]=@Id


END

GO
/****** Object:  StoredProcedure [dbo].[usp_CreateAccount]    Script Date: 10/8/2018 5:16:05 AM ******/
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
/****** Object:  StoredProcedure [dbo].[usp_GetNextNumber]    Script Date: 10/8/2018 5:16:05 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[usp_GetNextNumber]( @NextNumberName as varchar(20))
as
begin
begin transaction
select * into #temp from NextNumber where NextNumberName = @NextNumberName
update NextNumber set nextNumberValue+=1 where NextNumberName=@NextNumberName;
commit transaction
select * from #temp
end
GO
/****** Object:  StoredProcedure [dbo].[usp_RollupGeneralLedgerBalance]    Script Date: 10/8/2018 5:16:05 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE procedure [dbo].[usp_RollupGeneralLedgerBalance]
(
@AccountId as varchar(20),
@FiscalPeriod as int,
@FiscalYear as int

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
   fiscalyear=@FiscalYear and fiscalperiod=@FiscalPeriod
   group by accountid, fiscalperiod, fiscalyear,ledgertype
  ) accountbalances
  where
   AccountBalance.accountid=accountbalances.accountid
   and
   AccountBalance.AccountId=@AccountId
  
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
