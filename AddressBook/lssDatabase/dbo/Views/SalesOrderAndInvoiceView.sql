
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
