
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
  
