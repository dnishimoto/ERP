USE [listensoftwareDB]
GO
/****** Object:  View [dbo].[PurchaseOrderAndDetailView]    Script Date: 7/30/2018 6:36:11 AM ******/
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
