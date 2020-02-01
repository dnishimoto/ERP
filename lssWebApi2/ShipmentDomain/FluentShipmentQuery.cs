
using lssWebApi2.AutoMapper;
using lssWebApi2.Services;
using lssWebApi2.AbstractFactory;
using lssWebApi2.EntityFramework;
using lssWebApi2.Enumerations;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using lssWebApi2.MapperAbstract;
using X.PagedList;
using System.Linq.Expressions;

namespace lssWebApi2.ShipmentsDomain
{
    public class FluentShipmentQuery : MapperAbstract<Shipment,ShipmentView>,IFluentShipmentQuery
    {
        private UnitOfWork _unitOfWork = null;
        public FluentShipmentQuery() { }
        public FluentShipmentQuery(UnitOfWork unitOfWork) { _unitOfWork = unitOfWork; }

        public async Task<Shipment> CalculatedAmountsByDetails(Shipment shipments, List<ShipmentDetail> shipmentsDetails)
        {
            decimal? amount = 0;

            amount = shipmentsDetails.Sum(e => e.Amount);
            shipments.Amount = amount;
         
            await Task.Yield();
            return shipments;
        }
        public override async Task<Shipment> MapToEntity(ShipmentView inputObject)
        {

            Shipment outObject = mapper.Map<Shipment>(inputObject);
            await Task.Yield();
            return outObject;
        }

        public override async Task<IList<Shipment>> MapToEntity(IList<ShipmentView> inputObjects)
        {
            IList<Shipment> list = new List<Shipment>();

            foreach (var item in inputObjects)
            {
                Shipment outObject = mapper.Map<Shipment>(item);
                list.Add(outObject);
            }
            await Task.Yield();
            return list;

        }
        public async Task<Shipment> GetShipmentBySalesOrder(ShipmentView shipmentCreation)
        {
            Shipment shipment = null;
            try
            {
                Task<SalesOrder> salesOrderTask = _unitOfWork.salesOrderRepository.GetEntityById(shipmentCreation.SalesOrderId ?? 0);
                Task<NextNumber> nnShipmentTask =  _unitOfWork.nextNumberRepository.GetNextNumber(TypeOfShipment.ShipmentNumber.ToString());
                Task.WaitAll(salesOrderTask, nnShipmentTask);

                shipment = new Shipment()
                {
                    CustomerId = salesOrderTask.Result.CustomerId,
                    OrderNumber = salesOrderTask.Result.OrderNumber,
                    OrderType = salesOrderTask.Result.OrderType,
                    Duty = shipmentCreation.Duty,
                    Tax = shipmentCreation.Tax,
                    ShippingCost = shipmentCreation.ShippingCost,
                    Amount = shipmentCreation.Amount,
                    CodAmount = shipmentCreation.CodAmount,
                    ActualWeight = shipmentCreation.ActualWeight,
                    BillableWeight = shipmentCreation.BillableWeight,
                    ShippedFromLocationId = shipmentCreation.ShippedFromLocationId,
                    ShippedToLocationId = shipmentCreation.ShippedToLocationId,
                    TrackingNumber = shipmentCreation.TrackingNumber,
                    ShipmentDate = DateTime.Now,
                    CarrierId = shipmentCreation.CarrierId,
                    SalesOrderId = shipmentCreation.SalesOrderId,
                    ShipmentNumber = nnShipmentTask.Result.NextNumberValue
                };

                await Task.Yield();
                return shipment;


            }
            catch (Exception ex)
            {
                throw new Exception("GetShipmentBySalesOrderView", ex);
            }
        }

        public override async Task<ShipmentView> MapToView(Shipment inputObject)
        {

            ShipmentView outObject = mapper.Map<ShipmentView>(inputObject);
            await Task.Yield();
            return outObject;
        }

     
        public async Task<NextNumber> GetNextNumber()
        {
            return await _unitOfWork.nextNumberRepository.GetNextNumber(TypeOfShipment.ShipmentNumber.ToString());
        }
        public async Task<Shipment> GetEntityByNumber(long shipmentNumber)
        {
            return await _unitOfWork.shipmentRepository.GetEntityByNumber(shipmentNumber);
        }
        public override async Task<Shipment> GetEntityById(long ? shipmentId)
        {
            return await _unitOfWork.shipmentRepository.GetEntityById(shipmentId);
        }
        public override async Task<ShipmentView> GetViewById(long ? shipmentId)
        {
            Shipment detailItem = await _unitOfWork.shipmentRepository.GetEntityById(shipmentId);

            return await MapToView(detailItem);
        }
        public async Task<ShipmentView> GetViewByNumber(long shipmentNumber)
        {
            Shipment detailItem = await _unitOfWork.shipmentRepository.GetEntityByNumber(shipmentNumber);

            return await MapToView(detailItem);
        }
        public async Task<ShipmentDetailView> MapToDetailView(ShipmentDetail inputObject)
        {
 
            ShipmentDetailView outObject = mapper.Map<ShipmentDetailView>(inputObject);
            await Task.Yield();
            return outObject;
        }


        public async Task<PageListViewContainer<ShipmentView>> GetViewsByPage(Expression<Func<Shipment, bool>> predicate, Expression<Func<Shipment, object>> order, int pageSize, int pageNumber)
        {
            try
            {
                //IEnumerable<SalesOrder> query = _dbContext.SalesOrder.Where(predicate).OrderByDescending(order).Select(e => e);
                //var query = _dbContext.Shipment.Where(predicate).OrderByDescending(order).Select(e => e);
                //

                var query =  _unitOfWork.shipmentRepository.GetEntitiesByExpression(predicate);
                    
                query=query.OrderByDescending(order).Select(e => e);
        

                IPagedList<Shipment> list = await query.ToPagedListAsync(pageNumber, pageSize);

                PageListViewContainer<ShipmentView> container = new PageListViewContainer<ShipmentView>();
                container.PageNumber = pageNumber;
                container.PageSize = pageSize;
                container.TotalItemCount = list.TotalItemCount;

                ShipmentView view;
                foreach (var item in list)
                {
                    view = await MapToView(item);

                    IList<ShipmentDetail> listDetails = await _unitOfWork.shipmentDetailRepository.GetEntitiesByShipmentId(item.ShipmentId);

                    foreach (var item2 in listDetails)
                    {
                        view.ShipmentDetailViews.Add(await MapToDetailView(item2));
                    }


                    container.Items.Add(view);
                }

                await Task.Yield();

                //await Task.Yield();
                return container;
            }
            catch (Exception ex)
            {
                throw new Exception(GetMyMethodName(), ex);
            }

        }

    }
}
