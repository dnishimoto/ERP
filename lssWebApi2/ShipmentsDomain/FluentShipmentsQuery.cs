
using ERP_Core2.AutoMapper;
using ERP_Core2.Services;
using lssWebApi2.AbstractFactory;
using lssWebApi2.EntityFramework;
using lssWebApi2.Enumerations;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace lssWebApi2.ShipmentsDomain
{
    public class FluentShipmentsQuery : IFluentShipmentsQuery
    {
        private UnitOfWork _unitOfWork = null;
        public FluentShipmentsQuery() { }
        public FluentShipmentsQuery(UnitOfWork unitOfWork) { _unitOfWork = unitOfWork; }

        public async Task<Shipments> MapToEntity(ShipmentsView inputObject)
        {
            Mapper mapper = new Mapper();
            Shipments outObject = mapper.Map<Shipments>(inputObject);
            await Task.Yield();
            return outObject;
        }

        public async Task<List<Shipments>> MapToEntity(List<ShipmentsView> inputObjects)
        {
            List<Shipments> list = new List<Shipments>();
            Mapper mapper = new Mapper();
            foreach (var item in inputObjects)
            {
                Shipments outObject = mapper.Map<Shipments>(item);
                list.Add(outObject);
            }
            await Task.Yield();
            return list;

        }

        public async Task<ShipmentsView> MapToView(Shipments inputObject)
        {
            Mapper mapper = new Mapper();
            ShipmentsView outObject = mapper.Map<ShipmentsView>(inputObject);
            await Task.Yield();
            return outObject;
        }

        public async Task<Shipments> CreateShipmentBySalesOrder(ShipmentCreationView shipmentCreation)
        {

            return await _unitOfWork.shipmentsRepository.CreateShipmentBySalesOrder(shipmentCreation);
        }
        public async Task<NextNumber> GetNextNumber()
        {
            return await _unitOfWork.shipmentsRepository.GetNextNumber(TypeOfNextNumberEnum.ShipmentsNumber.ToString());
        }
        public async Task<Shipments> GetEntityByNumber(long shipmentNumber)
        {
            return await _unitOfWork.shipmentsRepository.GetEntityByNumber(shipmentNumber);
        }
        public async Task<Shipments> GetEntityById(long shipmentId)
        {
            return await _unitOfWork.shipmentsRepository.GetEntityById(shipmentId);
        }
        public async Task<ShipmentsView> GetViewById(long shipmentId)
        {
            Shipments detailItem = await _unitOfWork.shipmentsRepository.GetEntityById(shipmentId);

            return await MapToView(detailItem);
        }
        public async Task<ShipmentsView> GetViewByNumber(long shipmentNumber)
        {
            Shipments detailItem = await _unitOfWork.shipmentsRepository.GetEntityByNumber(shipmentNumber);

            return await MapToView(detailItem);
        }
        public async Task<PageListViewContainer<ShipmentsView>> GetViewsByPage(Func<Shipments, bool> predicate, Func<Shipments, object> order, int pageSize, int pageNumber)
        {
            return await _unitOfWork.shipmentsRepository.GetViewsByPage(predicate, order, pageSize, pageNumber);
        }

    }
}
