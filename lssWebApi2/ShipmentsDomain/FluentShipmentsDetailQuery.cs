using ERP_Core2.AutoMapper;
using ERP_Core2.Services;
using lssWebApi2.EntityFramework;
using lssWebApi2.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lssWebApi2.ShipmentsDomain
{

    public class FluentShipmentsDetailQuery : IFluentShipmentsDetailQuery
    {
        private UnitOfWork _unitOfWork = null;
        public FluentShipmentsDetailQuery() { }
        public FluentShipmentsDetailQuery(UnitOfWork unitOfWork) { _unitOfWork = unitOfWork; }

        public async Task<ShipmentsDetail> MapToEntity(ShipmentsDetailView inputObject)
        {
            Mapper mapper = new Mapper();
            ShipmentsDetail outObject = mapper.Map<ShipmentsDetail>(inputObject);
            await Task.Yield();
            return outObject;
        }

        public async Task<List<ShipmentsDetail>> MapToEntity(List<ShipmentsDetailView> inputObjects)
        {
            List<ShipmentsDetail> list = new List<ShipmentsDetail>();
            Mapper mapper = new Mapper();
            foreach (var item in inputObjects)
            {
                ShipmentsDetail outObject = mapper.Map<ShipmentsDetail>(item);
                list.Add(outObject);
            }
            await Task.Yield();
            return list;

        }

        public async Task<ShipmentsDetailView> MapToView(ShipmentsDetail inputObject)
        {
            Mapper mapper = new Mapper();
            ShipmentsDetailView outObject = mapper.Map<ShipmentsDetailView>(inputObject);
            await Task.Yield();
            return outObject;
        }
        public async Task<List<ShipmentsDetail>> GetEntitiesByShipmentId(long shipmentId)
        {
            return await _unitOfWork.shipmentsDetailRepository.GetEntitiesByShipmentId(shipmentId);
        }
        public async Task<List<ShipmentsDetailView>> GetViewsByShipmentId(long shipmentId)
        {
            List<ShipmentsDetailView> listViews = new List<ShipmentsDetailView>();
            List<ShipmentsDetail> list = await _unitOfWork.shipmentsDetailRepository.GetEntitiesByShipmentId(shipmentId);
            foreach (var item in list)
            {
                listViews.Add(await MapToView(item));
            }

            return listViews;
        }
        public async Task<NextNumber> GetNextNumber()
        {
            return await _unitOfWork.shipmentsDetailRepository.GetNextNumber(TypeOfNextNumberEnum.ShipmentsDetailNumber.ToString());
        }
        public async Task<ShipmentsDetailView> GetViewById(long shipmentDetailId)
        {
            ShipmentsDetail detailItem = await _unitOfWork.shipmentsDetailRepository.GetEntityById(shipmentDetailId);

            return await MapToView(detailItem);
        }
        public async Task<ShipmentsDetailView> GetViewByNumber(long shipmentDetailNumber)
        {
            ShipmentsDetail detailItem = await _unitOfWork.shipmentsDetailRepository.GetEntityByNumber(shipmentDetailNumber);

            return await MapToView(detailItem);
        }
        public async Task<ShipmentsDetail> GetEntityById(long shipmentDetailId) {
            return await _unitOfWork.shipmentsDetailRepository.GetEntityById(shipmentDetailId);
        }
        public async Task<ShipmentsDetail> GetEntityByNumber(long shipmentDetailNumber) {
            return await _unitOfWork.shipmentsDetailRepository.GetEntityByNumber(shipmentDetailNumber);
        }

    }
}