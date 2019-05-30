

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


