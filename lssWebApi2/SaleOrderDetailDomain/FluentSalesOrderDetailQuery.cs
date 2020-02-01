using lssWebApi2.AutoMapper;
using lssWebApi2.Services;
using lssWebApi2.EntityFramework;
using lssWebApi2.Enumerations;
using lssWebApi2.MapperAbstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lssWebApi2.SalesOrderDetailDomain
{
    public class FluentSalesOrderDetailQuery : MapperAbstract<SalesOrderDetail,SalesOrderDetailView>, IFluentSalesOrderDetailQuery
    {
        private UnitOfWork _unitOfWork = null;
        public FluentSalesOrderDetailQuery() { }
        public FluentSalesOrderDetailQuery(UnitOfWork unitOfWork) { _unitOfWork = unitOfWork; }

        public async Task<NextNumber> GetNextNumber()
        {
            return await _unitOfWork.nextNumberRepository.GetNextNumber(TypeOfSalesOrderDetail.SalesOrderDetailNumber.ToString());
        }
        public override async Task<IList<SalesOrderDetail>> MapToEntity(IList<SalesOrderDetailView> inputObjects)
        {
            IList<SalesOrderDetail> list = new List<SalesOrderDetail>();

            foreach (var item in inputObjects)
            {
                SalesOrderDetail outObject = mapper.Map<SalesOrderDetail>(item);
                list.Add(outObject);
            }
            await Task.Yield();
            return list;

        }
        public override async Task<SalesOrderDetail> MapToEntity(SalesOrderDetailView inputObject)
        {

            SalesOrderDetail outObject = mapper.Map<SalesOrderDetail>(inputObject);
            await Task.Yield();
            return outObject;
        }
        public override async Task<SalesOrderDetailView> MapToView(SalesOrderDetail inputObject)
        {
 
            SalesOrderDetailView outObject = mapper.Map<SalesOrderDetailView>(inputObject);


            Udc carrierUdc = null;
            Carrier carrier = await _unitOfWork.carrierRepository.GetEntityById(2);
            if (carrier != null) { carrierUdc = await _unitOfWork.udcRepository.GetEntityById(carrier.CarrierTypeXrefId); }

            Task<SalesOrder> salesOrderTask = _unitOfWork.salesOrderRepository.GetEntityById(inputObject.SalesOrderId);
            Task<ItemMaster> itemMasterTask = _unitOfWork.itemMasterRepository.GetEntityById(inputObject.ItemId);
            Task<ChartOfAccount> accountTask = _unitOfWork.chartOfAccountRepository.GetEntityById(inputObject.AccountId);
            Task<PurchaseOrder> purchaseOrderTask = _unitOfWork.purchaseOrderRepository.GetEntityById(inputObject.CarrierId ?? 0);
            Task<PurchaseOrderDetail> purchaseOrderDetailTask = _unitOfWork.purchaseOrderDetailRepository.GetEntityById(inputObject.PurchaseOrderDetailId);
            Task.WaitAll(salesOrderTask, itemMasterTask, accountTask, purchaseOrderTask, purchaseOrderDetailTask);

            outObject.ItemDescription2 = itemMasterTask.Result.Description2;
            outObject.Account = accountTask.Result.Account;
            outObject.BusUnit = accountTask.Result.BusUnit;
            outObject.CarrierName = carrierUdc.Value;
            outObject.CompanyCode = accountTask.Result.CompanyCode;
            outObject.CarrierName = carrierUdc.Value;

           outObject.PurchaseOrderId = purchaseOrderTask.Result?.PurchaseOrderId;
            outObject.PurchaseOrderDetailId = purchaseOrderDetailTask.Result?.PurchaseOrderDetailId;

            await Task.Yield();
            return outObject;
        }


        public async Task<IList<SalesOrderDetail>> GetDetailsBySalesOrderId(long ?salesOrderId)
        {
            return await _unitOfWork.salesOrderDetailRepository.GetEntitiesBySalesOrderId(salesOrderId);
        }
        public async Task<IList<SalesOrderDetailView>> GetDetailViewsBySalesOrderId(long ? salesOrderId)
        {
            IList<SalesOrderDetailView> listViews = new List<SalesOrderDetailView>();
            IList<SalesOrderDetail> list = await _unitOfWork.salesOrderDetailRepository.GetEntitiesBySalesOrderId(salesOrderId);
            foreach (var item in list)
            {
                listViews.Add(await MapToView(item));
            }

            return listViews;
        }
        public override async Task<SalesOrderDetailView> GetViewById(long ? salesOrderDetailId)
        {
            SalesOrderDetail detailItem = await _unitOfWork.salesOrderDetailRepository.GetEntityById(salesOrderDetailId);

            return await MapToView(detailItem);
        }
        public async Task<SalesOrderDetailView> GetViewByNumber(long salesOrderDetailNumber)
        {
            SalesOrderDetail detailItem = await _unitOfWork.salesOrderDetailRepository.GetEntityByNumber(salesOrderDetailNumber);

            return await MapToView(detailItem);
        }

        public override async Task<SalesOrderDetail> GetEntityById(long ? salesOrderDetailId)
        {
            return await _unitOfWork.salesOrderDetailRepository.GetEntityById(salesOrderDetailId);

        }
        public async Task<SalesOrderDetail> GetEntityByNumber(long salesOrderDetailNumber)
        {
            return await _unitOfWork.salesOrderDetailRepository.GetEntityByNumber(salesOrderDetailNumber);
        }
    }
}