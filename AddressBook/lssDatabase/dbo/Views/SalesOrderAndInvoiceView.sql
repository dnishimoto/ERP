
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
