using lssWebApi2.EntityFramework;
using lssWebApi2.Enumerations;
using lssWebApi2.SalesOrderDetailDomain;
using lssWebApi2.SalesOrderDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Xunit.Abstractions;

namespace lssWebApi2.SalesOrderDetailDomain
{

    public class UnitSalesOrderDetail
    {

        private readonly ITestOutputHelper output;

        public UnitSalesOrderDetail(ITestOutputHelper output)
        {
            this.output = output;
        }
        [Fact]
        public async Task TestAddUpdatDelete()
        {
            Udc carrierUdc = null;
            SalesOrderDetailModule SalesOrderDetailMod = new SalesOrderDetailModule();
            SalesOrder salesOrder = await SalesOrderDetailMod.SalesOrder.Query().GetEntityById(69);
            ItemMaster itemMaster = await SalesOrderDetailMod.ItemMaster.Query().GetEntityById(11);
            ChartOfAccount account = await SalesOrderDetailMod.ChartOfAccount.Query().GetEntityById(5);
            Carrier carrier = await SalesOrderDetailMod.Carrier.Query().GetEntityById(2);
            if (carrier != null) { carrierUdc = await SalesOrderDetailMod.Udc.Query().GetEntityById(carrier.CarrierTypeXrefId); }
            PurchaseOrder purchaseOrder = await SalesOrderDetailMod.purchaseOrder.Query().GetEntityById(2);
            PurchaseOrderDetail purchaseOrderDetail = await SalesOrderDetailMod.purchaseOrderDetail.Query().GetEntityById(11);

           SalesOrderDetailView view = new SalesOrderDetailView()
            {
                  SalesOrderId=salesOrder.SalesOrderId,
                  OrderNumber=salesOrder.OrderNumber,
                  ItemId=itemMaster.ItemId,
                  Description=itemMaster.Description,
                  ItemDescription2=itemMaster.Description2,
                  Quantity=4,
                  Amount=31.52M,
                  UnitOfMeasure=itemMaster.UnitOfMeasure,
                  UnitPrice=itemMaster.UnitPrice,
                  ScheduledShipDate=DateTime.Parse("12/2/2019"),
                  PromisedDate=DateTime.Parse("12/2/2019"),
                  GrossWeight=16.4M,
                  GrossWeightUnitOfMeasure="LBS",
                  ShippedDate=DateTime.Parse("12/2/2019"),
                  AccountId=account.AccountId,
                  Account=account.Account,
                  BusUnit=account.BusUnit,
                  LineNumber=2,
                  LotSerial="1234Lot",
                  CompanyCode=account.CompanyCode,
                  CarrierId=carrier.CarrierId,
                  CarrierName=carrierUdc.Value,
                  GLDate=DateTime.Parse("12/2/2019"),
                  QuantityOpen=4,
                  AmountOpen=31.52M,
                  PurchaseOrderId=purchaseOrder?.PurchaseOrderId,
                  PurchaseOrderDetailId=purchaseOrderDetail.PurchaseOrderDetailId
            };
            NextNumber nnNextNumber = await SalesOrderDetailMod.SalesOrderDetail.Query().GetNextNumber();

            view.SalesOrderDetailNumber = nnNextNumber.NextNumberValue;

            SalesOrderDetail salesOrderDetail = await SalesOrderDetailMod.SalesOrderDetail.Query().MapToEntity(view);

            SalesOrderDetailMod.SalesOrderDetail.AddSalesOrderDetail(salesOrderDetail).Apply();

            SalesOrderDetail newSalesOrderDetail = await SalesOrderDetailMod.SalesOrderDetail.Query().GetEntityByNumber(view.SalesOrderDetailNumber);

            Assert.NotNull(newSalesOrderDetail);

            newSalesOrderDetail.Description = "SalesOrderDetail Test Update";

            SalesOrderDetailMod.SalesOrderDetail.UpdateSalesOrderDetail(newSalesOrderDetail).Apply();

            SalesOrderDetailView updateView = await SalesOrderDetailMod.SalesOrderDetail.Query().GetViewById(newSalesOrderDetail.SalesOrderDetailId);

            Assert.Same(updateView.Description, "SalesOrderDetail Test Update");
              SalesOrderDetailMod.SalesOrderDetail.DeleteSalesOrderDetail(newSalesOrderDetail).Apply();
            SalesOrderDetail lookupSalesOrderDetail= await SalesOrderDetailMod.SalesOrderDetail.Query().GetEntityById(view.SalesOrderDetailId);

            Assert.Null(lookupSalesOrderDetail);
        }
       
      

    }
}
